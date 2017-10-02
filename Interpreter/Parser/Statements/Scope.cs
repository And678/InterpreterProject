using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class Scope : IStatement
	{
		private IList<IStatement> _statements;

		public Scope(IList<IStatement> statements)
		{
			_statements = statements;
		}
		public void Execute(Context.Context context)
		{
			foreach (var stmt in _statements)
			{
				stmt.Execute(context);
			}
		}
	}
}
