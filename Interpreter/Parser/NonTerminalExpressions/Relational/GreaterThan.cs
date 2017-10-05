using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Relational
{
	public class GreaterThan : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public GreaterThan(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		public Value Interpret(Context.IContext context)
		{
			var leftResult = _left.Interpret(context);
			var rightResult = _right.Interpret(context);
			if (leftResult.Type == ValueTypes.Int && rightResult.Type == ValueTypes.Int)
			{
				return new Value(ValueTypes.Bool, TypeHelpers.Convert<int>(leftResult) > TypeHelpers.Convert<int>(rightResult));
			}
			if (leftResult.Type == ValueTypes.String && rightResult.Type == ValueTypes.String)
			{
				return new Value(ValueTypes.Bool, TypeHelpers.Convert<string>(leftResult).Length > TypeHelpers.Convert<string>(rightResult).Length);
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have GREATERTHAN operation.");
		}
	}
}
