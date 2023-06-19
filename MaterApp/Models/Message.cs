using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaterApp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }
    }
}
