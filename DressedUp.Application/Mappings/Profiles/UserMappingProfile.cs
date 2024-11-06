using AutoMapper;
using DressedUp.Application.Commands.User.Authentication;
using DressedUp.Domain.Aggregates.UserAggregate;

namespace DressedUp.Application.Mappings.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // RegisterUserCommand -> User mapping
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => (src.Password)));        
    }
}