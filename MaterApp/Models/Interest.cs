namespace MaterApp.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserInterest> UserInterests { get; set; }
    }
}
