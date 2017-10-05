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
	public class WhileTests
	{

		[Test()]
		public void Execute_TrueExpr_ExecutesMultipleTimes()
		{
			int wasCalled = 0;
			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.SetupSequence(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Bool, true))
				.Returns(new Value(ValueTypes.Bool, true))
				.Returns(new Value(ValueTypes.Bool, true))
				.Returns(new Value(ValueTypes.Bool, true))
				.Returns(new Value(ValueTypes.Bool, true))
				.Returns(new Value(ValueTypes.Bool, false));
			stmt.Setup(s => s.Execute(It.IsAny<IContext>())).Callback(() => wasCalled++);

			var subject = new While(expr.Object, stmt.Object);

			subject.Execute(context.Object);

			Assert.That(wasCalled, Is.EqualTo(5));
		}

		[Test()]
		public void Execute_FalseExpr_ExecutesZeroTimes()
		{
			int wasCalled = 0;
			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.Setup(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Bool, false));
			stmt.Setup(s => s.Execute(It.IsAny<IContext>())).Callback(() => wasCalled++);

			var subject = new While(expr.Object, stmt.Object);

			subject.Execute(context.Object);

			Assert.That(wasCalled, Is.EqualTo(0));
		}
		
	

		[Test()]
		public void Execute_NotBoolExpr_ThrowsSyntaxExpression()
		{

			var context = new Mock<IContext>();
			var expr = new Mock<IExpression>();
			var stmt = new Mock<IStatement>();

			expr.Setup(e => e.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Int, 12));
			
			var subject = new While(expr.Object, stmt.Object);

			Assert.That(() => subject.Execute(context.Object), 
				Throws.InstanceOf<SyntaxException>());
		} 
		
	}
}