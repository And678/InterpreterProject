using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Lexer.Tests
{
	[TestFixture]
	public class Lexer
	{
		/*
		[Test]
		public void GetNextToken_()
		{

		}
		*/
		[Test]
		public void GetNextToken_Empty_ReturnsEOF()
		{
			var subject = new Lexer();
			Assert.Pass();
		}
	}
}
