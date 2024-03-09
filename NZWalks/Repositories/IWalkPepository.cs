using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domains;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories;

public interface IWalkPepository
{
    Task<List<Walk>> GetAllAsync( string? filterOn = null, string? filterQuery = null
        , string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1);
    Task<Walk>CreateAsync(Walk walk);
    Task<Walk> GetByIdAsync(Guid id);
    Task<Walk> UpdateAsync(Guid id, UpdateWalkRequestDto updateWalkRequestDto);
    Task<Walk> DeleteAsync(Guid id);
}

