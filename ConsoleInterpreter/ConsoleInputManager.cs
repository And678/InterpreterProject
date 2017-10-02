using System;
using Interpreter.Context;

namespace ConsoleInterpreter
{
	public class ConsoleInputManager : IInputManager
	{
		public string GetLineFromUser()
		{
			Console.Write(">>");
			return Console.ReadLine();
		}
		public void PrintLine(string line)
		{
			Console.WriteLine(line);
		}
	}
}