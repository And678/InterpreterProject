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
	public class ArrayAccessTests
	{

		[Test()]
		public void Interpret_ExistingArray_ReturnValue()
		{
			var context = new Mock<IContext>();

			var value = new Value(ValueTypes.Int, 42);
			var list = new List<Value>() {null, value};

			var exprIndex = new Mock<IExpression>();
			exprIndex.Setup(expression => expression.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Int, 1));

			var exprArr = new Mock<IExpression>();
			exprArr.Setup(expression => expression.Interpret(It.IsAny<IContext>()))
				.Returns(new Value(ValueTypes.Array, list));
			ArrayAccess subject = new ArrayAccess(exprIndex.Object, exprArr.Object);

			Assert.That(subject.Interpret(context.Object), Is.EqualTo(value));

		}
	}
}