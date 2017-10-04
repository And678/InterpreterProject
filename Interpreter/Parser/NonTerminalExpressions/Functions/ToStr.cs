using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	class ToStr : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public ToStr(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"ToStr accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.Context context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == "int")
			{
				return new Value("string", TypeHelpers.Convert<int>(result).ToString());
			}
			if (result.Type == "bool")
			{
				return new Value("string", TypeHelpers.Convert<bool>(result) ? "true" : "false");
			}
			if (result.Type == "path")
			{
				return new Value("string", TypeHelpers.Convert<string>(result));
			}
			throw new SyntaxException($"ToStr is not defined for {result.Type}");
		}
	}
}
