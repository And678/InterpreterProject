using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Context
{
	public class Context
	{
		private Dictionary<string, Value> _variables;

		public Context()
		{
			_variables = new Dictionary<string, Value>();
		}

		public Value LookUpVariable(string name)
		{
			if (_variables.ContainsKey(name))
			{
				return _variables[name];
			}
			throw new ApplicationException("Variable does not exist");
		}

		public void AddVariable(string type, string name)
		{
			if (!_variables.ContainsKey(name))
			{
				_variables.Add(name, new Value(type));
				return;
			}
			throw new ApplicationException("Variable already exists");
		}
	}
}
