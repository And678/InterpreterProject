using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	class StrToInt : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public StrToInt(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"StrToInt accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Intrerpret(Context.Context context)
		{
			var result = _expression.Intrerpret(context);
			if (result.Type == "string")
			{
				try
				{
					return new Value("int", int.Parse(TypeHelper.Convert<string>(result)));
				}
				catch (FormatException e)
				{
					throw new SyntaxException($"{TypeHelper.Convert<string>(result)} is not a valid integer.");
				}
				catch(OverflowException e)
				{
					throw new SyntaxException($"{TypeHelper.Convert<string>(result)} is not a valid integer.");
				}
			}
			throw new SyntaxException($"StrToInt is not defined for {result.Type}");
		}
	}
}