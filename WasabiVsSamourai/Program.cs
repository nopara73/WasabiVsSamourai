using NBitcoin;
using NBitcoin.RPC;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WasabiVsSamourai
{
	internal class Program
	{
		// TODO:
		// :) Establish RPC connecton with Bitcoin Core.
		// :) Go through all the blocks and txs from June 1. (Whirlpool launched the end of June?)
		// Identify Wasabi coinjoins (2 coord addresses + indistinguishable outputs > 2.)
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
				}

				if (bestHeight <= height)
				{
					break;
				}
				height++;
			}

			Console.WriteLine();
			Console.WriteLine("Press a button to exit...");
			Console.ReadKey();
		}

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
