using AutoMapper;
using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RestSharp;

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

    public async Task<List<Employee>> GetNextEmployeesAsync(Guid id, CancellationToken cancellationToken)
    {
        var aiJobs = new List<EmployerAiDto>();
        try
        {
            var options = new RestClientOptions($"http://localhost:8000/matches/");
            var client = new RestClient(options);
            var request = new RestRequest($"employees/{id}/");
            var response = await client.GetAsync<List<EmployerAiDto>>(request, cancellationToken);
            if (response is not null)
            {
                aiJobs = response.Where(x => x.Score > 0).ToList();
            }
        }
        catch{}

        var employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        var employerTags = employer.EmployerTags.Select(x => x.TagId);
        var employees = await _context.Employees.Where(x => aiJobs.Select(y => y.Id).Contains(x.Id)).ToListAsync(cancellationToken);

        var restOfEmployees = await _context.Employees
            .Where(x=> x.Matches.Any(y=>y.JobId!=employer.Id))
            .Where(x => x.UserTags.Any(y => employerTags.Contains(y.TagId)))
            .OrderBy(x=>x.Id).Take(10-aiJobs.Count).ToListAsync(cancellationToken);
        
        employees = employees.Concat(restOfEmployees).ToList();
        return employees;
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

    public async Task<Employee?> UpdateEmployeeProfileAsync(Guid id, EmployeeDto employeeDto)
    {
        var employee = await _context.Employees
            .Include(e => e.UserTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            throw new KeyNotFoundException("Employee not found");

        _mapper.Map(employeeDto, employee);

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
}