using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Product_Id { get; set; }
    }
}
