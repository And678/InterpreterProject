using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Multiplicative
{
	public class Mod : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public Mod(IExpression left, IExpression right)
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
				return new Value("int", TypeHelpers.Convert<int>(leftResult) % TypeHelpers.Convert<int>(rightResult));
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have MOD operation.");
		}
	}
}