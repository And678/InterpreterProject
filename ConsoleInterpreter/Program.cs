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
				Console.WriteLine("  █████╗ ███╗   ██╗██████╗ ██████╗ ██╗██╗   ██╗    ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗\r\n██╔══██╗████╗  ██║██╔══██╗██╔══██╗██║╚██╗ ██╔╝    ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝\r\n███████║██╔██╗ ██║██║  ██║██████╔╝██║ ╚████╔╝     ███████╗██║     ██████╔╝██║██████╔╝   ██║   \r\n██╔══██║██║╚██╗██║██║  ██║██╔══██╗██║  ╚██╔╝      ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   \r\n██║  ██║██║ ╚████║██████╔╝██║  ██║██║   ██║       ███████║╚██████╗██║  ██║██║██║        ██║   \r\n╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝       ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝");
				Console.WriteLine("╔╦╗┌─┐┌┬┐┌─┐  ┌┐ ┬ ┬  ┌─┐┌┐┌┌┬┐┬─┐┬┬ ┬  ╦╔═┌─┐┌┬┐┬┌┐┌┌─┐┬┌─┬ ┬┬ ┬\r\n║║║├─┤ ││├┤   ├┴┐└┬┘  ├─┤│││ ││├┬┘│└┬┘  ╠╩╗├─┤│││││││└─┐├┴┐└┬┘└┬┘\r\n╩ ╩┴ ┴─┴┘└─┘  └─┘ ┴   ┴ ┴┘└┘─┴┘┴└─┴ ┴   ╩ ╩┴ ┴┴ ┴┴┘└┘└─┘┴ ┴ ┴  ┴ \r\n┌┐┌┬ ┬┬  ┌─┐  ┌─┐┬  ─┐┌─┐                                              \r\n││││ ││  ├─┘  ├─┘│─ ─┤┌─┘                                          \r\n┘└┘└─┘┴─┘┴    ┴  ┴  ─┘└─┘   ");
				Console.WriteLine("Usage: >ConsoleInterpreter.exe <filepath>");
			}
			else if (File.Exists(args[0]))
			{
				File.ReadAllText(args[0]);
				Lexer lex = new Lexer(File.ReadAllText(args[0]));
				Parser parser = new Parser(lex);
				Context context = new Context(new ConsoleInputManager(), args[0]);
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
			}
			else
			{
				Console.WriteLine($"File {args[0]} does not exist.");
			}
			Console.Read();
		}
	}
}
