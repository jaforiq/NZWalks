using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NZWalks.Models.Domains;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : Controller
{
    private readonly IMapper _mapper;
    private readonly IWalkPepository _walkPepository;

    public WalksController(IMapper mapper, IWalkPepository walkPepository)
    {
        _mapper = mapper;
        _walkPepository = walkPepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
    {
        var walkDomainModel = await _walkPepository.GetAllAsync(filterOn, filterQuery);
        var walkDtos = _mapper.Map<List<WalkDto>>(walkDomainModel);
        return Ok(walkDtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // map DTO to model
        var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

        await _walkPepository.CreateAsync(walkDomainModel);

        // map Domain Model to Dto
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }


    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel =  await _walkPepository.GetByIdAsync(id);
        if(walkDomainModel == null)
        {
            return BadRequest();
        }

        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);        
        return Ok(walkDto);
    }

    [HttpPut]
    [Route("id:Guid")]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
        var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
        walkDomainModel = await _walkPepository.UpdateAsync(id, updateWalkRequestDto);
        if(walkDomainModel == null)
        {
            return NotFound();
        }

        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }

    [HttpDelete]
    [Route("id:Guid")]
    public async Task<IActionResult> Delete([FromQuery]Guid id)
    {
        var walkDomainModel = await _walkPepository.DeleteAsync(id);
        if(walkDomainModel == null)
        {
            return NotFound();
        }

        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }
}

