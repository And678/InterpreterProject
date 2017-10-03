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
		public Value Intrerpret(Context.Context context)
		{
			var leftResult = _left.Intrerpret(context);
			var rightResult = _right.Intrerpret(context);
			if (leftResult.Type == "string" && rightResult.Type == "string")
			{
				return AddStrings(leftResult, rightResult);
			}
			if (leftResult.Type == "int" && rightResult.Type == "int")
			{
				return AddInts(leftResult, rightResult);
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have add operation.");
		}

		private Value AddStrings(Value left, Value right)
		{
			return new Value("string", TypeHelpers.Convert<string>(left) + TypeHelpers.Convert<string>(right));
		}

		private Value AddInts(Value left, Value right)
		{
			return new Value("int", TypeHelpers.Convert<int>(left) + TypeHelpers.Convert<int>(right));
		}
	}
}