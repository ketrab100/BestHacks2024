using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using BestHacks2024.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestHacks2024.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeAsync(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        
        if(employee is null) return NotFound();
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
}