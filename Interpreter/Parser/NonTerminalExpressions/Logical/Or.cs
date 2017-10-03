﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Logical
{
	public class Or : IExpression
	{
		private IExpression _left;
		private IExpression _right;
		public Or(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		public Value Intrerpret(Context.Context context)
		{
			var leftResult = _left.Intrerpret(context);
			var rightResult = _right.Intrerpret(context);

			if (leftResult.Type == "bool" && rightResult.Type == "bool")
			{
				return new Value("bool", TypeHelpers.Convert<bool>(leftResult) || TypeHelpers.Convert<bool>(rightResult));
			}

			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have OR operation.");
		}
	}
}
