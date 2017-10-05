using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileExists : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileExists(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileDelete accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				string path = TypeHelpers.Convert<string>(result);
				return new Value(ValueTypes.Bool, File.Exists(path));
			}
			throw new SyntaxException($"FileDelete is not defined for {result.Type}");
		}
	}
}