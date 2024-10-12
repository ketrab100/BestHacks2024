using AutoMapper;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.Experience))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserTags, opt => opt.MapFrom(src => src.Tags.Select(tag => new UserTag
            {
                Tag = tag
            })));
    }
}