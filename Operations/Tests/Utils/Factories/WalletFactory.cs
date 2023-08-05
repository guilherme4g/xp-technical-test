using AutoBogus;
using Domain.Entities;

namespace Tests.Utils.Factories
{
    public static class WalletFactory
    {
        public static Wallet GenerateWallet()
        {
            var fakeWallet = AutoFaker.Generate<Wallet>();

            return fakeWallet;
        }

        public static Wallet[] GenerateWallets()
        {
            List<Wallet> wallets = new();

            for (int i = 0; i < 5; i++)
            {
               wallets.Add(GenerateWallet());
            }

            return wallets.ToArray();
        }
    }
}
