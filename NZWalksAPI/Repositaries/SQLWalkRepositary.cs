using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public class SQLWalkRepositary : IWalkRepositary
    {
        private readonly NZWalkDbContext dbContext;

        public SQLWalkRepositary(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk?> CreateWalk(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var existingData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingData == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingData);
            await dbContext.SaveChangesAsync();

            return existingData;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIDAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingData == null)
            {
                return null;
            }

            existingData.Name = walk.Name;
            existingData.Description = walk.Description;
            existingData.LengthInKm = walk.LengthInKm;
            existingData.WalkImageUrl = walk.WalkImageUrl;
            existingData.DifficultyId = walk.DifficultyId;
            existingData.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
