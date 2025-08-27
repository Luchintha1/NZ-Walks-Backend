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

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, 
            string? sortOn, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks =  dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortOn) == false)
            {
                if (sortOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortOn.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
