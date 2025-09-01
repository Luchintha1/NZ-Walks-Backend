using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositaries;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalks.API.Controllers
{
    // http://localhost:portNumber/api/Region
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        private readonly IRegionRepositary regionRepositary;

        public RegionController(NZWalkDbContext dbContext, IRegionRepositary regionRepositary)
        {
            this.dbContext = dbContext;
            this.regionRepositary = regionRepositary;
        }

        // GET: Return all the regions
        // GET: http://localhost:portNumber/api/Region
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            // Get the data from the database
            var regions = await regionRepositary.GetAllAsync();

            // Pass the data to a DTO
            var regionsDTO = new List<RegionDTO>();

            foreach (var region in regions)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    id = region.id,
                    code = region.code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // return DTO
            return Ok(regionsDTO);
        }

        // GET: Return a selected region
        // GET: http://localhost:portNumber/api/Region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get the data from the database
            var regionDomain = await regionRepositary.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Send the data to the DTO
            var regionDTO = new RegionDTO
            {
                id = regionDomain.id,
                code = regionDomain.code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return the DTO
            return Ok(regionDTO);
        }

        // POST: Create New Region
        // POST: http://localhost:portNumber/api/Region

        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            if (ModelState.IsValid)
            {
                // Map or Convert DTO in to Domain Model
                var regionDomainModel = new Region
                {
                    code = addRegionRequestDTO.code,
                    Name = addRegionRequestDTO.Name,
                    RegionImageUrl = addRegionRequestDTO.RegionImageUrl
                };

                // Use domain model to create a region
                regionDomainModel = await regionRepositary.CreateAsync(regionDomainModel);

                // Map domain model back to DTO

                var regionDTO = new RegionDTO
                {
                    id = regionDomainModel.id,
                    code = regionDomainModel.code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.id }, regionDTO);
            }
            else
            {
                return BadRequest();
            }
            

        }

        // PUT: Update Region
        // PUT: http://localhost:portNumber/api/Region/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if region exists

                var regionDomainModel = new Region
                {
                    code = updateRegionDTO.code,
                    Name = updateRegionDTO.Name,
                    RegionImageUrl = updateRegionDTO.RegionImageUrl
                };

                regionDomainModel = await regionRepositary.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Convert Domain Model to DTO
                var regionDTO = new RegionDTO
                {
                    id = regionDomainModel.id,
                    code = regionDomainModel.code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };

                return Ok(regionDTO);
            }
            else
            {
                return BadRequest();
            }
                
        }

        // DELETE: Delete Region
        // DELETE: http://localhost:portNumber/api/Region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepositary.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
