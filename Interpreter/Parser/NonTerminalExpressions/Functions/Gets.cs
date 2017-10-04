using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class Gets : IExpression
	{
		private const int ArgNumber = 0;
		public Gets(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"Gets accepts {ArgNumber} arguments.");
		}
		public Value Interpret(Context.Context context)
		{
			return new Value(ValueTypes.String, context.GetInput());
		}
	}
}