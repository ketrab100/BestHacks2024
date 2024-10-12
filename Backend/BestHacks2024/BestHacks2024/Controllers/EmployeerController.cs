using System.Security.Claims;
using AutoMapper;
using BestHacks2024.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestHacks2024.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class EmployeerController: ControllerBase
{
    private readonly IEmployerService _employerService;
    private readonly IMapper _mapper;
    
    public EmployeerController(IEmployerService employerService, IMapper mapper)
    {
        _employerService = employerService;
        _mapper = mapper;
    }
    
    [HttpGet("next")]
    public async Task<IActionResult> GetJobAsync(CancellationToken cancellationToken)
    {
        var identity = HttpContext.User.Identity  as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst("id").Value ?? throw new ArgumentException("Employee not found")) ;
        var employers  = await _employerService.GetNextEmployers(id, cancellationToken);
        
        return Ok(employers);
    }
}