using System;
using Interpreter.Context;

namespace Interpreter.Parser.TerminalExpressions
{
	public class VariableExpr : IExpression
	{
		private string _name;

		public VariableExpr(string name)
		{
			_name = name;
		}

		public string GetName()
		{
			return _name;
		}
		public Value Interpret(Context.Context context)
		{
			var varib = context.LookUpVariable(_name);
			return varib;
		}
	}
}
