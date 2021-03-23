using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.DTO
{
    public class UpdateModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Initial_Password { get; set; }
        public string New_Password { get; set; }
        public string Confirmed_Password { get; set; }
    }
}
