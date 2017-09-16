using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Lexer
{
	static class LexerDefinitions
	{
		public static readonly string Terminator = ";";

		public static readonly List<string> TypeIdentifiers = new List<string>()
		{
			"int",
			"bool",
			"file",
			"string"
		};
		public static readonly string StringDelimiter = "\"";
		public static readonly string FileDelimiter = "\'";
		public static readonly List<string> BoolValues = new List<string>()
		{
			"true",
			"false"
		};
		public static readonly string NullLiteral = "null";

		//Operators: (MAXIMUM LENGTH FOR OPERATORS IS 2)
		public static readonly Dictionary<TokenType, string> Operators = 
			new Dictionary<TokenType, string>()
			{
				{ TokenType.LeftBracket, "("},
				{ TokenType.RightBracket, ")"},

				{ TokenType.LeftSquareBracket, "{"},
				{ TokenType.RightSquareBracket, "}"},
				{ TokenType.Plus, "+"},
				{ TokenType.Minus, "-"},
				{ TokenType.Multiply, "*"},
				{ TokenType.Divide, "/"},
				{ TokenType.Mod, "%"},
				{ TokenType.Assign, "="},

				{ TokenType.And, "&&"},
				{ TokenType.Or, "||"},
				{ TokenType.Not, "!"},
				{ TokenType.Equality, "=="},
				{ TokenType.NotEquality, "!="}
			};
	
	}
}
