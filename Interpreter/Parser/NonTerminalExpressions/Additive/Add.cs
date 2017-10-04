using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Additive
{
	public class Add : IExpression
	{
		private IExpression _left;
		private IExpression _right;
		public Add(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}
		public Value Interpret(Context.Context context)
		{
			var leftResult = _left.Interpret(context);
			var rightResult = _right.Interpret(context);
			if (leftResult.Type == ValueTypes.String && rightResult.Type == ValueTypes.String)
			{
				return AddStrings(leftResult, rightResult);
			}
			if (leftResult.Type == ValueTypes.Int && rightResult.Type == ValueTypes.Int)
			{
				return AddInts(leftResult, rightResult);
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have add operation.");
		}

		private Value AddStrings(Value left, Value right)
		{
			return new Value(ValueTypes.String, TypeHelpers.Convert<string>(left) + TypeHelpers.Convert<string>(right));
		}

		private Value AddInts(Value left, Value right)
		{
			return new Value(ValueTypes.Int, TypeHelpers.Convert<int>(left) + TypeHelpers.Convert<int>(right));
		}
	}
}