using System;
using System.Collections.Generic;
using System.Text;

namespace WasabiVsSamourai
{
	public class YearMonth : IEquatable<YearMonth>
	{
		public int Year { get; set; }
		public int Month { get; set; }

		public bool Equals(YearMonth other)
		{
			return Year == other.Year && Month == other.Month;
		}
	}
}
