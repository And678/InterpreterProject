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
		FileLiteral,
		BoolLiteral,
		IntegerLiteral,
		NullLiteral,

		LeftBracket,
		RightBracket,
		LeftSquareBracket,
		RightSquareBracket,
		
		Plus,
		Minus,
		Multiply,
		Divide,
		Mod,
		Assign,

		And,
		Or,
		Not,
		Equality,
		NotEquality
	}
}
