using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WasabiVsSamourai
{
	public class YearMonth : IEquatable<YearMonth>
	{
		public int Year { get; set; }
		public int Month { get; set; }

		public override string ToString()
		{
			return $"{Year}.{Month}";
		}

		#region Equality

		public override bool Equals(object obj) => obj is YearMonth pubKey && this == pubKey;

		public bool Equals(YearMonth other) => this == other;

		public override int GetHashCode() => Year.GetHashCode() ^ Month.GetHashCode();

		public static bool operator ==(YearMonth x, YearMonth y) => x.Year == y.Year && x.Month == y.Month;

		public static bool operator !=(YearMonth x, YearMonth y) => !(x == y);

		#endregion Equality
	}
}
