using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaterApp.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsAccepted { get; set; }

        [ForeignKey(nameof(User1Id))]
        public User User1 { get; set; }

        [ForeignKey(nameof(User2Id))]
        public User User2 { get; set; }
    }
}
