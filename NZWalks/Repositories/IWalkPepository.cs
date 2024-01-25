using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domains;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories;

public interface IWalkPepository
{
    Task<List<Walk>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery = null);
    Task<Walk>CreateAsync(Walk walk);
    Task<Walk> GetByIdAsync(Guid id);
    Task<Walk> UpdateAsync(Guid id, UpdateWalkRequestDto updateWalkRequestDto);
    Task<Walk> DeleteAsync(Guid id);
}

