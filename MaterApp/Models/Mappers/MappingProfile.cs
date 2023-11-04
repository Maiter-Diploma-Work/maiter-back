using AutoMapper;
using MaterApp.Models.DTO;

namespace MaterApp.Models.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            this.CreateMap<RegisterPersonalInfoDTO, User>();
        }
    }
}
