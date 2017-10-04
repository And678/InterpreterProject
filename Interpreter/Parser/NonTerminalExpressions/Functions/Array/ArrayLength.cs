using System.Collections.Generic;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions.Array
{
	public class ArrayLength : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _array;

		public ArrayLength(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ARRAYLENGTH accepts {ArgNumber} arguments.");
			_array = expr[0];
		}

		public Value Interpret(Context.Context context)
		{
			var result1 = _array.Interpret(context);

			if (result1.Type == "array")
			{
				return new Value("int", TypeHelpers.Convert<List<Value>>(result1).Count);
			}
			throw new SyntaxException($"ARRAYLENGTH does not support {result1.Type}.");
		}
	}
}