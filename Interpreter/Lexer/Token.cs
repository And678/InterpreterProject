using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Lexer
{
	public class Token
	{
		public TokenType Type { get; }
		public string Value { get; }

		public Token(TokenType type)
		{
			Type = type;
			Value = String.Empty;
		}
		public Token(TokenType type, string value)
		{
			Type = type;
			Value = value;
		}

		public override string ToString()
		{
			StringBuilder newString = new StringBuilder();
			newString.Append('<');
			newString.Append(Type.ToString());
			if (Value != String.Empty)
			{
				newString.Append($", {Value}");
			}
			newString.Append('>');
			return newString.ToString();
		}
	}
}
