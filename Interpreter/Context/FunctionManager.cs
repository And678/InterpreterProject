using System.Collections.Generic;
using Interpreter.Parser;
using Interpreter.Parser.NonTerminalExpressions.Functions;
using Interpreter.Parser.NonTerminalExpressions.Functions.Array;

namespace Interpreter.Context
{
	public class FunctionManager : IFunctionManager
	{
		public IExpression CreateFunction(string funcName, IList<IExpression> exprList)
		{
			return LookUpCustomFunction(funcName, exprList);
		}

		private IExpression LookUpCustomFunction(string funcName, IList<IExpression> exprList)
		{
			return null;
		}

		
	}
}