using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.DTO
{
    public class CostumerDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
    }
}