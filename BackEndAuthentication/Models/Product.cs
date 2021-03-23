using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public int Price { get; set; }
    }
}
