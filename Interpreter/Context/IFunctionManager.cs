using System.Collections.Generic;
using Interpreter.Parser;

namespace Interpreter.Context
{
	public interface IFunctionManager
	{
		IExpression LookUpCustomFunction(string funcName, IList<IExpression> exprList);
		void AddNewFunction(string funcName, IList<IStatement> stmtList);
	}
}