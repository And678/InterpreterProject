using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	class IntToStr : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public IntToStr(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"IntToStr accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Intrerpret(Context.Context context)
		{
			var result = _expression.Intrerpret(context);
			if (result.Type == "int")
			{
				return new Value("string", TypeHelper.Convert<int>(result).ToString());
			}
			throw new SyntaxException($"IntToStr is not defined for {result.Type}");
		}
	}
}
