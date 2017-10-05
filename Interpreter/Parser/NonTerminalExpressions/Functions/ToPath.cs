using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class ToPath : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public ToPath(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ToPath accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.String)
			{
				return new Value(ValueTypes.Path, TypeHelpers.Convert<string>(result));
			}
			throw new SyntaxException($"ToPath is not defined for {result.Type}");
		}
	}
}