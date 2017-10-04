using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter
{
	public static class TypeHelpers
	{
		public static T Convert<T>(Value variable)
		{
			if (variable.Type == ValueTypes.Int && typeof(T) == typeof(int))
			{
				return (T)variable.Data;
			}

			if ((variable.Type == ValueTypes.String || variable.Type == ValueTypes.Path) && typeof(T) == typeof(string))
			{
				return (T)variable.Data;
			}

			if (variable.Type == ValueTypes.Bool && typeof(T) == typeof(bool))
			{
				return (T)variable.Data;
			}

			if (variable.Type == ValueTypes.Array && typeof(T) == typeof(List<Value>))
			{
				return (T)variable.Data;
			}
			throw new InvalidCastException($"Cannot cast {variable.Data.ToString()} to {typeof(T).ToString()}.");
		}
	}
}
