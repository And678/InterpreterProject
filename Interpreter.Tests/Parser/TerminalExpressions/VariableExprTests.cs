using NUnit.Framework;
using Interpreter.Parser.TerminalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;
using Moq;

namespace Interpreter.Parser.TerminalExpressions.Tests
{
	[TestFixture()]
	public class VariableExprTests
	{
		[Test()]
		public void GetName_Andriy_ReturnsAndriy()
		{
			string name = "Andriy";
			VariableExpr varExpr = new VariableExpr(name);
			Assert.That(varExpr.GetName(), Is.EqualTo(name));
		}

		[Test()]
		public void Interpret_VariableInContext_ReturnsVariable()
		{
			var mock = new Mock<Context.IContext>();
			var variable = new Value(ValueTypes.Bool, true);
			mock.Setup(context => context.LookUpVariable("andriy")).Returns(variable);
			var subject = new VariableExpr("andriy");

			Assert.That(subject.Interpret(mock.Object), Is.EqualTo(variable));
		}
	}
}