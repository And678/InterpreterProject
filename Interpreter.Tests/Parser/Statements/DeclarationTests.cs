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
	public class DeclarationTests
	{
		[Test()]
		public void Execute_AddVariable_AddsToContext()
		{
			bool wasCalled = false;
			var context = new Mock<IContext>();
			context.Setup(c => c.AddVariable(ValueTypes.Bool, "tested")).Callback(() => wasCalled = true);

			var decl = new Declaration(ValueTypes.Bool, "tested");
			decl.Execute(context.Object);
			Assert.That(wasCalled);
		}
	}
}