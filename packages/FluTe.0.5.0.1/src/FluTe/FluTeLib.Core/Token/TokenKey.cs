using System;
using System.Diagnostics;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   Denotes the unique identifier of a token within a template. Consists of a type and an identifier string.
	/// </summary>
	public struct TokenKey : IEquatable<TokenKey>, IComparable, IComparable<TokenKey>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string identifier;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TokenType type;

		public TokenKey(string identifier, TokenType type)
			: this()
		{
			this.identifier = identifier;
			this.type = type;
		}

		public string Identifier
		{
			get
			{
				return identifier;
			}
		}

		public TokenType Type
		{
			get
			{
				return type;
			}
		}

		public int CompareTo(TokenKey other)
		{
			return type.CompareTo(other.Type)*2 + Identifier.CompareTo(other.Identifier);
		}

		public int CompareTo(object obj)
		{
			if (obj is TokenKey)
			{
				return CompareTo((TokenKey) obj);
			}
			else
			{
				throw new ArgumentException("Cannot compare another type with TokenReference.");
			}
		}

		public bool Equals(TokenKey other)
		{
			return Identifier == other.Identifier && Type == other.Type;
		}

		public override bool Equals(object obj)
		{
			return obj is TokenKey && Equals((TokenKey) obj);
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode() ^ Type.GetHashCode();
		}

		public static TokenKey Injection(string identifier)
		{
			return new TokenKey(identifier, TokenType.Injection);
		}

		public static TokenKey Literal(string identifier)
		{
			return new TokenKey(identifier, TokenType.Literal);
		}

		public override string ToString()
		{
			return string.Format("{0}: '{1}'", Type, Identifier);
		}

		public static bool operator ==(TokenKey left, TokenKey right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TokenKey left, TokenKey right)
		{
			return !(left == right);
		}
	}
}