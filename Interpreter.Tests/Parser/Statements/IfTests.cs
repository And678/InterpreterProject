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
	public class IfTests
	{

		[Test()]
		public void Execute_BoolExprFalse_NotExecutes()
		{
			bool wasCalled = false;

			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.Setup(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Bool, false));
			stmt.Setup(s => s.Execute(It.IsAny<IContext>())).Callback(() => wasCalled = true);

			var subject = new If(expr.Object, stmt.Object);

			subject.Execute(context.Object);

			Assert.That(!wasCalled);
		}

		[Test()]
		public void Execute_BoolExprTrue_Executes()
		{
			bool wasCalled = false;

			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.Setup(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Bool, true));
			stmt.Setup(s => s.Execute(It.IsAny<IContext>())).Callback(() => wasCalled = true);

			var subject = new If(expr.Object, stmt.Object);

			subject.Execute(context.Object);

			Assert.That(wasCalled);
		}

		[Test()]
		public void Execute_NotBoolExpr_ThrowsSyntaxExpression()
		{
			bool wasCalled = false;

			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.Setup(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Int, 5));
			
			var subject = new If(expr.Object, stmt.Object);

			Assert.That(() => subject.Execute(context.Object), 
				Throws.InstanceOf<SyntaxException>());
		}
	}
}