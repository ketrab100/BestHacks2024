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

        foreach (var tag in employeeDto.Tags)
        {
            if (!existingTags.Any(t => t.Id == tag.Id))
            {
                _context.Tags.Add(tag);
                existingTags.Add(tag);
            }
        }

        var employee = _mapper.Map<Employee>(employeeDto);
        employee.CreatedAt = DateTime.UtcNow;

        employee.UserTags = existingTags.Select(tag => new UserTag
        {
            Tag = tag,
            User = employee
        }).ToList();

        _context.Employees.Add(employee);
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

        _mapper.Map(employeeDto, employee);

        var tagIds = employeeDto.Tags.Select(t => t.Id).ToList();

        var existingTags = await _context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync();

        foreach (var tag in employeeDto.Tags)
        {
            if (!existingTags.Any(t => t.Id == tag.Id))
            {
                _context.Tags.Add(tag);
                existingTags.Add(tag);
            }
        }

        employee.UserTags.Clear();

        employee.UserTags = existingTags.Select(tag => new UserTag
        {
            Tag = tag,
            User = employee
        }).ToList();

        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee?> UpdateEmployeeProfileAsync(Guid id, EmployeeProfileDto employeeDto)
    {
        var employee = await _context.Employees
            .Include(e => e.UserTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            throw new KeyNotFoundException("Employee not found");

        //_mapper.Map(employeeDto, employee);
        employee.FirstName = employeeDto.FirstName;
        employee.LastName = employeeDto.LastName;
        employee.Bio = employeeDto.Bio;
        employee.Location = employeeDto.Location;
        employee.ExperienceLevel = employeeDto.Experience;

        var employeeTags = employeeDto.Tags.Select(x => x.Name).ToList();

        var tags = await _context.Tags
            .Select(x => x.Name)
            .ToListAsync();

        var commonTags = tags.Intersect(employeeTags);

        //foreach (var tag in commonTags) 
        //{
        //    var tagObj = new Tag { Name = tag };
        //    employee.UserTags.Add(new UserTag
        //    {
        //        Emp
        //    });
        //}

        //employee.UserTags.Clear();

        //employee.UserTags = existingTags.Select(tag => new UserTag
        //{
        //    Tag = tag,
        //    User = employee
        //}).ToList();

        _context.Employees.Update(employee);
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

    private Employee MapToEmployee(EmployeeProfileDto employee)
    {
        return new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            //Email = employee.Email,
            Bio = employee.Bio,
            Location = employee.Location
        };
    }
}