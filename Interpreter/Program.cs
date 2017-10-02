using System;
using Interpreter.Lexer;
using Interpreter.Parser;

namespace Interpreter
{
	class Program
	{
		public static void Main()
		{
			Lexer.Lexer lex = new Lexer.Lexer("int abc; abc = 2 + 4;");
			Parser.Parser parser = new Parser.Parser(lex);
			Context.Context context = new Context.Context();
			var stmt1 = parser.BuildStatement();
			var stmt2 = parser.BuildStatement();
			Console.WriteLine(stmt1);
			Console.WriteLine(stmt2);
			stmt1.Execute(context);
			stmt2.Execute(context);
			/*try
			{
				stmt1.Execute(context);
				stmt2.Execute(context);
			}
			catch (SyntaxException e)
			{
				Console.WriteLine(e.Message);
			}*/

			Console.Read();
		}
	}
}
