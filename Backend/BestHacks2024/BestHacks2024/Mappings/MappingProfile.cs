using AutoMapper;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;

namespace BestHacks2024.Mappings;

public class MappingProfile : Profile
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployerService _employerService;
    public MappingProfile(IEmployeeService employeeService, IEmployerService employerService)
    {
        _employeeService = employeeService;
        _employerService = employerService;

        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.Experience))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserTags, opt => opt.MapFrom(src => src.Tags.Select(tag => new UserTag
            {
                Tag = tag,
                TagId = tag.Id,
            })));

        CreateMap<MatchDto, Match>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
            .ForMember(dest => dest.Conversations, opt => opt.Ignore()) // Conversations handled separately
            .ForMember(dest => dest.Employee, opt => opt.Ignore()) // Handle Employee in the service
            .ForMember(dest => dest.Employer, opt => opt.Ignore()) // Handle Job in the service
            .ForMember(dest => dest.AreMatched, opt => opt.Ignore()) // Ignore for now, calculate separately if needed
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // Set in the service or during entity creation
    }
}