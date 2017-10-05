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
	public class ScopeTests
	{
		[Test()]
		public void Execute_HasStatements_ExecutesThem()
		{
			MockRepository mocks = new MockRepository(MockBehavior.Strict);
			Mock<IStatement>[] arrayMocks =
			{
				mocks.Create<IStatement>(),
				mocks.Create<IStatement>(),
				mocks.Create<IStatement>(),
				mocks.Create<IStatement>()
			};
			var list = new List<IStatement>();
			foreach (var arrayMock in arrayMocks)
			{
				arrayMock.Setup(m => m.Execute(It.IsAny<IContext>()));
				list.Add(arrayMock.Object);
			}

			var scp = new Scope(list);
			scp.Execute(new Mock<IContext>().Object);
			Assert.That(() => mocks.VerifyAll(), Throws.Nothing);

		}
	}
}