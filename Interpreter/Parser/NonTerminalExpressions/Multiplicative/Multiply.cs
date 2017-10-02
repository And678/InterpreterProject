﻿using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Multiplicative
{
	public class Multiply : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public Multiply(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		public Value Intrerpret(Context.Context context)
		{
			var leftResult = _left.Intrerpret(context);
			var rightResult = _right.Intrerpret(context);
			if (leftResult.Type == "int" && rightResult.Type == "int")
			{
				return new Value("int", TypeHelper.Convert<int>(leftResult) * TypeHelper.Convert<int>(rightResult));
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have MULTIPLY operation.");
		}
	}
}