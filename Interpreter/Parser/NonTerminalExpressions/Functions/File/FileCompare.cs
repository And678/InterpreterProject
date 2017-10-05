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
				if (File.Exists(firstStr) && File.Exists(secondStr))
				{
					return new Value(ValueTypes.Bool, FileEquals(firstStr, secondStr));
				}
				throw new SyntaxException($"File does not exist.");
			}
			throw new SyntaxException($"FileMove is not defined for {first.Type}, {second.Type}.");
		}
		private bool FileEquals(string path1, string path2)
		{
			byte[] file1 = File.ReadAllBytes(path1);
			byte[] file2 = File.ReadAllBytes(path2);
			if (file1.Length == file2.Length)
			{
				for (int i = 0; i < file1.Length; i++)
				{
					if (file1[i] != file2[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}