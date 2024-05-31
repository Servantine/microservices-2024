using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletServices.DTO
{
    public class WalletDTO
    {
        public int WalletId { get; set; }
        public int CustomerId { get; set; }
        public int Saldo { get; set; }
    }
}