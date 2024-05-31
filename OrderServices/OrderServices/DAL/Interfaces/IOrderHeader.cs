using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.DAL.interfaces;
using OrderServices.Models;

namespace OrderServices.DAL.Interfaces
{
    public interface IOrderHeader : ICrud<OrderHeader>
    {
        IEnumerable<OrderHeader> GetByName(string name);
    }
}