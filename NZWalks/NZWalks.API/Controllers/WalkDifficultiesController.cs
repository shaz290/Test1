using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();
            //Convert Domain to DTOs
            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);
            return Ok(walkDifficultiesDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }
            //Convert Domain to DTOs
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Validate incoming request
            if(!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            //Call Repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            //Convert Domain to DTO

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //Return Response
            return CreatedAtAction(nameof(GetWalkDifficultyById),
                new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Validate Incoming request
            if(!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            //Call Repository to Update

            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            //Convert Domain to DTO

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            // return Response

            return Ok(walkDifficultyDTO);



        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }

        #region MyRegion
        private bool ValidateAddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest), $"{nameof(addWalkDifficultyRequest)} Cannot be Empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code), $"{nameof(addWalkDifficultyRequest.Code)} Cannot be Empty");
                return false;
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }


        private bool ValidateUpdateWalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest), $"{nameof(updateWalkDifficultyRequest)} Cannot be Empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code), $"{nameof(updateWalkDifficultyRequest.Code)} Cannot be Empty");
                return false;
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


