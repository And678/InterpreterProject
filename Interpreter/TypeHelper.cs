using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter
{
	public static class TypeHelper
	{
		public static T Convert<T>(Value variable)
		{
			if (variable.Type == "int" && typeof(T) == typeof(int))
			{
				return (T)variable.Data;
			}

			if ((variable.Type == "string" || variable.Type == "file") && typeof(T) == typeof(string))
			{
				return (T)variable.Data;
			}

			if (variable.Type == "bool" && typeof(T) == typeof(bool))
			{
				return (T)variable.Data;
			}

			throw new InvalidCastException($"Cannot {variable.Data.ToString()} to {variable.Type}.");
		}
	}
}
