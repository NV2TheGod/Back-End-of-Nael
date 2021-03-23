using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password_Hash { get; set; }
    }
}
