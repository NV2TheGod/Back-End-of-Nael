using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.DTO
{
    public class OrderDTO
    {
        public int Customer_Id { get; set; }
        public List<int> Product_Id { get; set; }
    }
}
