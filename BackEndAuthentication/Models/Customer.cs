using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
    }
}
