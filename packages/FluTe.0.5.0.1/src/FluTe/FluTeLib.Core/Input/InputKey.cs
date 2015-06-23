using System;
using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   A unique key that defines an input within a template. Consists of an 'input type' and an 'input label'.
	/// </summary>
	public struct InputKey : IEquatable<InputKey>, IComparable<InputKey>, IComparable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string label;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly InputType type;

		public InputKey(string label, InputType scope)
			: this()
		{
			this.label = label;
			type = scope;
		}

		public string Label
		{
			get
			{
				return label;
			}
		}

		public InputType Type
		{
			get
			{
				return type;
			}
		}

		public int CompareTo(InputKey other)
		{
			return Type.CompareTo(other.Type)*2 + Label.CompareTo(other.Label);
		}

		public int CompareTo(object obj)
		{
			if (obj is InputKey)
			{
				return CompareTo((InputKey) obj);
			}
			throw new InvalidOperationException("Cannot compare an object of a different type with InputKey.");
		}

		public bool Equals(InputKey other)
		{
			return Type.Equals(other.Type) && Label.Equals(other.Label);
		}

		public override bool Equals(object obj)
		{
			return obj is InputKey && Equals((InputKey) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return label.GetHashCode() ^ type.GetHashCode();
			}
		}

		public static InputKey Instance(string label)
		{
			return new InputKey(label, InputType.Instance);
		}

		public static InputKey Static(string label)
		{
			return new InputKey(label, InputType.Static);
		}

		public override string ToString()
		{
			return Type == InputType.Static ? "$ " + Label : Label;
		}

		public static bool operator ==(InputKey left, InputKey right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(InputKey left, InputKey right)
		{
			return !(left == right);
		}
	}
}