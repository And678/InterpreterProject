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
		public void Execute(Context context)
		{
			throw new NotImplementedException();
		}
	}
}
