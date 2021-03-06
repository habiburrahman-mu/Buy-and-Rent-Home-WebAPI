using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string Mobile { get; set; }
    }
}
