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
	public class AssignmentTests
	{
		[Test()]
		public void Execute_AssingNumberToIntValue_AssignmentWorks()
		{
			var iexp = new Mock<IExpression>();
			iexp.Setup(e => e.Interpret(It.IsAny<IContext>())).Returns(new Value(ValueTypes.Int, 42));

			var val = new Value(ValueTypes.Int, 5);
			var context = new Mock<IContext>();
			context.Setup(con => con.LookUpVariable("test")).Returns(val);

			Assignment subject = new Assignment("test", iexp.Object);
			subject.Execute(context.Object);

			Assert.That((int)val.Data, Is.EqualTo(42));
		}

		[Test()]
		public void Execute_AssingNumberToStringValue_ThrowsSyntaxException()
		{
			var iexp = new Mock<IExpression>();
			iexp.Setup(e => e.Interpret(It.IsAny<IContext>())).Returns(new Value(ValueTypes.Int, 42));

			var val = new Value(ValueTypes.String, "something");
			var context = new Mock<IContext>();
			context.Setup(con => con.LookUpVariable("test")).Returns(val);

			Assignment subject = new Assignment("test", iexp.Object);
			

			Assert.That(() => subject.Execute(context.Object), Throws.InstanceOf<SyntaxException>());
		}
	}
}