using AutoMapper;
using DressedUp.Application.Mappings.Profiles;

namespace DressedUp.Application.Mappings;

public static class MappingProfileRegister
{
    public static void RegisterMappings(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<UserMappingProfile>();
        // Diğer profilleri buraya ekleyin
    }
}