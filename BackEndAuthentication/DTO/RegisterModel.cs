using System.ComponentModel.DataAnnotations;

namespace backend_shop.DTO
{
    public class RegisterModel
    {
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
