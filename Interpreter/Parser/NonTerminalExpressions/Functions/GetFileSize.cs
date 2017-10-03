﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace Interpreter.Parser.NonTerminalExpressions.Functions
{
	public class GetFileSize : IExpression
	{
		private const int ArgNumber = 1;
		private IExpression _expression;
		public GetFileSize(IList<IExpression> expr)
		{
			if (expr.Count != ArgNumber)
				throw new SyntaxException($"GetFileSize accepts {ArgNumber} arguments.");
			_expression = expr.First();
		}
		public Value Intrerpret(Context.Context context)
		{
			var result = _expression.Intrerpret(context);
			if (result.Type == "path")
			{
				string resultPath = TypeHelpers.Convert<string>(result);
				if (File.Exists(resultPath))
				{
					return new Value("int" , (int)new FileInfo(resultPath).Length);
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