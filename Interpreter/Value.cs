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
		public ValueTypes Type { get; }
		public object Data { get; private set; }

		public Value(ValueTypes type)
		{
			Type = type;
			Data = null;
		}
		public Value(ValueTypes type, object value)
		{
			Type = type;
			Data = value;
		}

		public static Value Copy(Value copiable)
		{
			if (copiable.Type == ValueTypes.Path || copiable.Type == ValueTypes.String)
			{
				return new Value(copiable.Type, String.Copy((string)copiable.Data));
			}
			if (copiable.Type == ValueTypes.Int)
			{
				return new Value(copiable.Type, (int)copiable.Data);
			}
			if (copiable.Type == ValueTypes.Bool)
			{
				return new Value(copiable.Type, (bool)copiable.Data);
			}
			if (copiable.Type == ValueTypes.Array)
			{
				return new Value(copiable.Type, ((List<Value>)copiable.Data).ConvertAll(input => Value.Copy(input)));
			}
			throw new SyntaxException($"Can't copy {copiable.Type}");
		}

		public void SetValue(object value)
		{
			Data = value;
		}
		

	}
}
