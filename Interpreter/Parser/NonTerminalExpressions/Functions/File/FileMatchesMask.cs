using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class FileMatchesMask : IExpression
	{
		private const int ArgNumber = 2;
		private IExpression _file;
		private IExpression _mask;
		public FileMatchesMask(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"FileMatchesMask accepts {ArgNumber} arguments.");
			_file = expr.First();
			_mask = expr[1];
		}
		public Value Interpret(Context.IContext context)
		{
			var file = _file.Interpret(context);
			var mask = _mask.Interpret(context);
			if (file.Type == ValueTypes.Path && mask.Type == ValueTypes.String)
			{
				string fileStr = TypeHelpers.Convert<string>(file);
				string maskStr = TypeHelpers.Convert<string>(mask);
				return new Value(ValueTypes.Bool, FitsMask(fileStr, maskStr));
			}
			throw new SyntaxException($"FileMatchesMask is not defined for {file.Type} and {mask.Type}");
		}

		private bool FitsMask(string fileName, string fileMask)
		{
			string pattern =
				'^' +
				Regex.Escape(fileMask.Replace(".", "__DOT__")
						.Replace("*", "__STAR__")
						.Replace("?", "__QM__"))
					.Replace("__DOT__", "[.]")
					.Replace("__STAR__", ".*")
					.Replace("__QM__", ".")
				+ '$';
			return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(fileName);
		}
	}
}