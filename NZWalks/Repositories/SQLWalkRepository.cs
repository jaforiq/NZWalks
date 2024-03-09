using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domains;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories;

public class SQLWalkRepository : IWalkPepository
{
    private readonly NZWalkAuthDbContext _dbContext;

    public SQLWalkRepository(NZWalkAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Walk>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null
        ,[FromQuery] string? sortBy = null , [FromQuery] bool isAscending = true, int pageNumber = 1, int pageSize = 1)
    {
        var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();  //Include(x => x.Difficult) this is type safe(vul likhle compile time e error dibe)
        // filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
            {
            walks = walks.Where(x => x.Name.Contains(filterQuery));  //get all names that contains filterQuery as input  
            }
        }

        // Sorting 
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if(sortBy.Equals("Length", StringComparison.CurrentCultureIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
            }

        }

        // Pagination
        var skipResult = (pageNumber - 1) * pageSize;
        return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk> GetByIdAsync(Guid id)
    {
        var walkDomainModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if(walkDomainModel == null)
        {
            return null;
        }
        return walkDomainModel;
    }

    public async Task<Walk> UpdateAsync(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
    {
        var walkDomainModel = await _dbContext.Walks.FirstOrDefaultAsync(x =>x.Id == id);

        walkDomainModel.Name = updateWalkRequestDto.Name;
        walkDomainModel.Description = updateWalkRequestDto.Description;
        walkDomainModel.LengthInKM = updateWalkRequestDto.LengthInKM;
        walkDomainModel.WalkImageURL = updateWalkRequestDto.WalkImageURL;
        walkDomainModel.DifficultyId = updateWalkRequestDto.DifficultyId;
        walkDomainModel.RegionId = updateWalkRequestDto.RegionId;

        await _dbContext.SaveChangesAsync();
        return walkDomainModel;
    }

    public async Task<Walk> DeleteAsync(Guid id)
    {
        var walkDomainModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if(walkDomainModel == null)
        {
            return null;
        }

         _dbContext.Walks.Remove(walkDomainModel);
        await _dbContext.SaveChangesAsync();
        return walkDomainModel;
    }
} 

