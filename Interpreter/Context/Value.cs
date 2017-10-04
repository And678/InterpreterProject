using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Parser;

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

		public static Value Copy(Value copiable)
		{
			if (copiable.Type == "path" || copiable.Type == "string")
			{
				return new Value(String.Copy(copiable.Type), String.Copy((string)copiable.Data));
			}
			if (copiable.Type == "int")
			{
				return new Value(String.Copy(copiable.Type), (int)copiable.Data);
			}
			if (copiable.Type == "bool")
			{
				return new Value(String.Copy(copiable.Type), (bool)copiable.Data);
			}
			if (copiable.Type == "array")
			{
				return new Value(String.Copy(copiable.Type), ((List<Value>)copiable.Data).ConvertAll(input => Value.Copy(input)));
			}
			throw new SyntaxException($"Can't copy {copiable.Type}");
		}

		public void SetValue(object value)
		{
			Data = value;
		}
		

	}
}
