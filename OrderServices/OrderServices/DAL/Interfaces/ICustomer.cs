using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.Models;

namespace OrderServices.DAL.interfaces
{
    public interface ICustomer : ICrud<Customer>
    {
        IEnumerable<Customer> GetByName(string name);
    }
}