using System.Collections.Generic;
using Interpreter.Context;

namespace Interpreter.Parser.TerminalExpressions
{
	public class ArrayAccess : IExpression
	{
		private IExpression _index;
		private IExpression _array;

		public ArrayAccess(IExpression index, IExpression array)
		{
			_index = index;
			_array = array;
		}
		public Value Interpret(Context.IContext context)
		{
			var arr = _array.Interpret(context);
			var ind = _index.Interpret(context);
			if (arr.Type == ValueTypes.Array && ind.Type == ValueTypes.Int)
			{
				return TypeHelpers.Convert<List<Value>>(arr)[TypeHelpers.Convert<int>(ind)];
			}
			throw new SyntaxException($"ARRAYACCESS does not support {arr.Type}[{ind.Type}]");
		}
	}
}