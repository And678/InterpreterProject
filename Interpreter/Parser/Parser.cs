using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Lexer;
using Interpreter.Parser.NonTerminalExpressions.Additive;
using Interpreter.Parser.NonTerminalExpressions.Equality;
using Interpreter.Parser.NonTerminalExpressions.Functions;
using Interpreter.Parser.NonTerminalExpressions.Functions.Array;
using Interpreter.Parser.NonTerminalExpressions.Logical;
using Interpreter.Parser.NonTerminalExpressions.Multiplicative;
using Interpreter.Parser.NonTerminalExpressions.Relational;
using Interpreter.Parser.NonTerminalExpressions.Unary;
using Interpreter.Parser.Statements;
using Interpreter.Parser.TerminalExpressions;

namespace Interpreter.Parser
{
	public class Parser
	{
		private Lexer.Lexer _lexer;

		private Token _currentToken;
		private Token _nextToken;

		public Parser(Lexer.Lexer lexer)
		{
			_lexer = lexer;
			Next();
		}

		private void Next()
		{
			if (_nextToken == null)
			{
				_currentToken = _lexer.GetNextToken();
				_nextToken = _lexer.GetNextToken();
			}
			else if (_nextToken.Type == TokenType.EOF)
			{
				_currentToken = _nextToken;
			}
			else
			{
				_currentToken = _nextToken;
				_nextToken = _lexer.GetNextToken();
			}
		}
		private Token Take()
		{
			var token = _currentToken;
			Next();
			return token;
		}

		private Token Take(TokenType type)
		{
			if (_currentToken.Type != type)
			{
				throw new SyntaxException($"{_currentToken} ({_currentToken.Type}) is not of type {type}");
			}
			return Take();
		}


		public IStatement BuildStatement()
		{
			if (_currentToken.Type == TokenType.EOF)
			{
				return null;
			}
			if (_currentToken.Type == TokenType.TypeIdentifier)
			{
				return BuildDeclaration();
			}
			if (_currentToken.Type == TokenType.Identifier)
			{
				if (_nextToken.Type == TokenType.LeftBracket)
				{
					return BuildInvocation();
				}
				if (_nextToken.Type == TokenType.Assign)
				{
					return BuildAssignment();
				}
			}
			if (_currentToken.Type == TokenType.If)
			{
				return BuildIf();
			}
			if (_currentToken.Type == TokenType.While)
			{
				return BuildWhile();
			}
			if (_currentToken.Type == TokenType.LeftSquigglyBracket)
			{
				return BuildScope();
			}
			
			throw new SyntaxException($"{_currentToken.Type} is not a statement.");
		}

		private IStatement BuildDeclaration()
		{
			Token typeToken = Take(TokenType.TypeIdentifier);
			Token identifier = Take(TokenType.Identifier);
			Take(TokenType.Terminator);
			return new Declaration(typeToken.Value, identifier.Value);
		}

		private IStatement BuildAssignment()
		{
			Token variable = Take(TokenType.Identifier);
			Take(TokenType.Assign);
			IExpression expression = ParseExpression();
			Take(TokenType.Terminator);

			return new Assignment(variable.Value, expression);
		}
		private IStatement BuildInvocation()
		{
			IExpression expr = ParseFunction();
			Take(TokenType.Terminator);
			return new Invocation(expr);
		}

		private IStatement BuildIf()
		{
			Take(TokenType.If);
			Take(TokenType.LeftBracket);
			IExpression expr = ParseExpression();
			Take(TokenType.RightBracket);
			IStatement stmt = BuildStatement();
			return new If(expr, stmt);
		}

		private IStatement BuildWhile()
		{
			Take(TokenType.While);
			Take(TokenType.LeftBracket);
			IExpression expr = ParseExpression();
			Take(TokenType.RightBracket);
			IStatement stmt = BuildStatement();
			return new While(expr, stmt);
		}

		private IStatement BuildScope()
		{
			List<IStatement> statements = new List<IStatement>();
			Take(TokenType.LeftSquigglyBracket);
			while (_currentToken.Type != TokenType.RightSquigglyBracket)
			{
				statements.Add(BuildStatement());
			}
			Take(TokenType.RightSquigglyBracket);
			return new Scope(statements);
		}

		private IExpression ParseExpression()
		{
			return ParseLogicalExpression();
		}

		private IExpression ParseLogicalExpression()
		{
			IExpression left = ParseEqualityExpression();
			while (IsLogical())
			{
				var op = Take();
				var right = ParseEqualityExpression();
				left = CreateNewBinaryExpression(op.Type, left, right);
			}
			return left;
		}
		private IExpression ParseEqualityExpression()
		{
			IExpression left = ParseRelationalExpression();
			while (IsEquality())
			{
				var op = Take();
				var right = ParseRelationalExpression();
				left = CreateNewBinaryExpression(op.Type, left, right);
			}
			return left;
		}
		private IExpression ParseRelationalExpression()
		{
			IExpression left = ParseAdditiveExpression();
			while (IsRelational())
			{
				var op = Take();
				var right = ParseAdditiveExpression();
				left = CreateNewBinaryExpression(op.Type, left, right);
			}
			return left;
		}
		private IExpression ParseAdditiveExpression()
		{
			IExpression left = ParseMultiplicativeExpression();
			while (IsAdditive())
			{
				var op = Take();
				var right = ParseMultiplicativeExpression();
				left = CreateNewBinaryExpression(op.Type, left, right);
			}
			return left;
		}
		private IExpression ParseMultiplicativeExpression()
		{
			IExpression left = ParseUnaryExpression();
			while (IsMultiplicative())
			{
				var op = Take();
				var right = ParseUnaryExpression();
				left = CreateNewBinaryExpression(op.Type, left, right);
			}
			return left;
		}
		private IExpression ParseUnaryExpression()
		{
			IExpression left;
			if (IsUnary())
			{
				var op = Take();
				left = ParsePrimaryExpression();
				left = CreateNewUnaryExpression(op.Type, left);
			}
			else
			{

				left = ParsePrimaryExpression();
			}
			return left;
		}
		private IExpression ParsePrimaryExpression()
		{
			if (_currentToken.Type == TokenType.LeftBracket)
			{
				Take(TokenType.LeftBracket);
				var expr = ParseExpression();
				Take(TokenType.RightBracket);
				return expr;
			}
			if (_currentToken.Type == TokenType.Identifier)
			{
				if (_nextToken.Type == TokenType.LeftBracket)
				{
					return ParseFunction();
				}
				if (_nextToken.Type == TokenType.LeftSquareBracket)
				{
					return ParseArrayAccess();
				}
				return ParseVariableExpr();

			}
			if (_currentToken.Type == TokenType.StringLiteral ||
				_currentToken.Type == TokenType.IntegerLiteral ||
				_currentToken.Type == TokenType.PathLiteral ||
				_currentToken.Type == TokenType.BoolLiteral)
			{
				return ParseLiteral();
			}
			throw new SyntaxException("Unknown expression");
		}

		private IExpression CreateNewBinaryExpression(TokenType type, IExpression left, IExpression right)
		{
			switch (type)
			{
				case TokenType.Plus:
					return new Add(left, right);
				case TokenType.Minus:
					return new Subtract(left, right);
				case TokenType.Equality:
					return new Equals(left, right);
				case TokenType.NotEquality:
					return new NotEquals(left, right);
				case TokenType.And:
					return new And(left, right);
				case TokenType.Or:
					return new Or(left, right);
				case TokenType.Divide:
					return new Divide(left, right);
				case TokenType.Multiply:
					return new Multiply(left, right);
				case TokenType.Mod:
					return new Mod(left, right);
				case TokenType.GreaterThan:
					return new GreaterThan(left, right);
				case TokenType.LessThan:
					return new LessThan(left, right);
				default:
					throw new SyntaxException($"{type.ToString()} is not binary operator");
			}
		}
		private IExpression CreateNewUnaryExpression(TokenType type, IExpression expr)
		{
			switch (type)
			{
				case TokenType.Not:
					return new Not(expr);
				default:
					throw new SyntaxException($"{type.ToString()} is not binary operator");
			}
		}

		private IExpression ParseFunction()
		{
			var identifier = Take(TokenType.Identifier);
			Take(TokenType.LeftBracket);
			List<IExpression> exprList = new List<IExpression>();
			while (_currentToken.Type != TokenType.RightBracket)
			{
				exprList.Add(ParseExpression());
				if (_currentToken.Type == TokenType.Comma)
				{
					Take(TokenType.Comma);
				}
			}
			Take(TokenType.RightBracket);
			switch (identifier.Value)
			{
				case "echo":
					return new Echo(exprList);
				case "tostr":
					return new ToStr(exprList);
				case "gets":
					return new Gets(exprList);
				case "toint":
					return new ToInt(exprList);
				case "getfilesize":
					return new GetFileSize(exprList);
				case "arrayadd":
					return new ArrayAdd(exprList);
				case "arraylength":
					return new ArrayLength(exprList);
				default:
					throw new SyntaxException($"Function {identifier.Value} does not exist.");
			}
		}

		private IExpression ParseVariableExpr()
		{
			var tok = Take(TokenType.Identifier);
			return new VariableExpr(tok.Value);
		}

		private IExpression ParseLiteral()
		{
			var tok = Take();
			return new Literal(tok.Type, tok.Value);
		}
		private IExpression ParseArrayAccess()
		{
			var array = ParseVariableExpr();
			Take(TokenType.LeftSquareBracket);
			var expr = ParseExpression();
			Take(TokenType.RightSquareBracket);
			return new ArrayAccess(expr, array);
		}
		#region IsTokenType
		private bool IsAdditive()
		{
			switch (_currentToken.Type)
			{
				case TokenType.Plus:
				case TokenType.Minus:
					return true;
				default:
					return false;
			}
		}
		private bool IsMultiplicative()
		{
			switch (_currentToken.Type)
			{
				case TokenType.Divide:
				case TokenType.Multiply:
				case TokenType.Mod:
					return true;
				default:
					return false;
			}
		}
		private bool IsEquality()
		{
			switch (_currentToken.Type)
			{
				case TokenType.Equality:
				case TokenType.NotEquality:
					return true;
				default:
					return false;
			}
		}

		private bool IsLogical()
		{
			switch (_currentToken.Type)
			{
				case TokenType.And:
				case TokenType.Or:
					return true;
				default:
					return false;
			}
		}

		private bool IsRelational()
		{
			switch (_currentToken.Type)
			{
				case TokenType.GreaterThan:
				case TokenType.LessThan:
					return true;
				default:
					return false;
			}
		}

		private bool IsUnary()
		{
			switch (_currentToken.Type)
			{
				case TokenType.Not:
					return true;
				default:
					return false;
			}
		}
		#endregion

	}
}
