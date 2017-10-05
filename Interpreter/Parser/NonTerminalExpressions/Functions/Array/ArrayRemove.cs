using System.Collections.Generic;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions.Array
{
	public class ArrayRemove : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _array;
		private IExpression _expr;

		public ArrayRemove(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ARRAYREMOVE accepts {ArgNumber} arguments.");
			_array = expr[0];
			_expr = expr[1];
		}

		public Value Interpret(Context.IContext context)
		{
			var result1 = _array.Interpret(context);
			var result2 = _expr.Interpret(context);

			if (result1.Type == ValueTypes.Array && result2.Type == ValueTypes.Int)
			{
				TypeHelpers.Convert<List<Value>>(result1).RemoveAt(TypeHelpers.Convert<int>(result2));
			}
			else
			{
				throw new SyntaxException($"ARRAYREMOVE does not support ({result1.Type}, {result2.Type}).");
			}
			return null;
		}
	}
}