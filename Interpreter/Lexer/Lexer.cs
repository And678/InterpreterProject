using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Parser;

namespace Interpreter.Lexer
{
	public class Lexer
	{
		private int _currentIndex = 0;

		private string _code;


		public Lexer(string code)
		{
			_code = code;
		}

		private void ParseWhiteSpace()
		{
			while (_currentIndex < _code.Length && Char.IsWhiteSpace(_code[_currentIndex]))
			{
				_currentIndex++;
			}
		}
		public Token GetNextToken()
		{
			ParseWhiteSpace();
			if (_currentIndex >= _code.Length) {
				return new Token(TokenType.EOF);
			}
			Char current = _code[_currentIndex];
			if (current == LexerDefinitions.Terminator[0]) {
				_currentIndex++;
				return new Token(TokenType.Terminator);
			}
			if (current == LexerDefinitions.StringDelimiter[0])
			{
				return ParseStringLiteral();
			}
			if (current == LexerDefinitions.PathDelimiter[0])
			{
				return ParseStringLiteral(true);
			}
			if (Char.IsLetter(current))
			{
				return ParseIdentifier();
			}
			if (Char.IsDigit(current) || (current == '-' && Char.IsDigit(_code[_currentIndex + 1])))
			{
				return ParseNumber();
			}
			if (LexerDefinitions.OperatorSymbols.Contains(current.ToString()))
			{
				return ParseSymbols();
			}
			throw new SyntaxException("Wrong symbol");
		}


		private Token ParseStringLiteral(bool isPath = false)
		{
			_currentIndex++;
			StringBuilder newLiteral = new StringBuilder();
			try
			{
				while (_code[_currentIndex] != (isPath ? 
					LexerDefinitions.PathDelimiter[0] : LexerDefinitions.StringDelimiter[0]))
				{
					newLiteral.Append(_code[_currentIndex]);
					_currentIndex++;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new SyntaxException("Unenclosed string/path literal");
			}
			_currentIndex++;
			return new Token(
				(isPath ? TokenType.PathLiteral : TokenType.StringLiteral),
				newLiteral.ToString()
			);
		}

		private Token ParseIdentifier()
		{
			StringBuilder newLiteral = new StringBuilder();
			try
			{
				while (Char.IsLetter(_code[_currentIndex]))
				{
					newLiteral.Append(_code[_currentIndex]);
					_currentIndex++;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new SyntaxException("Missing ;");
			}
			string literalString = newLiteral.ToString().ToLower();


			if (literalString == LexerDefinitions.While)
			{
				return new Token(TokenType.While);
			}

			if (literalString == LexerDefinitions.If)
			{
				return new Token(TokenType.If);
			}
			var typeId = LexerDefinitions.TypeIdentifiers.FirstOrDefault(pair => pair.Key == literalString.ToString());
			if (typeId.Key != null)
			{
				return new Token(typeId.Value);
			}

			foreach (var type in LexerDefinitions.BoolValues)
			{
				if (literalString == type)
				{
					return new Token(
						TokenType.BoolLiteral,
						type
					);
				}
			}
			return new Token(
				TokenType.Identifier,
				literalString
			);
		}


		private Token ParseNumber()
		{
			StringBuilder newLiteral = new StringBuilder();
			try
			{
				if (_code[_currentIndex] == '-')
				{
					newLiteral.Append('-');
					_currentIndex++;
				}
				while (Char.IsDigit(_code[_currentIndex]))
				{
					newLiteral.Append(_code[_currentIndex]);
					_currentIndex++;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new SyntaxException("Missing ;");
			}
			return new Token(
				TokenType.IntegerLiteral,
				newLiteral.ToString()
			);
		}

		private Token ParseSymbols()
		{
			string oneSymbol = _code[_currentIndex].ToString();
			string twoSymbol = string.Empty;
			if (_currentIndex < _code.Length - 1)
				twoSymbol = _code.Substring(_currentIndex, 2);

			if (twoSymbol != string.Empty)
				foreach (var token in LexerDefinitions.Operators)
				{
					if (twoSymbol == token.Value)
					{
						_currentIndex += 2;
						return new Token(token.Key, token.Value);
					}
				}
			foreach (var token in LexerDefinitions.Operators)
			{
				if (oneSymbol == token.Value)
				{
					_currentIndex++;
					return new Token(token.Key, token.Value);
				}
			}
			throw new SyntaxException("Unknown operator");

		}
	}
}
