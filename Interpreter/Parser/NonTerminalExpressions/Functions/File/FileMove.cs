using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileMove : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _from;
		private IExpression _to;
		public FileMove(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileMove accepts {ArgNumber} arguments.");
			_from = expr.First();
			_to = expr[1];
		}
		public Value Interpret(Context.IContext context)
		{
			var from = _from.Interpret(context);
			var to = _to.Interpret(context);
			if (from.Type == ValueTypes.Path && to.Type == ValueTypes.Path)
			{
				string file = TypeHelpers.Convert<string>(from);
				if (context.FileManager.FileExists(file))
				{
					context.FileManager.MoveFile(file, TypeHelpers.Convert<string>(to));
					return null;
				}
				throw new SyntaxException($"{file} does not exist.");
				
			}
			throw new SyntaxException($"FileMove is not defined for {from.Type}, {to.Type}.");
		}
	}
}