using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Context;
using Interpreter.Parser;
using Moq;
using NUnit.Framework.Constraints;

namespace Interpreter.Lexer.Tests
{
	[TestFixture]
	public class LexerTests
	{
		public class TokenComparer : IEqualityComparer
		{
			public new bool Equals(object x, object y)
			{
				return ((Token) x).Type == ((Token) y).Type && ((Token) x).Value == ((Token) y).Value;
			}

			public int GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	   [Test]
		public void GetNextToken_Empty_AlwaysReturnsEOF()
		{
			var subject = new Lexer("");
			for (int i = 0; i < 10; i++)
			{
				var result = subject.GetNextToken();
				Assert.That(result.Type, Is.EqualTo(TokenType.EOF));
			}
		}
		[Test]
		public void GetNextToken_IntMinusIdentifier_ParsedCorrectly()
		{
			var subject = new Lexer("5-test");
			Token[] result =
			{
				subject.GetNextToken(),
				subject.GetNextToken(),
				subject.GetNextToken()
			};
			Token[] expected =
			{
				new Token(TokenType.IntegerLiteral, "5"),
				new Token(TokenType.Minus),
				new Token(TokenType.Identifier, "test")

			};

			Assert.That(result, Is.EqualTo(expected).Using(new TokenComparer()));
		}
		[Test]
		public void GetNextToken_ExclamationMarks_ParsedCorrectly()
		{
			var subject = new Lexer("!!!");
			Token[] result =
			{
				subject.GetNextToken(),
				subject.GetNextToken(),
				subject.GetNextToken()
			};
			Token[] expected =
			{
				new Token(TokenType.Not),
				new Token(TokenType.Not),
				new Token(TokenType.Not)

			};

			Assert.That(result, Is.EqualTo(expected).Using(new TokenComparer()));
		}

		public void GetNextToken_UnknownSymbol_ThrowsException()
		{
			var subject = new Lexer("$");

			Assert.That(subject.GetNextToken, Throws.InstanceOf<SyntaxException>());
		}
	}
}
