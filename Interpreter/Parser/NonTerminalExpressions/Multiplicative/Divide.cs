using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Multiplicative
{
	public class Divide : IExpression
	{
		private IExpression _left;
		private IExpression _right;

		public Divide(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		public Value Interpret(Context.Context context)
		{
			var leftResult = _left.Interpret(context);
			var rightResult = _right.Interpret(context);
			if (leftResult.Type == "int" && rightResult.Type == "int")
			{
				return new Value("int", TypeHelpers.Convert<int>(leftResult) / TypeHelpers.Convert<int>(rightResult));
			}
			throw new SyntaxException($"{leftResult.Type} and {rightResult.Type} don't have DIVIDE operation.");
		}
	}
}