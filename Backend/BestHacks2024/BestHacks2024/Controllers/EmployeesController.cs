using AutoMapper;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestHacks2024.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
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
        
        return Ok(employeeDto);
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
    
    [HttpGet("next")]
    public async Task<IActionResult> GetNextAsync(CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity  as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value ?? throw new ArgumentException("Employee not found")) ;
        var employers  = await _employeeService.GetNextEmployeesAsync(id, cancellationToken);
        
        return Ok(employers);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetEmployeeAsync(Guid id)
    //{
    //    var employee = await _employeeService.GetEmployeeByIdAsync(id);

    //    if(employee is null) return NotFound();
    //    return Ok(employee);
    //}

    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        return Ok(await _employeeService.GetEmployeesAsync());
    }

    //[HttpPost]
    //public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDto dto)
    //{
    //    var employee = await _employeeService.CreateEmployeeAsync(dto);
    //    if(employee is null) return BadRequest();
    //    return Ok(employee);
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] EmployeeDto dto)
    //{
    //    var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, dto);
    //    if (updatedEmployee == null) return NotFound();

    //    return Ok(updatedEmployee);
    //}

    //[HttpDelete]
    //public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
    //{
    //    try
    //    {
    //        await _employeeService.DeleteEmployeeAsync(id);
    //        return Ok();
    //    }
    //    catch
    //    {
    //        return BadRequest();
    //    }
    //}
}