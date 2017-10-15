using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileGetSize : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileGetSize(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"GetFileSize accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				string resultPath = TypeHelpers.Convert<string>(result);
				if (context.FileManager.FileExists(resultPath))
				{
					return new Value(ValueTypes.Int , context.FileManager.GetFileSize(resultPath));
				}
				throw new SyntaxException($"{resultPath} does not exist");
			}
			throw new SyntaxException($"GetFileSize is not defined for {result.Type}");
		}
	}
}