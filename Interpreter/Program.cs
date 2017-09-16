using System;
using Interpreter.Lexer;

namespace Interpreter
{
	class Program
	{
		public static void Main()
		{
			Lexer.Lexer lex = new Lexer.Lexer("\"abc\"1613166  goodlife1 654 int 654bool 000 intbool\'good\'54136  shotgun\"123\"==;");
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
