using AutoMapper;
using KvizHub.DTO.User;
using KvizHub.Models.User;

namespace QuizWebServer.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationDTO, Users>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.Image, o => o.Ignore());

            CreateMap<Users, UserResponseDTO>();
        }
    }
}
