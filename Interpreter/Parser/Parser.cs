using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Lexer;
using Interpreter.Parser.NonTerminalExpressions.Additive;
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
				throw new SyntaxException($"{_currentToken} is not of type {type}");
			}
			return Take();
		}


		public IStatement BuildStatement()
		{
			if (_currentToken.Type == TokenType.TypeIdentifier)
			{
				return BuildDeclaration();
			}
			if (_currentToken.Type == TokenType.Identifier)
			{
				if (_nextToken.Type == TokenType.LeftBracket)
				{
					//Invocation
					throw new NotImplementedException();
				}
				if (_nextToken.Type == TokenType.Assign)
				{
					return BuildAssignment();
				}
			}
			if (_currentToken.Type == TokenType.If)
			{
				//If
				throw new NotImplementedException();
			}
			if (_currentToken.Type == TokenType.While)
			{
				//While
				throw new NotImplementedException();
			}
			if (_currentToken.Type == TokenType.LeftSquareBracket)
			{
				//Scope
				throw new NotImplementedException();
			}
			
			throw new NotImplementedException();
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

		private IExpression ParseExpression()
		{
			return ParseLogicalExpression();
		}

		private IExpression ParseLogicalExpression()
		{
			IExpression left = ParseEqualityExpression();
			// while is logical
			//do parse right
			// left = exp(left, right)
			return left;
		}
		private IExpression ParseEqualityExpression()
		{
			IExpression left = ParseRelationalExpression();
			// while is equality
			//do parse right
			// left = exp(left, right)
			return left;
		}
		private IExpression ParseRelationalExpression()
		{
			IExpression left = ParseAdditiveExpression();
			// while is relational
			//do parse right
			// left = exp(left, right)
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
			// while is multiplicative
			//do parse right
			// left = exp(left, right)
			return left;
		}
		private IExpression ParseUnaryExpression()
		{
			IExpression left = ParsePrimaryExpression();
			// parse unary
			return left;
		}
		private IExpression ParsePrimaryExpression()
		{
			if (_currentToken.Type == TokenType.Identifier)
			{
				if (_nextToken.Type == TokenType.LeftBracket)
				{
					// Parse function
					throw new NotImplementedException();
				}
				var tok = Take();
				return new VariableExpr(tok.Value);

			}
			if (_currentToken.Type == TokenType.StringLiteral ||
				_currentToken.Type == TokenType.IntegerLiteral ||
				_currentToken.Type == TokenType.FileLiteral ||
				_currentToken.Type == TokenType.BoolLiteral)
			{
				var tok = Take();
				return new Literal(tok.Type, tok.Value);
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
					throw new NotImplementedException();
				case TokenType.Equality:
					throw new NotImplementedException();
				case TokenType.NotEquality:
					throw new NotImplementedException();
				case TokenType.And:
					throw new NotImplementedException();
				case TokenType.Or:
					throw new NotImplementedException();
				case TokenType.Divide:
					throw new NotImplementedException();
				case TokenType.Multiply:
					throw new NotImplementedException();
				case TokenType.GreaterThan:
					throw new NotImplementedException();
				case TokenType.LessThan:
					throw new NotImplementedException();
				default:
					throw new SyntaxException($"{type.ToString()} is not binary operator");
			}	
		}
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

	}
}
