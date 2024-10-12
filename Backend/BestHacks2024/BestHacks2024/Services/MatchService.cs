using AutoMapper;
using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class MatchService : IMatchService
{
    private readonly BestHacksDbContext _context;
    private readonly IMapper _mapper;
    
    public MatchService(BestHacksDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<Match>> GetMatchesAsync()
    {
        return await _context
            .Matches
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Match?> GetMatchByIdAsync(Guid id)
    {
        return await _context
            .Matches
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ICollection<Match>> GetEmployeeMatchesAsync(Guid employeeId)
    {
        return await _context
            .Matches
            .AsNoTracking()
            .Where(x => x.Employee.Id == employeeId)
            .ToListAsync();
    }

    public async Task<ICollection<Match>> GetEmployerMatchesAsync(Guid employerId)
    {
        return await _context
            .Matches
            .AsNoTracking()
            .Where(x => x.Employer.Id == employerId)
            .ToListAsync();
    }

    public async Task<Match> CreateMatchAsync(MatchDto matchDto)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x=> x.Id == matchDto.EmployeeId);
        if (employee == null)
        {
            throw new KeyNotFoundException("Employee not found");
        }

        var employer = await _context.Employers.FirstOrDefaultAsync(x=> x.Id == matchDto.JobId);
        if(employer == null)
            throw new KeyNotFoundException("Employer not found");

        var match = _mapper.Map<Match>(matchDto);

        match.Employee = employee;
        match.Employer = employer;
        match.AreMatched = matchDto.DidEmployerAcceptCandidate && matchDto.DidEmployeeAcceptJobOffer;

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return match;
    }

    public async Task<Match?> UpdateMatchAsync(Guid matchId, MatchDto matchDto)
    {
        var existingMatch = await _context.Matches
            .Include(m => m.Employee)
            .Include(m => m.Employer)
            .FirstOrDefaultAsync(m => m.Id == matchId);

        if (existingMatch == null)
        {
            throw new KeyNotFoundException("Match not found");
        }

        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == matchDto.EmployeeId);
        if (employee == null)
        {
            throw new KeyNotFoundException("Employee not found");
        }

        var employer = await _context.Employers.FirstOrDefaultAsync(x=> x.Id == matchDto.JobId);
        if (employer == null)
        {
            throw new KeyNotFoundException("Employer not found");
        }

        _mapper.Map(matchDto, existingMatch);

        existingMatch.Employee = employee;
        existingMatch.Employer = employer;

        existingMatch.DidEmployerAcceptCandidate = matchDto.DidEmployerAcceptCandidate;
        existingMatch.DidEmployeeAcceptJobOffer = matchDto.DidEmployeeAcceptJobOffer;

        _context.Matches.Update(existingMatch);
        await _context.SaveChangesAsync();

        return existingMatch;
    }

    public async Task DeleteMatchAsync(Guid matchId)
    {
        var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == matchId);

        if (match == null)
        {
            throw new Exception("Match not found");
        }

        _context.Matches.Remove(match);
        await _context.SaveChangesAsync();
    }
}