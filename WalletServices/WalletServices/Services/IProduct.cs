using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletServices.Models;
using WalletServices.DTO;


namespace WalletServices.Services
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetByProductId(int ProductId);
    }
}