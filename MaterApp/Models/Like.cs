using System.ComponentModel.DataAnnotations;

namespace MaterApp.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; } // Первичный ключ для модели Like

        public int LikerId { get; set; } // Идентификатор пользователя, который ставит лайк
        public int LikeeId { get; set; } // Идентификатор пользователя, которому ставят лайк

        public User Liker { get; set; } // Навигационное свойство для пользователя, который ставит лайк
        public User Likee { get; set; } // Навигационное свойство для пользователя, которому ставят лайк
    }

}
