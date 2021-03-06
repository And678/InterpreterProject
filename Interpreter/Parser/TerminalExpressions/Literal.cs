﻿using System.IO;
using Interpreter.Context;
using Interpreter.Lexer;

namespace Interpreter.Parser.TerminalExpressions
{
	public class Literal : IExpression
	{
		private string _value;
		private TokenType _type;

		public Literal(TokenType type, string value)
		{
			_value = value;
			_type = type;
		}
		public Value Interpret(Context.IContext context)
		{
			if (_type == TokenType.BoolLiteral)
			{
				if (_value == "true")
				{
					return new Value(ValueTypes.Bool, true);
				}
				if (_value == "false")
				{
					return new Value(ValueTypes.Bool, false);
				}
				throw new SyntaxException($"Unknown boolean literal - {_value}");
			}
			if (_type == TokenType.StringLiteral)
			{
				return new Value(ValueTypes.String, _value);
			}
			if (_type == TokenType.IntegerLiteral)
			{
				return new Value(ValueTypes.Int, int.Parse(_value));
			}
			if (_type == TokenType.PathLiteral)
			{
				if (FileHelpers.IsValidPath(_value))
				{
					if (Path.IsPathRooted(_value))
					{
						return new Value(ValueTypes.Path, _value);
					}
					return new Value(ValueTypes.Path, FileHelpers.BuildAbsolute(
						context.GetCurrentPath(), _value));
				}
			}
			throw new SyntaxException($"{_type} is not literal.");
		}
	}
}