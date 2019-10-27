using NBitcoin;
using NBitcoin.RPC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WasabiVsSamourai.Helpers;

namespace WasabiVsSamourai
{
	public class Program
	{
		private static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World! This software compares Wasabi and Samourai coinjoins. Although, I'm not sure it makes much sense, because Wasabi is trustless, and Samourai is untrusted.");
			Console.WriteLine();

			Logger.InitializeDefaults();

			ParseArgs(args, out NetworkCredential rpcCred);

			var rpcConf = new RPCCredentialString
			{
				UserPassword = rpcCred
			};
			var client = new RPCClient(rpcConf, Network.Main);

			using (BenchmarkLogger.Measure(operationName: "Parsing The Blockchain For CoinJoins"))
			{
				var cjIndexer = new CoinJoinIndexer(client);
				var months = await cjIndexer.FindCoinJoinsAsync();

				foreach (var month in months)
				{
					Console.WriteLine();
					Console.WriteLine(month.Key);
					month.Value.Display();
				}
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
