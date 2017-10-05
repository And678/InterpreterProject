using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileGetListInDir : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileGetListInDir(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileGetListInDir accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var expression = _expression.Interpret(context);
			if (expression.Type == ValueTypes.Path)
			{
				string path = TypeHelpers.Convert<string>(expression);
				if (Directory.Exists(path))
				{
					return new Value(ValueTypes.Array, Directory.GetFiles(path).ToList());
				}
			}
			throw new SyntaxException($"FileGetListInDir is not defined for {expression.Type}.");
		}
	}
}