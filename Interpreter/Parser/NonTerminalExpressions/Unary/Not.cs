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

		public Value Interpret(Context.IContext context)
		{
			var result = _expr.Interpret(context);

			if (result.Type == ValueTypes.Bool)
			{
				return new Value(ValueTypes.Bool, !TypeHelpers.Convert<bool>(result));
			}
			throw new SyntaxException($"{result.Type}don't have NOT operation.");
		}
	}
}