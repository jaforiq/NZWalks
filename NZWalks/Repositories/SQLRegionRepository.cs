using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domains;


namespace NZWalks.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly NZWalkDbContext _dbContext;
    public SQLRegionRepository(NZWalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync(); 
    }

    public async Task<Region> GetByIdAsync(Guid id)
    {
        return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _dbContext.Regions.AddAsync(region);
        await _dbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if(existingRegion == null)
        {
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.ImageURL = region.ImageURL; 

        await _dbContext.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region> DeleteAsync(Guid id)
    {
        var existRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if(existRegion == null)
        {
            return null;
        }

        _dbContext.Regions.Remove(existRegion);
        await _dbContext.SaveChangesAsync();
        return existRegion;
    }
}

