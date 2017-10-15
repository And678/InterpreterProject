using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileCompare : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _first;
		private IExpression _second;
		public FileCompare(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileMove accepts {ArgNumber} arguments.");
			_first = expr.First();
			_second = expr[1];
		}
		public Value Interpret(Context.IContext context)
		{
			var first = _first.Interpret(context);
			var second = _second.Interpret(context);
			if (first.Type == ValueTypes.Path && second.Type == ValueTypes.Path)
			{
				var firstStr = TypeHelpers.Convert<string>(first);
				var secondStr = TypeHelpers.Convert<string>(second);
				if (context.FileManager.FileExists(firstStr) && context.FileManager.FileExists(secondStr))
				{
					return new Value(ValueTypes.Bool, context.FileManager.CompareFiles(firstStr, secondStr));
				}
				throw new SyntaxException($"File does not exist.");
			}
			throw new SyntaxException($"FileMove is not defined for {first.Type}, {second.Type}.");
		}
	}
}