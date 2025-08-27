using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public interface IWalkRepositary
    {
        Task<Walk?> CreateWalk(Walk walk);

        Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, string? sortOn, bool isAscending, 
            int pageNumber = 1, int pageSize = 1000);

        Task<Walk?> GetWalkByIDAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
