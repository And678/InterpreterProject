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
			if (variable.Type == "int" && typeof(T) == typeof(int))
			{
				return (T)variable.Data;
			}

			if ((variable.Type == "string" || variable.Type == "path") && typeof(T) == typeof(string))
			{
				return (T)variable.Data;
			}

			if (variable.Type == "bool" && typeof(T) == typeof(bool))
			{
				return (T)variable.Data;
			}

			throw new InvalidCastException($"Cannot cast {variable.Data.ToString()} to {typeof(T).ToString()}.");
		}
	}
}
