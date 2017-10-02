using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class While : IStatement
	{
		private IExpression _expression;
		private IStatement _statement;

		public While(IExpression expression, IStatement statement)
		{
			_expression = expression;
			_statement = statement;
		}
		public void Execute(Context.Context context)
		{
			var result = _expression.Intrerpret(context);
			if (result.Type == "bool")
			{
				while (TypeHelper.Convert<bool>(result))
				{
					_statement.Execute(context);
					result = _expression.Intrerpret(context);
				}
			}
			else
			{
				throw new SyntaxException($"If expressions are required to be bool, not {result.Type}");
			}

		}
	}
}
