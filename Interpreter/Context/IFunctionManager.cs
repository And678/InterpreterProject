using System.Collections.Generic;
using Interpreter.Parser;

namespace Interpreter.Context
{
	public interface IFunctionManager
	{
		IExpression CreateFunction(string funcName, IList<IExpression> exprList);
	}
}