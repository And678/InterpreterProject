using NUnit.Framework;
using Interpreter.Parser.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;
using Moq;

namespace Interpreter.Parser.Statements.Tests
{
	[TestFixture()]
	public class InvocationTests
	{

		[Test()]
		public void Execute_WithExpression_Executes()
		{
			var exp = new Mock<IExpression>();
			var subject = new Invocation(exp.Object);
			subject.Execute(new Mock<IContext>().Object);
			
			Assert.That(() => exp.Verify(e => e.Interpret(It.IsAny<IContext>())), Throws.Nothing);
		}
	}
}