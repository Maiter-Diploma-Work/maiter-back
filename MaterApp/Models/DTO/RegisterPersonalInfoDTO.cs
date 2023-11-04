namespace MaterApp.Models.DTO
{
    public class CharacterTraitDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BottomName { get; set; }
        public string TopName { get; set; }
        public int Degree { get; set; }
    }

    public class GoalsDTO
    {
        public bool Friends { get; set; }
        public bool Love { get; set; }
        public bool Adventure { get; set; }
    }

    public class RegisterPersonalInfoDTO
    {
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Gender { get; set; }
        public List<double> Location { get; set; }
        public double Height { get; set; }
        public string Education { get; set; }
        public string BIO { get; set; }
        public List<CharacterTraitDTO> CharacterTraits { get; set; }
        public GoalsDTO Goals { get; set; }
        public string Status { get; set; }
        public string LookingFor { get; set; }
        public List<string> Expectations { get; set; }
        public List<string> Interests { get; set; }
        public string FavoriteSong { get; set; }
    }
}
