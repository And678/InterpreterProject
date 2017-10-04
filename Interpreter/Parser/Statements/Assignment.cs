using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class Assignment : IStatement
	{
		private IExpression _rvalue;
		private string _identifier;
		public Assignment(string identifier, IExpression expression)
		{
			_rvalue = expression;
			_identifier = identifier;
		}	
		public void Execute(Context.Context context)
		{
			var varib = context.LookUpVariable(_identifier);
			var result = _rvalue.Interpret(context);
			if (varib.Type == result.Type)
				varib.SetValue(result.Data);
			else
			{
				throw new SyntaxException($"Cannot assign {result.Type} to {varib.Type}");
			}
		}
	}
}
