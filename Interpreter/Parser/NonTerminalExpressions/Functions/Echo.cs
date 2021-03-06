﻿using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Context;
using Interpreter.Lexer;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class Echo : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public Echo(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"Echo accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Int)
			{
				context.AddToOutput(TypeHelpers.Convert<int>(result));
			}
			else if (result.Type == ValueTypes.String)
			{
				context.AddToOutput(TypeHelpers.Convert<string>(result));
			}
			else if (result.Type == ValueTypes.Bool)
			{
				context.AddToOutput(TypeHelpers.Convert<bool>(result));
			}
			else
			{
				throw new SyntaxException($"Echo is not defined for {result.Type}");
			}
			return null;
		}
	}
}