using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Parser;

namespace Interpreter.Context
{
	public class Context
	{
		private Dictionary<string, Value> _variables;

		private StringBuilder _output;

		public Context()
		{
			_output = new StringBuilder();
			_variables = new Dictionary<string, Value>();
		}

		public Value LookUpVariable(string name)
		{
			if (_variables.ContainsKey(name))
			{
				return _variables[name];
			}
			throw new SyntaxException("Variable does not exist");
		}

		public void AddVariable(string type, string name)
		{
			if (!_variables.ContainsKey(name))
			{
				_variables.Add(name, new Value(type));
				return;
			}
			throw new SyntaxException("Variable already exists");
		}

		public void AddToOutput(string output)
		{
			_output.Append(output);
		}
		public void AddToOutput(int output)
		{
			_output.Append(output);
		}
		public void AddToOutput(bool output)
		{
			_output.Append(output);
		}

		public string GetOutput()
		{
			var result = _output.ToString();
			_output.Clear();
			return result;
		}
	}
}
