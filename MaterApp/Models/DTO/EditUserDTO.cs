namespace MaterApp.Models.DTO
{
    public class EditUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public List<int> Interests { get; set; }
    }
}
