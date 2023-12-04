using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
