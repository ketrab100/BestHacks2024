using AutoMapper;
using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class MatchService : IMatchService
{
    private readonly BestHacksDbContext _context;
    
    public MatchService(BestHacksDbContext context)
    {
        _context = context;
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
            .Include(x => x.Employee).ThenInclude(x => x.UserTags).ThenInclude(x => x.Tag)
            .Include(x => x.Employer).ThenInclude(x => x.EmployerTags).ThenInclude(x => x.Tag)
            .Include(x => x.Conversations)
            .AsNoTracking()
            .Where(x => x.Employee.Id == employeeId && x.AreMatched == true)
            .ToListAsync();
    }

    public async Task<ICollection<Match>> GetEmployerMatchesAsync(Guid employerId)
    {
        return await _context
            .Matches
            .Include(x => x.Employee).ThenInclude(x => x.UserTags).ThenInclude(x => x.Tag)
            .Include(x => x.Employer).ThenInclude(x => x.EmployerTags).ThenInclude(x => x.Tag)
            .AsNoTracking()
            .Where(x => x.Employer.Id == employerId && x.AreMatched == true)
            .ToListAsync();
    }

    public async Task<Match> CreateMatchAsync(Guid userId, SwipeDto swipeDto)
    {
        Employee? employee;
        Employer? employer;
        bool isEmployee = false;
        
        employee = await _context.Employees.FirstOrDefaultAsync(x=> x.Id == userId);
        if (employee == null)
        {
            employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NullReferenceException("Could not find employer");
            employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == swipeDto.SwipedId) ?? throw new NullReferenceException("Could not find employee");
        }
        else
        {
            isEmployee = true;
            employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == swipeDto.SwipedId) ?? throw new NullReferenceException("Could not find employer");
        }

        var match = await _context
            .Matches
            .Where(x => x.Employee.Id == employee.Id && x.Employer.Id == employer.Id)
            .FirstOrDefaultAsync();
        if (match is null)
        {
            match = new Match();
            match.Employee = employee;
            match.Employer = employer;

            match.DidEmployerAcceptCandidate = swipeDto.SwipedId == employee.Id && swipeDto.SwipeResult;
            match.DidEmployeeAcceptJobOffer = swipeDto.SwipedId == employer.Id && swipeDto.SwipeResult;

            _context.Matches.Add(match);
        }
        else
        {
            if (!isEmployee) 
                match.DidEmployerAcceptCandidate = swipeDto.SwipeResult;
            else  
                match.DidEmployeeAcceptJobOffer = swipeDto.SwipeResult;
        }

        if (match.DidEmployerAcceptCandidate is not null && match.DidEmployeeAcceptJobOffer is not null)
            match.AreMatched = match.DidEmployerAcceptCandidate.Value && match.DidEmployeeAcceptJobOffer.Value;
        else
            match.AreMatched = false;
        await _context.SaveChangesAsync();

        return match;
    }

    public async Task<Match?> UpdateMatchAsync(Guid userId, SwipeDto swipeDto)
    {
        Employee? employee;
        Employer? employer;
        
        employee = await _context.Employees.FirstOrDefaultAsync(x=> x.Id == userId);
        if (employee == null)
        {
            employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NullReferenceException("Could not find employer");
            employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == swipeDto.SwipedId) ?? throw new NullReferenceException("Could not find employee");
        }
        else
        {
            employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == swipeDto.SwipedId) ?? throw new NullReferenceException("Could not find employer");
        }

        var match = await _context
            .Matches
            .Where(x => x.Employee.Id == employee.Id && x.Employer.Id == employer.Id)
            .FirstOrDefaultAsync();
        if (match is null)
        {
            match = new Match();
            match.Employee = employee;
            match.Employer = employer;

            match.DidEmployerAcceptCandidate = swipeDto.SwipedId == employee.Id && swipeDto.SwipeResult;
            match.DidEmployeeAcceptJobOffer = swipeDto.SwipedId == employer.Id && swipeDto.SwipeResult;
        }
        else
        {
            if (match.DidEmployerAcceptCandidate is null) match.DidEmployerAcceptCandidate = swipeDto.SwipeResult;
            else if (match.DidEmployeeAcceptJobOffer is null) match.DidEmployeeAcceptJobOffer = swipeDto.SwipeResult;
        }

        if (match.DidEmployerAcceptCandidate is not null && match.DidEmployeeAcceptJobOffer is not null)
            match.AreMatched = match.DidEmployerAcceptCandidate.Value && match.DidEmployeeAcceptJobOffer.Value;
        else
        {
            match.AreMatched = false;
        }
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return match;
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

    public async Task<AddConversationDto> CreateConversationAsync(Guid userId, AddConversationDto conversationDto)
    {
        var newConversation = new Conversation();
        newConversation.SenderId = userId;
        newConversation.Message = conversationDto.Message;
        newConversation.MatchId = conversationDto.MatchId;

        _context.Conversations.Add(newConversation);
        await _context.SaveChangesAsync();

        return conversationDto;
    }
}