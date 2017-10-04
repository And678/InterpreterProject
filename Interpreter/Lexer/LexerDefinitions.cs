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
			"path",
			"string",
			"array"
		};
		public static readonly string StringDelimiter = "\"";
		public static readonly string PathDelimiter = "\'";
		public static readonly List<string> BoolValues = new List<string>()
		{
			"true",
			"false"
		};

		public static readonly string While = "while";
		public static readonly string If = "if";

		public static readonly string OperatorSymbols = "(){}+-*/%^&|[]<>,!=";
		//Operators: (MAXIMUM LENGTH FOR OPERATORS IS 2)
		public static readonly Dictionary<TokenType, string> Operators = 
			new Dictionary<TokenType, string>()
			{
				{ TokenType.LeftBracket, "("},
				{ TokenType.RightBracket, ")"},

				{ TokenType.LeftSquareBracket, "["},
				{ TokenType.RightSquareBracket, "]"},

				{ TokenType.LeftSquigglyBracket, "{"},
				{ TokenType.RightSquigglyBracket, "}"},

				{ TokenType.Plus, "+"},
				{ TokenType.Minus, "-"},
				{ TokenType.Multiply, "*"},
				{ TokenType.Divide, "/"},
				{ TokenType.Mod, "%"},
				{ TokenType.Assign, "="},
				{ TokenType.GreaterThan, ">" },
				{ TokenType.LessThan, "<" },
				{ TokenType.Comma, ","},

				{ TokenType.And, "&&"},
				{ TokenType.Or, "||"},
				{ TokenType.Not, "!"},
				{ TokenType.Equality, "=="},
				{ TokenType.NotEquality, "!="}
			};
	
	}
}
