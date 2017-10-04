using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Lexer
{
	public enum TokenType
	{
		EOF,
		Terminator,

		Identifier,
		TypeIdentifier,

		StringLiteral,
		PathLiteral,
		BoolLiteral,
		IntegerLiteral,

		LeftBracket,
		RightBracket,
		LeftSquareBracket,
		RightSquareBracket,
		LeftSquigglyBracket,
		RightSquigglyBracket,

		Comma,

		// Additive
		Plus,
		Minus, 

		// Multiplicative
		Multiply,
		Divide,
		Mod,

		Assign,

		While,
		If,

		//logical
		And,
		Or,

		//Unary
		Not,

		//Relational
		GreaterThan,
		LessThan,
		
		//Equality
		Equality,
		NotEquality
	}
}
