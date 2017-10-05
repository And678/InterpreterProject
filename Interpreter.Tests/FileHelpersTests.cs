using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tests
{
	[TestFixture]
	public class FileHelpersTests
	{
		[Test]
		public void IsValidPath_GoodPath_returnsTrue()
		{
			Assert.That(Interpreter.FileHelpers.IsValidPath(@"C:\folder\file.txt"));
		}

		[Test]
		public void IsValidPath_BadPath_returnsFalse()
		{
			Assert.That(Interpreter.FileHelpers.IsValidPath(@"C-:\*fЇo@#$%^&lder\file.txt"));
		}

		[Test]
		public void BuildAbsolute_withLines_BuildsCorrectly()
		{
			string wdir = "abc\\";
			string rel = "\\file.txt";

			string result = Interpreter.FileHelpers.BuildAbsolute(wdir, rel);
			Assert.That(result, Is.EqualTo("abc\\file.txt"));
		}

		[Test]
		public void BuildAbsolute_withoutLines_BuildsCorrectly()
		{
			string wdir = "abc";
			string rel = "file.txt";

			string result = Interpreter.FileHelpers.BuildAbsolute(wdir, rel);
			Assert.That(result, Is.EqualTo("abc\\file.txt"));
		}
	}
}
