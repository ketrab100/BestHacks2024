using System.Security.Claims;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using BestHacks2024.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestHacks2024.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyMatchesAsync(CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value);

        var myEmployeeMatches = await _matchService.GetEmployeeMatchesAsync(id); //wystarczy jeden
        //var myEmployerMatches = await _matchService.GetEmployerMatchesAsync(id);
        return Ok(myEmployeeMatches.Select(Mappers.MapToMatchDto));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMatchAsync([FromBody] SwipeDto swipeDto, CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value);

        var newMatch = await _matchService.CreateMatchAsync(id, swipeDto);
        return Ok(); // cyclic reference!
    }

    [HttpPost("/conversations")]
    public async Task<IActionResult> CreateConversationAsync([FromBody] AddConversationDto conversationDto, CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value);

        var newMatch = await _matchService.CreateConversationAsync(id, conversationDto);
        return Ok(); // cyclic reference!
    }

    //[HttpPut("update")]
    //public async Task<IActionResult> UpdateMatchAsync([FromBody] SwipeDto swipeDto, CancellationToken cancellationToken)
    //{
    //    var identity = HttpContext.User.Identity as ClaimsIdentity;
    //    var id = Guid.Parse(identity.FindFirst("id").Value);

    //    var updatedMatch = await _matchService.UpdateMatchAsync(id, swipeDto);
    //    return Ok(updatedMatch);
    //}

    //[HttpDelete("delete/{id}")]
    //public async Task<IActionResult> DeleteMatchAsync(Guid id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        await _matchService.DeleteMatchAsync(id);
    //        return Ok();
    //    }
    //    catch
    //    {
    //        return BadRequest();
    //    }
    //}

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    //{
    //    var match = await _matchService.GetMatchByIdAsync(id);

    //    if(match is null) return NotFound();
    //    return Ok(match);
    //}

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await _matchService.GetMatchesAsync());
    }

    //[HttpGet("employee/{id}")]
    //public async Task<IActionResult> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken)
    //{
    //    var matches = await _matchService.GetEmployeeMatchesAsync(employeeId);
    //    return Ok(matches);
    //}

    //[HttpGet("employer/{id}")]
    //public async Task<IActionResult> GetByEmployerId(Guid employerId, CancellationToken cancellationToken)
    //{
    //    var matches = await _matchService.GetEmployerMatchesAsync(employerId);
    //    return Ok(matches);
    //}
}