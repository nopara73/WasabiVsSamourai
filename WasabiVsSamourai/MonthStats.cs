using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WasabiVsSamourai
{
	public class MonthStats
	{
		public List<Transaction> WasabiCjs { get; } = new List<Transaction>();
		public List<Transaction> SamouraiCjs { get; } = new List<Transaction>();

		public Money WasabiTotalVolume => GetTotalVolume(WasabiCjs);
		public Money SamouraiTotalVolume => GetTotalVolume(SamouraiCjs);

		private Money GetTotalVolume(IEnumerable<Transaction> txs)
		{
			return txs.SelectMany(x => x.Outputs).Sum(x => x.Value);
		}
	}
}
