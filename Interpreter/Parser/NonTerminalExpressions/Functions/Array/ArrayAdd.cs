using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;
using Interpreter.Parser.TerminalExpressions;

namespace Interpreter.Parser.NonTerminalExpressions.Functions.Array
{
	public class ArrayAdd : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _array;
		private IExpression _expr;

		public ArrayAdd(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ARRAYADD accepts {ArgNumber} arguments.");
			_array = expr[0];
			_expr = expr[1];
		}

		public Value Interpret(Context.IContext context)
		{
			var result1 = _array.Interpret(context);
			var result2 = _expr.Interpret(context);

			if (result1.Type == ValueTypes.Array)
			{
				TypeHelpers.Convert<List<Value>>(result1).Add(Value.Copy(result2));
			}
			else
			{
				throw new SyntaxException($"ARRAYADD does not support {result1.Type}.");
			}
			return null;
		}
	}
}