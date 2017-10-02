using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class Invocation : IStatement
	{
		private IExpression _functionExpression;

		public Invocation(IExpression expr)
		{
			_functionExpression = expr;
		}
		public void Execute(Context.Context context)
		{
			_functionExpression.Intrerpret(context);
		}
	}
}
