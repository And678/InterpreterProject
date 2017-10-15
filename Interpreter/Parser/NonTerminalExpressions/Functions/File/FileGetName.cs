using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileGetName : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileGetName(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileGetName accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				string path = TypeHelpers.Convert<string>(result);
				return new Value(ValueTypes.String, context.FileManager.GetFileName(path));
			}
			throw new SyntaxException($"FileGetName is not defined for {result.Type}");
		}
	}
}