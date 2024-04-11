using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkService _walkService;

        public WalksController(IMapper mapper, IWalkService walkService)
        {
            _mapper = mapper;
            _walkService = walkService;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var walkDomainModel = _mapper.Map<Walk>(model);
                await _walkService.CreateAsync(walkDomainModel);
                if (walkDomainModel == null)
                {
                    return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Not Found");
                }
                var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 100)
        {

            var walkdomainModel = await _walkService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (walkdomainModel.Count() > 0 && walkdomainModel != null)
            {

                return Ok(_mapper.Map<List<WalkDto>>(walkdomainModel));
            }

            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Not Found");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkService.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Not Found");
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto model)
        {

            var walkDomainModel = _mapper.Map<Walk>(model);
            walkDomainModel = await _walkService.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Not Found");
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walkDomainModel = await _walkService.DeleteAsnyc(id);
            if (walkDomainModel == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Not Found");
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
    }
}
