﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser.Statements
{
	public class Declaration : IStatement
	{
		private string _type;
		private string _identifier;

		public Declaration(string type, string identifier)
		{
			_type = type;
			_identifier = identifier;
		}
		public void Execute(Context context)
		{
			throw new NotImplementedException();
		}
	}
}