using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositaries;
using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Controllers
{

    // api/Walks

    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepositary walkRepositary;

        public WalksController(IWalkRepositary walkRepositary)
        {
            this.walkRepositary = walkRepositary;
        }

        // Create Walks
        // POST: Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO walkRequestDTO)
        {

            // Map walks DTO to the Domain Model
            var walksDomainModel = new Walk
            {
                Name = walkRequestDTO.Name,
                Description = walkRequestDTO.Description,
                LengthInKm = walkRequestDTO.LengthInKm,
                WalkImageUrl = walkRequestDTO.WalkImageUrl,
                DifficultyId = walkRequestDTO.DifficultyId,
                RegionId = walkRequestDTO.RegionId
            };

            walksDomainModel = await walkRepositary.CreateWalk(walksDomainModel);

            // Map to the domain model to the DTO

            var walkDTO = new WalkDTO
            {
                Id = walksDomainModel.Id,
                Name = walksDomainModel.Name,
                Description = walksDomainModel.Description,
                LengthInKm = walksDomainModel.LengthInKm,
                WalkImageUrl = walksDomainModel.WalkImageUrl,
                DifficultyId = walksDomainModel.DifficultyId,
                RegionId = walksDomainModel.RegionId
            };

            return Ok(walkDTO);

        }

        // Get Walks
        // Get: All Walks

        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            var walksDomainModel = await walkRepositary.GetAllWalksAsync();

            var walksDTO = new List<WalkDTO>();

            foreach (var walk in walksDomainModel)
            {
                walksDTO.Add(new WalkDTO()
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    DifficultyId = walk.DifficultyId,
                    RegionId = walk.RegionId
                });
            }

            return Ok(walksDTO);
        }
    }
}
