using System.ComponentModel.DataAnnotations;

namespace MaterApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }

        public ICollection<User> Participants { get; set; }
    }
}
