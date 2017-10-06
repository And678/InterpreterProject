using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class SubString : IExpression
	{
		private const int ArgNumber = 3;
		private IExpression _string;
		private IExpression _start;
		private IExpression _end;

		public SubString(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"SubString accepts {ArgNumber} arguments.");
			_string = expr.First();
			_start = expr[1];
			_end = expr[2];
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _string.Interpret(context);
			var start = _start.Interpret(context);
			var end = _end.Interpret(context);
			if (result.Type == ValueTypes.String && 
				start.Type == ValueTypes.Int &&
				end.Type == ValueTypes.Int)
			{
				string str = TypeHelpers.Convert<string>(result);
				int startInt = TypeHelpers.Convert<int>(start);
				int endInt = TypeHelpers.Convert<int>(end);
				if (startInt > str.Length)
				{
					throw new SyntaxException($"{startInt} is not within string bounds.");
				}
				if (endInt > str.Length)
				{
					throw new SyntaxException($"{startInt} + {endInt} is not within string bounds.");
				}
				if (endInt < 0)
				{
					endInt = str.Length - startInt;
				}
				return new Value(ValueTypes.String, 
					str.Substring(startInt, endInt));
			}
			throw new SyntaxException($"SubString is not defined for {result.Type}, {start.Type}, {end.Type}.");
		}
	}
}