using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Logical
{
	public class And : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public And(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		public Value Interpret(Context.Context context)
		{
			var leftResult = _left.Interpret(context);
			var rightResult = _right.Interpret(context);

			if (leftResult.Type == ValueTypes.Bool && rightResult.Type == ValueTypes.Bool)
			{
				return new Value(ValueTypes.Bool, TypeHelpers.Convert<bool>(leftResult) && TypeHelpers.Convert<bool>(rightResult));
			}

			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have AND operation.");
		}
	}
}