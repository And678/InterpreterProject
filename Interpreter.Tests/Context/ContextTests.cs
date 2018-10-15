using NUnit.Framework;
using Interpreter.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Parser;
using Moq;

namespace Interpreter.Context.Tests
{
	[TestFixture()]
	public class ContextTests
	{
		[Test()]
		public void GetCurrentFile_InitializedInConstructor_ReturnsCorrectly()
		{
			var file = @"c:\folder\file.txt";
			var context = new Context(new Mock<IFunctionManager>().Object, new Mock<IInputManager>().Object, new Mock<IFileManager>().Object, file);

			Assert.That(context.GetCurrentFile(), Is.EqualTo(file));
		}

		[Test()]
		public void GetCurrentPath_InitializedInConstructor_ReturnsCorrectly()
		{
			var file = @"c:\folder\file.txt";
			var context = new Context(new Mock<IFunctionManager>().Object, new Mock<IInputManager>().Object, new Mock<IFileManager>().Object, file);

			Assert.That(context.GetCurrentPath(), Is.EqualTo(@"c:\folder\").Or.EqualTo(@"c:\folder"));
		}

		[Test()]
		public void LookUpVariable_DeclaredVariable_ReturnsVariable()
		{
			var context = new Context(new Mock<IFunctionManager>().Object, new Mock<IInputManager>().Object, new Mock<IFileManager>().Object, "C:\\file.c");
			var expected = new Value(ValueTypes.Int, 5);

			context.AddVariable(ValueTypes.Int, "test", 5);
			Assert.That(context.LookUpVariable("test").Type, Is.EqualTo(expected.Type));
			Assert.That((int)context.LookUpVariable("test").Data, Is.EqualTo((int)expected.Data));
		}
		[Test()]
		public void LookUpVariable_UnDeclaredVariable_ThrowsSyntaxException()
		{
			var context = new Context(new Mock<IFunctionManager>().Object, new Mock<IInputManager>().Object, new Mock<IFileManager>().Object, "C:\\file.c");
			Assert.That(() => context.LookUpVariable("test"), Throws.InstanceOf<SyntaxException>());
		}

		[Test()]
		public void AddVariable_AddTwice_ThrowsSyntaxExpression()
		{
			var context = new Context(new Mock<IFunctionManager>().Object, new Mock<IInputManager>().Object, new Mock<IFileManager>().Object, "C:\\file.c");
			context.AddVariable(ValueTypes.Int, "test", 5);
			Assert.That(() => context.AddVariable(ValueTypes.Int, "test", 5), Throws.InstanceOf<SyntaxException>());
		}

		[Test()]
		public void AddToOutput_SomeString_CallsIInputManager()
		{
			var mock = new Mock<IInputManager>();
			string start = "TestTestTest.";
			string result = String.Empty;
			mock.Setup(manager => manager.PrintLine(It.IsAny<string>())).Callback<string>((str) => result = str);
			var context = new Context(new Mock<IFunctionManager>().Object, mock.Object, new Mock<IFileManager>().Object, "C:\\file.c");

			context.AddToOutput(start);

			Assert.That(result, Is.EqualTo(start));
		}


		[Test()]
		public void GetInput_CallsIInputManager()
		{
			var mock = new Mock<IInputManager>();
			bool WasCalled = false;
			mock.Setup(manager => manager.GetLineFromUser()).Callback(() => WasCalled = true);
			var context = new Context(new Mock<IFunctionManager>().Object, mock.Object, new Mock<IFileManager>().Object, "C:\\file.c");

			context.GetInput();

			Assert.That(WasCalled);
		}
	}
}