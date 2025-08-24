using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public interface IWalkRepositary
    {
        Task<Walk?> CreateWalk(Walk walk);

        Task<List<Walk>> GetAllWalksAsync();

        Task<Walk?> GetWalkByIDAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
