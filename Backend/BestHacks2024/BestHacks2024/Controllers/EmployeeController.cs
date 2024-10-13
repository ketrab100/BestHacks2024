using AutoMapper;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using BestHacks2024.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestHacks2024.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    //private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeAsync(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        
        if(employee is null) return NotFound();
        return Ok(employee);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyAccount()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var employee = await _employeeService.GetEmployeeByIdAsync(Guid.Parse(identity.FindFirst("id").Value)) ?? throw new ArgumentException("Employee not found");

        var tags = new List<TagDto>();
        foreach (var tag in employee.UserTags)
        {
            TagDto tagDto = new TagDto();
            tagDto.Id = tag.TagId;
            tagDto.Name = tag.Tag.Name;
            tags.Add(tagDto);
        }

        var employeeDto = new EmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Bio = employee.Bio,
            Location = employee.Location,
            Experience = employee.ExperienceLevel,
            ImageBase64 = Convert.ToBase64String(employee.Image ?? new byte[0]),
            Tags = tags
        };
        
        return Ok(employee);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        return Ok(await _employeeService.GetEmployeesAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDto dto)
    {
        var employee = await _employeeService.CreateEmployeeAsync(dto);
        if(employee is null) return BadRequest();
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] EmployeeDto dto)
    {
        var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, dto);
        if (updatedEmployee == null) return NotFound();
    
        return Ok(updatedEmployee);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeDto dto)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value);
        var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, dto);
        if (updatedEmployee == null) return NotFound();

        return Ok(updatedEmployee);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
    {
        try
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
    private TagDto MapToTagDto(Tag tag)
    {
        return new TagDto
        {
            Name = tag.Name,
        };
    }
}