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
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest();
            }
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
                    RegionId = walk.RegionId,
                    Region = new RegionDTO()
                    {
                        id = walk.Region.id,
                        code = walk.Region.code,
                        Name = walk.Region.Name,
                        RegionImageUrl = walk.Region.RegionImageUrl
                    },
                    Difficulty = new DifficultyDTO()
                    {
                        Id = walk.Difficulty.Id,
                        Name = walk.Difficulty.Name
                    }
                });
            }

            return Ok(walksDTO);
        }

        // Get a single Walk
        // Get: api/Walks/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            // Get the data from the database to the Domain Model
            var walksDomainModel = await walkRepositary.GetWalkByIDAsync(id);

            // Map the Domain Model to DTO
            
            if (walksDomainModel == null)
            {
                return NotFound();
            }

            var walksDTO = new WalkDTO()
            {
                Id = walksDomainModel.Id,
                Name = walksDomainModel.Name,
                Description = walksDomainModel.Description,
                LengthInKm = walksDomainModel.LengthInKm,
                WalkImageUrl = walksDomainModel.WalkImageUrl,
                Region = new RegionDTO()
                {
                    id = walksDomainModel.Region.id,
                    code = walksDomainModel.Region.code,
                    Name = walksDomainModel.Region.Name,
                    RegionImageUrl = walksDomainModel.Region.RegionImageUrl
                },
                Difficulty = new DifficultyDTO()
                {
                    Id = walksDomainModel.Difficulty.Id,
                    Name = walksDomainModel.Difficulty.Name,
                }
            };

            return Ok(walksDTO);
        }

        // Update a single Walk
        // Put: api/Walk/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalksDTO updateWalksDTO)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to domain
                var walksDomainModel = new Walk()
                {
                    Id = id,
                    Name = updateWalksDTO.Name,
                    Description = updateWalksDTO.Description,
                    LengthInKm = updateWalksDTO.LengthInKm,
                    WalkImageUrl = updateWalksDTO.WalkImageUrl,
                    DifficultyId = updateWalksDTO.DifficultyId,
                    RegionId = updateWalksDTO.RegionId
                };

                // Update the database
                walksDomainModel = await walkRepositary.UpdateWalkAsync(id, walksDomainModel);

                if (walksDomainModel == null)
                {
                    return NotFound();
                }

                // Map from Domain to DTO
                var walksDTO = new WalkDTO()
                {
                    Id = walksDomainModel.Id,
                    Name = walksDomainModel.Name,
                    Description = walksDomainModel.Description,
                    LengthInKm = walksDomainModel.LengthInKm,
                    WalkImageUrl = walksDomainModel?.WalkImageUrl,
                    Difficulty = new DifficultyDTO()
                    {
                        Id = walksDomainModel.Difficulty.Id,
                        Name = walksDomainModel.Difficulty.Name
                    },
                    Region = new RegionDTO()
                    {
                        id = walksDomainModel.Region.id,
                        code = walksDomainModel.Region.code,
                        Name = walksDomainModel.Region.Name,
                        RegionImageUrl = walksDomainModel.Region.RegionImageUrl
                    }
                };

                return Ok(walksDTO);
            }
            else
            {
                return NotFound();
            }
        }

        // Delete selected walk
        // DELETE: api/walk/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            // Delete the data

            var deletedData = await walkRepositary.DeleteWalkAsync(id);

            if (deletedData == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
