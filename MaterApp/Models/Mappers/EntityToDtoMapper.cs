using MaterApp.Models.DTO;

namespace MaterApp.Models.Mappers
{
    public class EntityToDtoMapper
    {
        public static User RegisterPersonalInfoDtoToUser(RegisterPersonalInfoDTO dto)
        {
            User user = new User();
            user.FirstName = dto.Name;
            return user;
        }
    }
}
