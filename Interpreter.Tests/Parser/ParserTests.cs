using NUnit.Framework;
using Interpreter.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Lexer;
using Interpreter.Parser.Statements;
using Moq;

namespace Interpreter.Parser.Tests
{
	[TestFixture()]
	public class ParserTests
	{
		[Test()]
		public void BuildStatement_DeclarationTokens_ReturnsDeclaration()
		{
			var mock = new Mock<Lexer.ILexer>();
			mock.SetupSequence(lexer => lexer.GetNextToken())
				.Returns(new Token(TokenType.IntIdentifier))
				.Returns(new Token(TokenType.Identifier, "hi"))
				.Returns(new Token(TokenType.Terminator))
				.Returns(new Token(TokenType.EOF));

			Parser parser = new Parser(mock.Object);

			Assert.That(parser.BuildStatement(), Is.InstanceOf<Declaration>());
		}

		[Test()]
		public void BuildStatement_RandomTokens_ThrowsSyntaxException()
		{
			var mock = new Mock<Lexer.ILexer>();
			mock.SetupSequence(lexer => lexer.GetNextToken())
				.Returns(new Token(TokenType.RightBracket))
				.Returns(new Token(TokenType.StringLiteral, "hi"))
				.Returns(new Token(TokenType.Assign))
				.Returns(new Token(TokenType.Terminator));

			Parser parser = new Parser(mock.Object);

			Assert.That(() => parser.BuildStatement(), Throws.InstanceOf<SyntaxException>());
		}

		[Test()]
		public void BuildStatement_DeclarationTokensNoTerminator_ThrowsSyntaxException()
		{
			var mock = new Mock<Lexer.ILexer>();
			mock.SetupSequence(lexer => lexer.GetNextToken())
				.Returns(new Token(TokenType.IntIdentifier))
				.Returns(new Token(TokenType.Identifier, "hi"))
				.Returns(new Token(TokenType.EOF));

			Parser parser = new Parser(mock.Object);

			Assert.That(() => parser.BuildStatement(), Throws.InstanceOf<SyntaxException>());
		}
	}
}