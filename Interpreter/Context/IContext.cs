using System.Collections.Generic;
using Interpreter.Parser;

namespace Interpreter.Context
{
	public interface IContext
	{
		IFileManager FileManager { get; }
		string GetCurrentFile();
		string GetCurrentPath();
		Value LookUpVariable(string name);
		void AddVariable(ValueTypes type, string name);
		void AddVariable(ValueTypes type, string name, object data);
		void AddToOutput(string output);
		void AddToOutput(int output);
		void AddToOutput(bool output);
		string GetInput();
	}
}