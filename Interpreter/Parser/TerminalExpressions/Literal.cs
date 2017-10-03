using System.IO;
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
		public Value Intrerpret(Context.Context context)
		{
			if (_type == TokenType.BoolLiteral)
			{
				if (_value == "true")
				{
					return new Value("bool", true);
				}
				if (_value == "false")
				{
					return new Value("bool", false);
				}
				throw new SyntaxException($"Unknown boolean literal - {_value}");
			}
			if (_type == TokenType.StringLiteral)
			{
				return new Value("string", _value);
			}
			if (_type == TokenType.IntegerLiteral)
			{
				return new Value("int", int.Parse(_value));
			}
			if (_type == TokenType.PathLiteral)
			{
				if (FileHelpers.IsValidPath(_value))
				{
					if (Path.IsPathRooted(_value))
					{
						return new Value("path", _value);
					}
					return new Value("path", FileHelpers.BuildAbsolute(
						context.GetCurrentPath(), _value));
				}
			}
			throw new SyntaxException($"{_type} is not literal.");
		}
	}
}