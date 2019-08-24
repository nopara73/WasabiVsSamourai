using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace WasabiVsSamourai
{
	public class MonthStats
	{
		public List<Transaction> WasabiCjs { get; } = new List<Transaction>();
		public List<Transaction> SamouraiCjs { get; } = new List<Transaction>();
	}
}
