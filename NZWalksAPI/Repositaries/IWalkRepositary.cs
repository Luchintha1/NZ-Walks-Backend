using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public interface IWalkRepositary
    {
        Task<Walk?> CreateWalk(Walk walk);

        Task<List<Walk>> GetAllWalksAsync();
    }
}
