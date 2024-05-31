using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletServices.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int CustomerId { get; set; }
        public int Saldo { get; set; }

        internal static void Add(Wallet wallet)
        {
            throw new NotImplementedException();
        }
    }
}