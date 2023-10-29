namespace MaterApp.Models.DTO
{
    public class ChatRoomDTO
    {
        public int Id { get; set; }
        public List<int> ParticipantIds { get; set; }
        public DateTime LastMessageSentAt { get; set; }
    }
}
