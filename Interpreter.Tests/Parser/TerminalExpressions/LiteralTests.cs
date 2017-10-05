using NUnit.Framework;
using Interpreter.Parser.TerminalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;
using Interpreter.Lexer;
using Moq;

namespace Interpreter.Parser.TerminalExpressions.Tests
{
	[TestFixture()]
	public class LiteralTests
	{

		[Test()]
		public void Interpret_BoolLiteralTrue_ReturnBoolValue()
		{
			var subject = new Literal(TokenType.BoolLiteral, "true");
			var mock = new Mock<IContext>();
			var expected = new Value(ValueTypes.Bool, true);
			Assert.That(subject.Interpret(mock.Object).Type, Is.EqualTo(expected.Type));
			Assert.That((bool)subject.Interpret(mock.Object).Data, Is.EqualTo((bool)expected.Data));
		}

		[Test()]
		public void Interpret_StringLiteral_ReturnStringValue()
		{
			var subject = new Literal(TokenType.StringLiteral, "testT");
			var mock = new Mock<IContext>();
			var expected = new Value(ValueTypes.String, "testT");
			Assert.That(subject.Interpret(mock.Object).Type, Is.EqualTo(expected.Type));
			Assert.That((string)subject.Interpret(mock.Object).Data, Is.EqualTo((string)expected.Data));
		}

		[Test()]
		public void Interpret_IntLiteral_ReturnIntValue()
		{
			var subject = new Literal(TokenType.IntegerLiteral, "-15");
			var mock = new Mock<IContext>();
			var expected = new Value(ValueTypes.Int, -15);
			Assert.That(subject.Interpret(mock.Object).Type, Is.EqualTo(expected.Type));
			Assert.That((int)subject.Interpret(mock.Object).Data, Is.EqualTo((int)expected.Data));
		}
		[Test()]
		public void Interpret_PathLiteral_ReturnPathValue()
		{
			var subject = new Literal(TokenType.PathLiteral, "file.txt");
			var mock = new Mock<IContext>();
			mock.Setup(context => context.GetCurrentPath()).Returns(@"c:\folder");

			var expected = new Value(ValueTypes.Path, @"c:\folder\file.txt");
			Assert.That(subject.Interpret(mock.Object).Type, Is.EqualTo(expected.Type));
			Assert.That((string)subject.Interpret(mock.Object).Data, Is.EqualTo((string)expected.Data));
		}
		[Test()]
		public void Interpret_NotLiteral_ThrowsSyntaxException()
		{
			var subject = new Literal(TokenType.Assign, "");
			var mock = new Mock<IContext>();
			Assert.That(() => subject.Interpret(mock.Object), Throws.InstanceOf<SyntaxException>());
		}
	}
}