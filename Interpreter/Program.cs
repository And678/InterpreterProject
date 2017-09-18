using System;
using Interpreter.Lexer;

namespace Interpreter
{
	class Program
	{
		public static void Main()
		{
			Lexer.Lexer lex = new Lexer.Lexer("int abc = 5;\n    abc = abc + 5;\n    echo(abc);\n   string xyz = \"This is a string!\";  ");
			Token tok = lex.GetNextToken();
			while (tok.Type != TokenType.EOF)
			{
				Console.WriteLine(tok.ToString());
				tok = lex.GetNextToken();
			}
			Console.Read();
		}
	}
}
