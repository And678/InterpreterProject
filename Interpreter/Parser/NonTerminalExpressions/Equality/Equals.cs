using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Equality
{
	public class Equals : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public Equals(IExpression left, IExpression right)
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
				return new Value("bool", TypeHelpers.Convert<int>(leftResult) == TypeHelpers.Convert<int>(rightResult));
			}

			if (leftResult.Type == "string" && rightResult.Type == "string")
			{
				return new Value("bool", TypeHelpers.Convert<string>(leftResult) == TypeHelpers.Convert<string>(rightResult));
			}

			if (leftResult.Type == "path" && rightResult.Type == "path")
			{
				return new Value("bool", TypeHelpers.Convert<string>(leftResult) == TypeHelpers.Convert<string>(rightResult));
			}

			if (leftResult.Type == "bool" && rightResult.Type == "bool")
			{
				return new Value("bool", TypeHelpers.Convert<bool>(leftResult) == TypeHelpers.Convert<bool>(rightResult));
			}

			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have EQUALS operation.");
		}
	}
}
