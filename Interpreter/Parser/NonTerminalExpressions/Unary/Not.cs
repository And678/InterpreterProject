using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Unary
{
	public class Not : IExpression
	{
		private IExpression _expr;

		public Not(IExpression expr)
		{
			_expr = expr;
		}

		public Value Intrerpret(Context.Context context)
		{
			var result = _expr.Intrerpret(context);

			if (result.Type == "bool")
			{
				return new Value("bool", !TypeHelper.Convert<bool>(result));
			}
			throw new SyntaxException($"{result.Type}don't have NOT operation.");
		}
	}
}