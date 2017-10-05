using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Parser;

namespace Interpreter.Context
{
	public class Context : IContext
	{
		private Dictionary<string, Value> _variables;

		private IInputManager _inputManager;

		public Context(IInputManager myInputManager, string myPath)
		{
			_variables = new Dictionary<string, Value>();
			_inputManager = myInputManager;
			AddVariable(ValueTypes.Path, "thisfile", myPath);
			AddVariable(ValueTypes.Path, "thisdir", Path.GetDirectoryName(myPath));
		}

		public string GetCurrentFile()
		{
			return TypeHelpers.Convert<string>(LookUpVariable("thisfile"));
		}

		public string GetCurrentPath()
		{
			return TypeHelpers.Convert<string>(LookUpVariable("thisdir"));
		}

		public Value LookUpVariable(string name)
		{
			if (_variables.ContainsKey(name))
			{
				return _variables[name];
			}
			throw new SyntaxException("Variable does not exist");
		}

		public void AddVariable(ValueTypes type, string name)
		{
			if (!_variables.ContainsKey(name))
			{
				if (type == ValueTypes.Array)
				{
					_variables.Add(name, new Value(type, new List<Value>()));
				}
				else
				{
					_variables.Add(name, new Value(type));
				}
				return;
			}
			throw new SyntaxException("Variable already exists");
		}

		public void AddVariable(ValueTypes type, string name, object data)
		{
			if (!_variables.ContainsKey(name))
			{
				_variables.Add(name, new Value(type, data));
				return;
			}
			throw new SyntaxException("Variable already exists");
		}

		public void AddToOutput(string output)
		{
			_inputManager.PrintLine(output);
		}
		public void AddToOutput(int output)
		{
			_inputManager.PrintLine(output.ToString());
		}
		public void AddToOutput(bool output)
		{
			_inputManager.PrintLine(output.ToString());
		}

		public string GetInput()
		{
			return _inputManager.GetLineFromUser();
		}
	}
}
