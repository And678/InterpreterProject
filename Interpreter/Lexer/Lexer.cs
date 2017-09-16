using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Token GetNextToken()
		{

			if (_currentIndex >= _code.Length) {
				return new Token(TokenType.EOF);
			}


			while (Char.IsWhiteSpace(_code[_currentIndex])) {
				_currentIndex++;

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
			if (current == LexerDefinitions.FileDelimiter[0])
			{
				return ParseStringLiteral(true);
			}
			if (Char.IsLetter(current))
			{
				return ParseIdentifier();
			}
			if (Char.IsDigit(current))
			{
				return ParseNumber();
			}
			if (Char.IsSymbol(current))
			{
				return ParseSymbols();
			}
			throw new FormatException("Wrong symbol");
		}


		private Token ParseStringLiteral(bool isFile = false)
		{
			_currentIndex++;
			StringBuilder newLiteral = new StringBuilder();
			try
			{
				while (_code[_currentIndex] != (isFile ? 
					LexerDefinitions.FileDelimiter[0] : LexerDefinitions.StringDelimiter[0]))
				{
					newLiteral.Append(_code[_currentIndex]);
					_currentIndex++;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new FormatException("Unenclosed string/file literal");
			}
			_currentIndex++;
			return new Token(
				(isFile ? TokenType.FileLiteral : TokenType.StringLiteral),
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
				throw new FormatException("Missing ;");
			}
			string literalString = newLiteral.ToString().ToLower();

			if (literalString == LexerDefinitions.NullLiteral)
			{
				return new Token(TokenType.NullLiteral);
			}

			foreach (var type in LexerDefinitions.TypeIdentifiers)
			{
				if (literalString == type)
				{
					return new Token(
						TokenType.TypeIdentifier,
						type
					);
				}
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
				while (Char.IsDigit(_code[_currentIndex]))
				{
					newLiteral.Append(_code[_currentIndex]);
					_currentIndex++;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new FormatException("Missing ;");
			}
			return new Token(
				TokenType.IntegerLiteral,
				newLiteral.ToString()
			);
		}

		private Token ParseSymbols()
		{
			string oneSymbol = _code[_currentIndex].ToString();
			string twoSymbol;
			try
			{
				twoSymbol = _code.Substring(_currentIndex, 2);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new FormatException("Missing ;");
			}

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
			throw new FormatException("Unknown operator");

		}
	}
}
