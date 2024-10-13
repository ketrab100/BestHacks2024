using AutoMapper;
using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BestHacks2024.Services;

public class EmployeeService : IEmployeeService
{
    private readonly BestHacksDbContext _context;
    private readonly IMapper _mapper;
    
    public EmployeeService(BestHacksDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        return await _context
            .Employees
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        return await _context
            .Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Employee?> GetNextEmployeeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Employee?> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var tagIds = employeeDto.Tags.Select(t => t.Id).ToList();
        var existingTags = await _context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync();

        var employee = new Employee()
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            Bio = employeeDto.Bio,
            Location = employeeDto.Location,
            ExperienceLevel = employeeDto.Experience,
            Image = Convert.FromBase64String(employeeDto.ImageBase64 ?? ""),
        };
        employee.CreatedAt = DateTime.UtcNow;

        employee.UserTags = existingTags.Select(tag => new UserTag
        {
            Tag = tag,
            User = employee
        }).ToList();

        var newEmployee = _context.Employees.Add(employee);
        var userTags = employeeDto.Tags.Select(x => new UserTag { TagId = x.Id, UserId = newEmployee.Entity.Id });
        await _context.UserTags.AddRangeAsync(userTags);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee?> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto)
    {
        var employee = await _context.Employees
            .Include(e => e.UserTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            throw new KeyNotFoundException("Employee not found");
        
        var userTags = employeeDto.Tags.Select(x => new UserTag { TagId = x.Id, UserId = employee.Id});
        employee.UserTags = userTags.ToList();
        
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task DeleteEmployeeAsync(Guid id)
    {
        var employee = await _context.Employees
            .Include(e => e.UserTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null) throw new KeyNotFoundException("Employee of ID not found");

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}