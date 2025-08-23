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

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }
    }
}
