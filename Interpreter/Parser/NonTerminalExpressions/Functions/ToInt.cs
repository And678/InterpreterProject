using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	class ToInt : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public ToInt(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ToInt accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Intrerpret(Context.Context context)
		{
			var result = _expression.Intrerpret(context);
			if (result.Type == "string")
			{
				try
				{
					return new Value("int", int.Parse(TypeHelpers.Convert<string>(result)));
				}
				catch (FormatException e)
				{
					throw new SyntaxException($"{TypeHelpers.Convert<string>(result)} is not a valid integer.");
				}
				catch(OverflowException e)
				{
					throw new SyntaxException($"{TypeHelpers.Convert<string>(result)} is not a valid integer.");
				}
			}
			if (result.Type == "bool")
			{
				if (TypeHelpers.Convert<bool>(result))
				{
					return new Value("int", 1);
				}
				return new Value("int", 0);
			}
			throw new SyntaxException($"ToInt is not defined for {result.Type}");
		}
	}
}