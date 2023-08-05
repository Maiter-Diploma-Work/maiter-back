using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterApp.Models
{
    public class BlockedUsers
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } // Идентификатор пользователя, который выполнил блокировку
        public int BlockedUserId { get; set; } // Идентификатор заблокированного пользователя

        // Навигационные свойства для связи с соответствующими пользователями
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("BlockedUserId")]
        public User BlockedUser { get; set; }

    }
}
