using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
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

        public RegionController(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Return all the regions
        // GET: http://localhost:portNumber/api/Region
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get the data from the database
            var regions = dbContext.Regions.ToList();

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

        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get the data from the database
            var region = dbContext.Regions.FirstOrDefault(x => x.id == id);

            if (region == null)
            {
                return NotFound();
            }

            // Send the data to the DTO
            var regionDTO = new RegionDTO
            {
                id = region.id,
                code = region.code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Return the DTO
            return Ok(regionDTO);
        }

        // POST: Create New Region
        // POST: http://localhost:portNumber/api/Region

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO in to Domain Model
            var regionDomainModel = new Region
            {
                code = addRegionRequestDTO.code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Use domain model to create a region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map domain model back to DTO

            var regionDTO = new RegionDTO
            {
                id = regionDomainModel.id,
                code = regionDomainModel.code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.id}, regionDTO);

        }

        // PUT: Update Region
        // PUT: http://localhost:portNumber/api/Region/{id}

        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            // Check if region exists

            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.code = updateRegionDTO.code;
            regionDomainModel.Name = updateRegionDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionDTO.RegionImageUrl;


            dbContext.SaveChanges();

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

        // DELETE: Delete Region
        // DELETE: http://localhost:portNumber/api/Region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete Region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            return Ok();
        }

    }
}
