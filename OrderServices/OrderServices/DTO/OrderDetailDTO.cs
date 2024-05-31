using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.DTO
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }

        public int OrderHeaderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}