using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public interface IRegionRepositary
    {
        // Get all Regions
        Task<List<Region>> GetAllAsync();

        // Get a Region
        Task<Region?> GetByIdAsync(Guid id);

        // Create New Region
        Task<Region> CreateAsync(Region region);

        // Update Region
        Task<Region?> UpdateAsync(Guid id, Region region);

        // Delete Region
        Task<Region?> DeleteRegionAsync(Guid id);
    }
}
