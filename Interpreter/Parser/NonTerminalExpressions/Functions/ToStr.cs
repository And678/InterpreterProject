﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class ToStr : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public ToStr(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ToStr accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Int)
			{
				return new Value(ValueTypes.String, TypeHelpers.Convert<int>(result).ToString());
			}
			if (result.Type == ValueTypes.Bool)
			{
				return new Value(ValueTypes.String, TypeHelpers.Convert<bool>(result) ? "true" : "false");
			}
			if (result.Type == ValueTypes.Path)
			{
				return new Value(ValueTypes.String, TypeHelpers.Convert<string>(result));
			}
			throw new SyntaxException($"ToStr is not defined for {result.Type}");
		}
	}
}
