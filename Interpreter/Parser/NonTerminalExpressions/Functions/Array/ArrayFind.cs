using System;
using System.Collections.Generic;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions.Array
{
	public class ArrayFind : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _array;
		private IExpression _expr;

		public ArrayFind(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ARRAYFIND accepts {ArgNumber} arguments.");
			_array = expr[0];
			_expr = expr[1];
		}

		public Value Interpret(Context.Context context)
		{
			var result1 = _array.Interpret(context);
			var result2 = _expr.Interpret(context);

			if (result1.Type == ValueTypes.Array)
			{
				return new Value(ValueTypes.Int, TypeHelpers.Convert<List<Value>>(result1).FindIndex(value => Match(value, result2)));
			}
			throw new SyntaxException($"ARRAYFIND does not support ({result1.Type}).");
		}

		private bool Match(Value value1, Value value2)
		{
			if (value1.Type == value2.Type)
			{
				switch (value1.Type)
				{
					case ValueTypes.Int:
						return (int) value1.Data == (int) value2.Data;
					case ValueTypes.Path:
					case ValueTypes.String:
						return (string)value1.Data == (string)value2.Data;
					case ValueTypes.Bool:
						return (bool)value1.Data == (bool)value2.Data;
					case ValueTypes.Array:
						return (List<Value>)value1.Data == (List<Value>)value2.Data;
				}
			}
			return false;
		}
	}
}