using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletServices.Models;

namespace WalletServices.DAL.interfaces
{
    public interface IWallet : ICrud<Wallet>
    {
        List<Wallet> GetAll();
        IEnumerable<Wallet> GetByName(string name);

        new Wallet GetById(int id);
    }
        

}