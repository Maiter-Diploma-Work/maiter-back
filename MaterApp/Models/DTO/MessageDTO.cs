namespace MaterApp.Models.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int CreatorId { get; set; }
        public string Content { get; set; }
        public bool BeenRedacted { get; set; }
        public string Type { get; set; }
        public DateTime SentAt { get; set; }
    }
}
