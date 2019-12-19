using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WasabiVsSamourai.Helpers;

namespace WasabiVsSamourai
{
	public class MonthStats
	{
		private List<Transaction> WasabiCjs { get; } = new List<Transaction>();
		private List<Transaction> SamouraiCjs { get; } = new List<Transaction>();
		private object Lock { get; } = new object();

		public void AddWasabiTx(Transaction tx)
		{
			lock (Lock)
			{
				WasabiCjs.Add(tx);
			}
		}

		public void AddSamouraiTx(Transaction tx)
		{
			lock (Lock)
			{
				SamouraiCjs.Add(tx);
			}
		}

		public int WasabiTxCount => WasabiCjs.Count;
		public int SamouraiTxCount => SamouraiCjs.Count;

		public Money WasabiTotalVolume => GetTotalVolume(WasabiCjs);
		public Money SamouraiTotalVolume => GetTotalVolume(SamouraiCjs);

		public Money WasabiTotalMixedVolume => GetTotalMixedVolume(WasabiCjs);
		public Money SamouraiTotalMixedVolume => GetTotalMixedVolume(SamouraiCjs);

		public Money WasabiTotalAnonsetWeightedMixedVolume => GetTotalAnonsetWeightedMixedVolume(WasabiCjs);
		public Money SamouraiTotalAnonsetWeightedMixedVolume => GetTotalAnonsetWeightedMixedVolume(SamouraiCjs);

		private Money GetTotalVolume(IEnumerable<Transaction> txs)
		{
			lock (Lock)
			{
				return txs.SelectMany(x => x.Outputs).Sum(x => x.Value);
			}
		}

		private Money GetTotalMixedVolume(IEnumerable<Transaction> txs)
		{
			lock (Lock)
			{
				var totalMixed = Money.Zero;
				foreach (var tx in txs)
				{
					foreach (var group in tx.GetIndistinguishableOutputs(false))
					{
						totalMixed += group.count * group.value;
					}
				}

				return totalMixed;
			}
		}

		private Money GetTotalAnonsetWeightedMixedVolume(IEnumerable<Transaction> txs)
		{
			lock (Lock)
			{
				var totalMixed = Money.Zero;
				foreach (var tx in txs)
				{
					foreach (var group in tx.GetIndistinguishableOutputs(false))
					{
						totalMixed += group.count * group.value * group.count;
					}
				}

				return totalMixed;
			}
		}

		public IEnumerable<Transaction> GetAllSamouraiCjs()
		{
			lock (Lock)
			{
				return SamouraiCjs.ToArray();
			}
		}

		public IEnumerable<Transaction> GetAllWasabiCjs()
		{
			lock (Lock)
			{
				return WasabiCjs.ToArray();
			}
		}

		public void Display()
		{
			Console.WriteLine($"Wasabi transaction count:                   {WasabiTxCount}");
			Console.WriteLine($"Samourai transaction count:                 {SamouraiTxCount}");
			Console.WriteLine($"Wasabi total volume:                        {WasabiTotalVolume.GetWholeBTC()} BTC");
			Console.WriteLine($"Samourai total volume:                      {SamouraiTotalVolume.GetWholeBTC()} BTC");
			Console.WriteLine($"Wasabi total mixed volume:                  {WasabiTotalMixedVolume.GetWholeBTC()} BTC");
			Console.WriteLine($"Samourai total mixed volume:                {SamouraiTotalMixedVolume.GetWholeBTC()} BTC");
			Console.WriteLine($"Wasabi anonset weighted volume mix score:   {WasabiTotalAnonsetWeightedMixedVolume.GetWholeBTC()}");
			Console.WriteLine($"Samourai anonset weighted volume mix score: {SamouraiTotalAnonsetWeightedMixedVolume.GetWholeBTC()}");
		}
	}
}
