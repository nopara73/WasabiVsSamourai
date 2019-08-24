using NBitcoin;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WasabiVsSamourai
{
	internal class Program
	{
		// TODO:
		// :) Establish RPC connecton with Bitcoin Core.
		// :) Go through all the blocks and txs from June 1. (Whirlpool launched the end of June?)
		// :) Identify Wasabi coinjoins (2 coord addresses + indistinguishable outputs > 2.)
		// Identify Samourai coinjoins (5 in, 5 out + almost equal amounts.)
		// Count the total volume.
		// Count the mixed volume.
		// Count the mixed volume weighted with the anonset gained.
		// Write out monthly comparision.
		// Publish to GitHub, were the readme is the output.
		private static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World! This software compares Wasabi and Samourai coinjoins. Although, I'm not sure it makes much sense, because Wasabi is trustless, and Samourai is untrusted.");

			ParseArgs(args, out NetworkCredential rpcCred);

			var rpcConf = new RPCCredentialString
			{
				UserPassword = rpcCred
			};
			var client = new RPCClient(rpcConf, Network.Main);

			var bestHeight = await client.GetBlockCountAsync();
			// Starts with June 1.
			var height = 578717;
			while (true)
			{
				var block = await client.GetBlockAsync(height);

				var timeStamp = block.Header.BlockTime;
				foreach (var tx in block.Transactions)
				{
					var isWasabiCj = tx.Outputs.Any(x => WasabiCoordScripts.Contains(x.ScriptPubKey)) && tx.GetIndistinguishableOutputs(includeSingle: false).Any(x => x.count > 2);

					if (isWasabiCj)
					{
						Console.WriteLine(tx.GetHash());
					}
				}

				if (bestHeight <= height)
				{
					// Refresh bestHeight and if still no new block, then end here.
					bestHeight = await client.GetBlockCountAsync();
					if (bestHeight <= height)
					{
						break;
					}
				}
				height++;
			}

			Console.WriteLine();
			Console.WriteLine("Press a button to exit...");
			Console.ReadKey();
		}

		public static IEnumerable<Script> WasabiCoordScripts = new Script[]
		{
			BitcoinAddress.Create("bc1qs604c7jv6amk4cxqlnvuxv26hv3e48cds4m0ew", Network.Main).ScriptPubKey,
			BitcoinAddress.Create("bc1qa24tsgchvuxsaccp8vrnkfd85hrcpafg20kmjw", Network.Main).ScriptPubKey
		};

		private static void ParseArgs(string[] args, out NetworkCredential cred)
		{
			string rpcUser = null;
			string rpcPassword = null;

			var rpcUserArg = "--rpcuser=";
			var rpcPasswordArg = "--rpcpassword=";
			foreach (var arg in args)
			{
				var idx = arg.IndexOf(rpcUserArg, StringComparison.Ordinal);
				if (idx == 0)
				{
					rpcUser = arg.Substring(idx + rpcUserArg.Length);
				}

				idx = arg.IndexOf(rpcPasswordArg, StringComparison.Ordinal);
				if (idx == 0)
				{
					rpcPassword = arg.Substring(idx + rpcPasswordArg.Length);
				}
			}

			cred = new NetworkCredential(rpcUser, rpcPassword);
		}
	}
}
