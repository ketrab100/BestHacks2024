using System.Security.Claims;
using AutoMapper;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestHacks2024.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class EmployerController : ControllerBase
{
    private readonly IEmployerService _employerService;
    private readonly IMapper _mapper;

    public EmployerController(IEmployerService employerService, IMapper mapper)
    {
        _employerService = employerService;
        _mapper = mapper;
    }

    [HttpGet("job/{id}")]
    public async Task<IActionResult> GetJobAsync(CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value ?? throw new ArgumentException("Employee not found"));
        var jobs = await _employerService.GetNextEmployers(id, cancellationToken);

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var employer = await _employerService.GetEmployerByIdAsync(id);
        
        if(employer is null) return NotFound();
        return Ok(employer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _employerService.GetEmployersAsync());
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyAccountAsync()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var employer = await _employerService.GetEmployerByIdAsync(Guid.Parse(identity.FindFirst("id").Value)) ?? throw new ArgumentException("Employer not found");

        var tags = new List<TagDto>();
        foreach (var tag in employer.EmployerTags)
        {
            TagDto tagDto = new TagDto();
            tagDto.Id = tag.TagId;
            tagDto.Name = tag.Tag.Name;
            tags.Add(tagDto);
        }
        
        var employerDto = new EmployerDto()
        {
            Id = employer.Id,
            CompanyName = employer.CompanyName,
            Email = employer.Email,
            Location = employer.Location,
            ExperienceLevel = employer.ExperienceLevel,
            JobDescription = employer.JobDescription,
            JobTitle = employer.JobTitle,
            ImageBase64 = Convert.ToBase64String(employer.Image ?? new byte[0]),
            Tags = tags
        };
        
        return Ok(employerDto);
    }
    
    [HttpPut("me")]
    public async Task<IActionResult> UpdateEmployerAsync([FromBody] EmployerDto dto)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value);
        var updatedEmployee = await _employerService.UpdateEmployerAsync(id, dto);
        if (updatedEmployee == null) return NotFound();

        return Ok(updatedEmployee);
    }
}