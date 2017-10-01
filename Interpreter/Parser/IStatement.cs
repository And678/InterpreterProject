using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser
{
	public interface IStatement
	{
		void Execute(Context context);
	}
}
