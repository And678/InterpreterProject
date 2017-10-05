
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Parser
{
	public interface IExpression
	{
		Value Interpret(Context.IContext context);
	}
}
