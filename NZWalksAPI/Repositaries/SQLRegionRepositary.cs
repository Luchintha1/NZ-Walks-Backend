using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public class SQLRegionRepositary : IRegionRepositary
    {
        private readonly NZWalkDbContext _dbContext;

        public SQLRegionRepositary(NZWalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingData = await _dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

            if (existingData == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(existingData);
            await _dbContext.SaveChangesAsync();

            return existingData;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingData = await _dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

            if (existingData == null)
            {
                return null;
            }

            existingData.code = region.code;
            existingData.Name = region.Name;
            existingData.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return existingData;
        }
    }
}
