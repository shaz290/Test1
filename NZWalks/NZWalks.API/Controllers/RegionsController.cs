using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRespository regionRespository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRespository regionRespository ,IMapper mapper)
        {
            this.regionRespository= regionRespository;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions=await regionRespository.GetAllAsync();
            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,

            //    };
            //    regionsDTO.Add(regionDTO);
            //});


            var regionsDTO= mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
           var region = await regionRespository.GetAsync(id);


            if(region== null)
            {
                return NotFound();  
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);



            return Ok(regionDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Validate the Request

           //if(!ValidateAddRegionAsync(addRegionRequest))
           // {
           //     return BadRequest(ModelState);

           // }

            //Request(DTO) do Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //Pass detials to Repository
            region = await regionRespository.AddAsync(region);
            //Convert back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id= region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);

         
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
           var region = await regionRespository.DeleteAsync(id);
            //If null not found
            if(region == null)
            {
                return NotFound();
            }
            //Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return Ok response

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateregionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Validate the Incoming request

            //if(!ValidateUpdateregionAsync(updateRegionRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            //Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };
            //Update Region using repository
           region =  await regionRespository.UpdateAsync(id, region);
            //If Null then NotFound
            if (region == null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //Return OK Response
            return Ok(regionDTO);
        }






        #region Private methods

        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest== null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),$"Add Region Data is required.");
                return false;

            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if(addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} Area cannot be less than or equals to Zero.");
            }


            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} Population cannot be less than Zero");
            }
            if(ModelState.ErrorCount> 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateregionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {

            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"Update Region Data is required.");
                return false;

            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} Area cannot be less than or equals to Zero.");
            }


            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} Population cannot be less than Zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion


    }
}
