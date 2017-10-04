using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class ToBool : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public ToBool(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ToBool accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.Context context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.String)
			{
				return new Value(ValueTypes.Bool, 
					TypeHelpers.Convert<string>(result) == "true");
			}
			if (result.Type == ValueTypes.Int)
			{
				return new Value(ValueTypes.Bool, 
					TypeHelpers.Convert<int>(result) != 0);
			}
			throw new SyntaxException($"ToBool is not defined for {result.Type}");
		}
	}
}