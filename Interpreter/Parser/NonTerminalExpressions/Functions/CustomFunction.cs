using System;
using System.Collections.Generic;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class CustomFunction : IExpression
	{
		
		public CustomFunction(string name, IList<IExpression> expr)
		{
			throw new NotImplementedException();
		}

		public Value Interpret(Context.IContext context)
		{
			throw new NotImplementedException();
		}
		
	}
}