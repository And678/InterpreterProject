using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class If : IStatement
	{
		private IExpression _expression;
		private IStatement _statement;

		public If(IExpression expression, IStatement statement)
		{
			_expression = expression;
			_statement = statement;
		}
		public void Execute(Context.Context context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == "bool")
			{
				if (TypeHelpers.Convert<bool>(result))
				{
					_statement.Execute(context);
				}
			}
			else
			{
				throw new SyntaxException($"If expressions are required to be bool, not {result.Type}");
			}

		}
	}
}
