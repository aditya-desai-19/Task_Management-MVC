using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management.Models
{
    public class Tasks
    {
        [Key]
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }

        //Navigation property
        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}
