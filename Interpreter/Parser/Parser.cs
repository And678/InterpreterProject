using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Lexer;
using Interpreter.Parser.Statements;

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
				throw new ApplicationException("Failed to take token");	//TODO: Better errors
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
			throw new NotImplementedException();
		}
		
	}
}
