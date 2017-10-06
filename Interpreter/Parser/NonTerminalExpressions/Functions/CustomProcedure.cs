using System.Collections.Generic;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class CustomProcedure : IExpression
	{
		private IList<IStatement> _stmtList;

		public CustomProcedure(IList<IStatement> stmtList)
		{
			_stmtList = stmtList;
		}
		public Value Interpret(IContext context)
		{
			foreach (var stmt in _stmtList) { 
				stmt.Execute(context);
			}
			return null;
		}
	}
}