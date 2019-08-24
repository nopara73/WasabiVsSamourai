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

		public Money WasabiTotalMixedVolume => GetTotalMixedVolume(WasabiCjs);
		public Money SamouraiTotalMixedVolume => GetTotalMixedVolume(SamouraiCjs);

		public Money WasabiTotalAnonsetWeightedMixedVolume => GetTotalAnonsetWeightedMixedVolume(WasabiCjs);
		public Money SamouraiTotalAnonsetWeightedMixedVolume => GetTotalAnonsetWeightedMixedVolume(SamouraiCjs);

		private Money GetTotalVolume(IEnumerable<Transaction> txs)
		{
			return txs.SelectMany(x => x.Outputs).Sum(x => x.Value);
		}

		private Money GetTotalMixedVolume(IEnumerable<Transaction> txs)
		{
			var totalMixed = Money.Zero;
			foreach (var tx in txs)
			{
				totalMixed += tx.GetIndistinguishableOutputs(false).Sum(x => x.value);
			}

			return totalMixed;
		}

		private Money GetTotalAnonsetWeightedMixedVolume(IEnumerable<Transaction> txs)
		{
			var totalMixed = Money.Zero;
			foreach (var tx in txs)
			{
				totalMixed += tx.GetIndistinguishableOutputs(false).Sum(x => x.value * x.count);
			}

			return totalMixed;
		}
	}
}
