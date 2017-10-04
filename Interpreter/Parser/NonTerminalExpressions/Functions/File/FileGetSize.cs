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
		public Value Interpret(Context.Context context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				string resultPath = TypeHelpers.Convert<string>(result);
				if (File.Exists(resultPath))
				{
					return new Value(ValueTypes.Int , (int)new FileInfo(resultPath).Length);
				}
				throw new SyntaxException($"{resultPath} does not exist");
			}
			else
			{
				throw new SyntaxException($"GetFileSize is not defined for {result.Type}");
			}
		}
	}
}