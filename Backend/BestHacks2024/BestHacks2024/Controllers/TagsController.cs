using System.Security.Claims;
using AutoMapper;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using BestHacks2024.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestHacks2024.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTagsAsync()
    {
        var tags = await _tagService.GetTagsAsync();
        var tagDtos = tags.Select(Mappers.MapToTagDto);
        return Ok(await _tagService.GetTagsAsync());
    }

    //[HttpPost]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    //public async Task<IActionResult> AddTagAsync([FromBody] TagDto dto)
    //{
    //    var employee = await _tagService.CreateTagAsync(dto);
    //    if(employee is null) return BadRequest();
    //    return Ok(employee);
    //}

    //[HttpDelete]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    //public async Task<IActionResult> DeleteTagAsync(Guid id)
    //{
    //    try
    //    {
    //        await _tagService.DeleteTagAsync(id);
    //        return Ok();
    //    }
    //    catch
    //    {
    //        return BadRequest();
    //    }
    //}
}