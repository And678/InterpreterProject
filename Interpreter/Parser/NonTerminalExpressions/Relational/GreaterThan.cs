﻿using System;
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

		public Value Intrerpret(Context.Context context)
		{
			var leftResult = _left.Intrerpret(context);
			var rightResult = _right.Intrerpret(context);
			if (leftResult.Type == "int" && rightResult.Type == "int")
			{
				return new Value("bool", TypeHelper.Convert<int>(leftResult) > TypeHelper.Convert<int>(rightResult));
			}
			if (leftResult.Type == "string" && rightResult.Type == "string")
			{
				return new Value("bool", TypeHelper.Convert<string>(leftResult).Length > TypeHelper.Convert<string>(rightResult).Length);
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have GREATERTHAN operation.");
		}
	}
}
