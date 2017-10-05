using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileCreate : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public FileCreate(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileCreate accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Interpret(Context.IContext context)
		{
			var result = _expression.Interpret(context);
			if (result.Type == ValueTypes.Path)
			{
				File.Create(TypeHelpers.Convert<string>(result));
			}
			throw new SyntaxException($"FileCreate is not defined for {result.Type}");
		}
	}
}