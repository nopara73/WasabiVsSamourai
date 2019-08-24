using System;

namespace WasabiVsSamourai
{
    internal class Program
    {
        // TODO:
        // Establish RPC connecton with Bitcoin Core.
        // Go through all the blocks and txs from June 1.
        // Identify Wasabi coinjoins (2 coord addresses + indistinguishable outputs > 2.)
        // Identify Samourai coinjoins (5 in, 5 out + almost equal amounts.)
        // Count the total volume.
        // Count the mixed volume.
        // Count the mixed volume weighted with the anonset gained.
        // Write out monthly comparision.
        // Publish to GitHub, were the readme is the output.
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World! This softeware compares Wasabi and Samourai coinjoins. Although, I'm not sure it makes much sense, because Wasabi is trustless, and Samourai is untrusted.");

            string rpcUser = "";
            string rpcPassword = "";
            ParseArgs(args, ref rpcUser, ref rpcPassword);

            Console.WriteLine();
            Console.WriteLine("Press a button to exit...");
            Console.ReadKey();
        }

        private static void ParseArgs(string[] args, ref string rpcUser, ref string rpcPassword)
        {
            foreach (var arg in args)
            {
                if (rpcUser == null)
                {
                    rpcUser = arg;
                }
                else if (rpcPassword == null)
                {
                    rpcPassword = arg;
                }
                else if (arg == "--rpcuser")
                {
                    rpcUser = null;
                }
                else if (arg == "--rpcpassword")
                {
                    rpcPassword = null;
                }
            }
        }
    }
}
