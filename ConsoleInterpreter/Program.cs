using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;
using Interpreter.Lexer;
using Interpreter.Parser;

namespace ConsoleInterpreter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine(" █████╗ ███╗   ██╗██████╗ ██████╗ ██╗██╗   ██╗    ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗\r\n██╔══██╗████╗  ██║██╔══██╗██╔══██╗██║╚██╗ ██╔╝    ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝\r\n███████║██╔██╗ ██║██║  ██║██████╔╝██║ ╚████╔╝     ███████╗██║     ██████╔╝██║██████╔╝   ██║   \r\n██╔══██║██║╚██╗██║██║  ██║██╔══██╗██║  ╚██╔╝      ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   \r\n██║  ██║██║ ╚████║██████╔╝██║  ██║██║   ██║       ███████║╚██████╗██║  ██║██║██║        ██║   \r\n╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝       ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝");
				Console.WriteLine("Made by Andriy Kaminskyy IKNI PI-32");
				Console.Write("Usage: >ConsoleInterpreter.exe <filepath>");
				Console.ReadKey();
			}
			else if (File.Exists(args[0]))
			{
				Lexer lex = new Lexer(File.ReadAllText(args[0]));
				Parser parser = new Parser(lex, new FunctionManager());
				IContext context = new Context(new FunctionManager(), new ConsoleInputManager(), new FileManager(), args[0]);
				try
				{
					IStatement cur = parser.BuildStatement();
					while (cur != null)
					{
						cur.Execute(context);
						cur = parser.BuildStatement();
					}
				}
				catch (SyntaxException e)
				{
					Console.WriteLine($"SyntaxError: {e.Message}");
				}
				Console.Write("Program end.");
				Console.ReadKey();
			}
			else
			{
				Console.WriteLine($"File {args[0]} does not exist.");
				Console.ReadKey();
			}
		}
	}
}
