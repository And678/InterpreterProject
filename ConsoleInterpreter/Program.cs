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
				Console.WriteLine("#################################################################\n" +
								"#                                                               #\n" +
								"#                           MPZ Lab 1                           #\n" +
								"#                          AndriyScript                         #\n" +
								"#                                                               #\n" +
								"#                                           Made by:            #\n" +
								"#                                           Andriy Kaminskyy    #\n" +
								"#                                           NULP IKNI PI-32     #\n" +
								"#                                                               #\n" +
								"#                           Lviv-2017                           #\n" +
								"#################################################################\n");
			}
			else if (File.Exists(args[0]))
			{
				File.ReadAllText(args[0]);
				Lexer lex = new Lexer(File.ReadAllText(args[0]));
				Parser parser = new Parser(lex);
				Context context = new Context();
				try
				{
					IStatement cur = parser.BuildStatement();
					while (cur != null)
					{
						cur.Execute(context);
						string abc = context.GetOutput();
						if (abc != string.Empty)
							Console.WriteLine(abc);
						cur = parser.BuildStatement();
					}
				}
				catch (SyntaxException e)
				{
					Console.WriteLine($"SyntaxError: {e.Message}");
				}
			}
			else
			{
				Console.WriteLine($"File {args[0]} does not exist.");
			}
			Console.Read();
		}
	}
}
