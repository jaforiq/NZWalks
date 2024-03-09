using AutoMapper;
using NZWalks.Data;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;
using NZWalks.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RegionsController : ControllerBase
{
    //private readonly NZWalkDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionsController(NZWalkAuthDbContext dbContext, IRegionRepository regionRepository,
         IMapper mapper) 
    {
        //_dbContext = dbContext;
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        var regionDomain = await _regionRepository.GetAllAsync();  // repository isresponsible to talk to DB

        // map/convert Region Domain Model to Region Dto
        //var regionsDto = new List<RegionDto>();
        //foreach (var regionDomain in regions)
        //{
        //    regionsDto.Add(new RegionDto()
        //    {
        //        Id = regionDomain.Id,
        //        Code = regionDomain.Code,
        //        Name = regionDomain.Name,
        //        ImageURL = regionDomain.ImageURL
        //    });

        //}

        // Map Domain Model to DTOs
        var regionDtos = _mapper.Map<List<RegionDto>>(regionDomain);  // Map<Destination>(Source);

        return Ok(regionDtos);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        //var region = _dbContext.Regions.Find(id);  // find only take prymary key
        var regionDomain = await _regionRepository.GetByIdAsync(id);

        if(regionDomain == null)
        {
            return NotFound();
        }

        // map/convert Region Domain Model to Region Dto
        //var regionsDto= new RegionDto()
        //{
        //    Id = regionDomain.Id,
        //    Code = regionDomain.Code,
        //    Name = regionDomain.Name,
        //    ImageURL = regionDomain.ImageURL
        //};

        // Return Dto back to claint
        
        return Ok(_mapper.Map<RegionDto>(regionDomain));
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Map/ convert Dto to Domain Model
        //var regionDomainModel = new Region
        //{
        //    Code = addRegionRequestDto.Code,
        //    Name = addRegionRequestDto.Name,
        //    ImageURL = addRegionRequestDto.ImageURL
        //};
        
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            //return Ok();
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        
       
        

        // Map Domain model back to Dto
        //var regionDto = new RegionDto
        //{
        //    Id = regionDomainModel.Id,
        //    Code = regionDomainModel.Code,
        //    Name = regionDomainModel.Name,
        //    ImageURL = regionDomainModel.ImageURL
        //};
                
    }

    [HttpPut]
    [ValidateModel]
    [Route("id:Guid")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        //var regionDomainModel = new Region
        //{
        //    Code = updateRegionRequestDto.Code,
        //    Name = updateRegionRequestDto.Name,
        //    ImageURL = updateRegionRequestDto.ImageURL
        //};
        
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return null;
            }

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        
        

        // map Domain to Dto
        //var regionDto = new RegionDto
        //{
        //    Id = regionDomainModel.Id,
        //    Code = regionDomainModel.Code,
        //    Name = regionDomainModel.Name,
        //    ImageURL = regionDomainModel.ImageURL
        //};

        
    }

    [HttpDelete]
    [Route("id:Guid")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var regionDomain =await _regionRepository.DeleteAsync(id);

        if(regionDomain == null)
        {
            return NotFound();
        }

        //var regionDto = new RegionDto
        //{
        //    Id = regionDomain.Id,
        //    Code = regionDomain.Code,
        //    Name = regionDomain.Name,
        //    ImageURL = regionDomain.ImageURL
        //};

        var regionDto = _mapper.Map<RegionDto>(regionDomain);

        return Ok(regionDto);
    }
}

