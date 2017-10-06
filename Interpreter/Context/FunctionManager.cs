using System.Collections.Generic;
using Interpreter.Parser;
using Interpreter.Parser.NonTerminalExpressions.Functions;
using Interpreter.Parser.NonTerminalExpressions.Functions.Array;

namespace Interpreter.Context
{
	public class FunctionManager : IFunctionManager
	{
		private Dictionary<string, CustomProcedure> _expr;

		public FunctionManager()
		{
			_expr = new Dictionary<string, CustomProcedure>();
		}
		public IExpression LookUpCustomFunction(string funcName, IList<IExpression> exprList)
		{
			return _expr[funcName];
		}

		public void AddNewFunction(string funcName, IList<IStatement> stmtList)
		{
			_expr.Add(funcName, new CustomProcedure(stmtList));
		}


	}
}