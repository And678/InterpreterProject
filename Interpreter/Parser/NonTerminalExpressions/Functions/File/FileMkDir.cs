using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileMkDir : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileMkDir(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileMkDir accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				var path = Directory.CreateDirectory(TypeHelpers.Convert<string>(result));
				return new Value(ValueTypes.Path, path.FullName);
			}
			throw new SyntaxException($"FileMkDir is not defined for {result.Type}");
		}
	}
}