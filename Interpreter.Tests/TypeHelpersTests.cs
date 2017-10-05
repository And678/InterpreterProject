using NUnit.Framework;
using Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;

namespace Interpreter.Tests
{
	[TestFixture]
	public class TypeHelpersTests
	{
		[Test]
		public void Convert_IntValueFive_ReturnsFive()
		{
			var arranged = new Value(ValueTypes.Int, 5);
			var result = TypeHelpers.Convert<int>(arranged);
			Assert.That(result, Is.EqualTo(5));
		}

		[Test]
		public void Convert_StringValueAndriy_ReturnsAndriy()
		{
			var arranged = new Value(ValueTypes.String, "Andriy");
			var result = TypeHelpers.Convert<string>(arranged);
			Assert.That(result, Is.EqualTo("Andriy"));
		}

		[Test]
		public void Convert_PathValueAndriy_ReturnsAndriy()
		{
			var arranged = new Value(ValueTypes.Path, "Andriy");
			var result = TypeHelpers.Convert<string>(arranged);
			Assert.That(result, Is.EqualTo("Andriy"));
		}

		[Test]
		public void Convert_IntToString_ThrowsInvalidCastException()
		{
			var arranged = new Value(ValueTypes.Int, 5);
			Assert.That(() => TypeHelpers.Convert<string>(arranged), Throws.TypeOf<InvalidCastException>());
		}
	}
}