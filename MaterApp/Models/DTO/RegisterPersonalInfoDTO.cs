namespace MaterApp.Models.DTO
{
    public class RegisterPersonalInfoDTO
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public double Height { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string BIO { get; set; }
        public int Temperament { get; set; }
        public int ThinkingType { get; set; } //тип мышления
        public int Availability { get; set; } //свободен/занят
        public int OrganizedOrNot { get; set; } //организованный/нет
        public int Independency { get; set; } //ком.игрок/незав-й
        public int ActivityLevel { get; set; } //уровень активности
        public int Riskiness { get; set; } //рискованный/нет
        public string Status { get; set; } 
        public string LookingFor { get; set; }
        public bool FriendGoal { get; set; }
        public bool LoveGoal { get; set; }
        public bool AdventureGoal { get; set; }
        public List<string> Expectations { get; set; }
        public List<string> Interests { get; set; }
    }
}
