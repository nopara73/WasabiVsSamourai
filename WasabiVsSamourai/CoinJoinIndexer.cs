using NBitcoin;
using NBitcoin.RPC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasabiVsSamourai.Helpers;

namespace WasabiVsSamourai
{
	public class CoinJoinIndexer
	{
		public RPCClient RpcClient { get; }

		public CoinJoinIndexer(RPCClient client)
		{
			RpcClient = Guard.NotNull(nameof(client), client);
		}

		public static IEnumerable<Script> WasabiCoordScripts = new Script[]
		{
			BitcoinAddress.Create("bc1qs604c7jv6amk4cxqlnvuxv26hv3e48cds4m0ew", Network.Main).ScriptPubKey,
			BitcoinAddress.Create("bc1qa24tsgchvuxsaccp8vrnkfd85hrcpafg20kmjw", Network.Main).ScriptPubKey
		};

		public static IEnumerable<Money> SamouraiPools = new Money[]
		{
			Money.Coins(0.01m),
			Money.Coins(0.05m),
			Money.Coins(0.5m)
		};

		public static Money MaxSamouraiPoolFee = Money.Coins(0.0011m);

		private decimal PercentageDone { get; set; } = 0;
		private decimal PreviousPercentageDone { get; set; } = -1;

		public async Task<IOrderedEnumerable<KeyValuePair<YearMonth, MonthStats>>> FindCoinJoinsAsync()
		{
			var bestHeight = await RpcClient.GetBlockCountAsync();

			// Starts with June 1.
			var height = 578718;
			var totalBlocks = bestHeight - height;
			Console.WriteLine($"{totalBlocks} blocks will be analyzed.");

			var months = new ConcurrentDictionary<YearMonth, MonthStats>();
			while (true)
			{
				var blockTasks = new List<Task<Block>>();

				var batchClient = RpcClient.PrepareBatch();
				// Default rpcworkqueue is 16 and GetBlockAsync is working with 2 requests.
				var parallelBlocks = 8;
				var maxHeight = Math.Min(height + parallelBlocks, bestHeight);
				int h;
				for (h = height; h < maxHeight; h++)
				{
					blockTasks.Add(batchClient.GetBlockAsync(h));
				}
				if (blockTasks.Any())
				{
					height = h - 1;
				}
				await batchClient.SendBatchAsync();

				Parallel.ForEach(blockTasks, async (blockTask) =>
				{
					var block = await blockTask;

					var timeStamp = block.Header.BlockTime;
					var monthStamp = new YearMonth { Year = timeStamp.Year, Month = timeStamp.Month };
					MonthStats stat;
					if (months.TryGetValue(monthStamp, out MonthStats s))
					{
						stat = s;
					}
					else
					{
						stat = new MonthStats();
						months.TryAdd(monthStamp, stat);

						var prevMonth = monthStamp.Month - 1;
						YearMonth prevYearMonth = new YearMonth { Year = monthStamp.Year, Month = prevMonth };
						if (prevMonth >= 1 && months.TryGetValue(prevYearMonth, out MonthStats prevS))
						{
							Console.WriteLine(prevYearMonth);
							prevS.Display();
						}
					}

					Parallel.ForEach(block.Transactions, (tx) =>
					{
						Parallel.Invoke(
							() =>
							{
								var isWasabiCj = tx.Outputs.Any(x => WasabiCoordScripts.Contains(x.ScriptPubKey)) && tx.GetIndistinguishableOutputs(includeSingle: false).Any(x => x.count > 2);
								if (isWasabiCj)
								{
									stat.AddWasabiTx(tx);
								}
							},
							() =>
							{
								var isSamouraiCj = tx.Inputs.Count == 5 && tx.Outputs.Count == 5 && tx.Outputs.Select(x => x.Value).Distinct().Count() == 1 && SamouraiPools.Any(x => x.Almost(tx.Outputs.First().Value, MaxSamouraiPoolFee));
								if (isSamouraiCj)
								{
									stat.AddSamouraiTx(tx);
								}
							});
					});
				});

				decimal totalBlocksPer100 = totalBlocks / 100m;
				int blocksLeft = bestHeight - height;
				int processedBlocks = totalBlocks - blocksLeft;
				PercentageDone = processedBlocks / totalBlocksPer100;
				bool displayProgress = (PercentageDone - PreviousPercentageDone) >= 1;
				if (displayProgress)
				{
					Console.WriteLine($"Progress: {(int)PercentageDone}%");
					PreviousPercentageDone = PercentageDone;
				}
				if (bestHeight <= height)
				{
					// Refresh bestHeight and if still no new block, then end here.
					bestHeight = await RpcClient.GetBlockCountAsync();
					if (bestHeight <= height)
					{
						break;
					}
				}
				height++;
			}

			IOrderedEnumerable<KeyValuePair<YearMonth, MonthStats>> ordered = months
				.OrderBy(x => x.Key.Year)
				.ThenBy(x => x.Key.Month);
			return ordered;
		}
	}
}
