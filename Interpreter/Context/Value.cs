using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Context
{
	public class Value
	{
		public string Type { get; }
		public object Data { get; private set; }

		public Value(string type)
		{
			Type = type;
			Data = null;
		}
		public Value(string type, object value)
		{
			Type = type;
			Data = value;
		}

		public void SetValue(object value)
		{
			Data = value;
		}
		

	}
}
