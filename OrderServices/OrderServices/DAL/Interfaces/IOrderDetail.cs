using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.Models;

namespace OrderServices.DAL.interfaces
{
    public interface IOrderDetail : ICrud<OrderDetail>
    {
        IEnumerable<OrderDetail> GetByName(string name);
    }
}