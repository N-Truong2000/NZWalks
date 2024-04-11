using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Services;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.API.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionService _regionService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionService regionService, IMapper autoMapper, ILogger<RegionsController> logger)
        {
            _regionService = regionService;
            _autoMapper = autoMapper;
            _logger = logger;
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllv2()
        {
            try
            {
                _logger.LogError("abacac");
                _logger.LogWarning("123123");

                _logger.LogInformation("GetAllRegions action method was invoked v22222222222222222");
                var regionsDomain = await _regionService.GetAllAsync();

                _logger.LogInformation($"Finished GetAllRegions request with data:{JsonSerializer.Serialize(regionsDomain)}");

                var regionsDto = _autoMapper.Map<List<RegionDto>>(regionsDomain);
                if (regionsDto.Any())
                {
                    return Ok(regionsDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }


            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "No regionsDomain found.");
        } 
        [MapToApiVersion("1.0")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllv1()
        {
            try
            {
                _logger.LogInformation("GetAllRegions action method was invoked v11111111111111111111");
                var regionsDomain = await _regionService.GetAllAsync();

                _logger.LogInformation($"Finished GetAllRegions request with data:{JsonSerializer.Serialize(regionsDomain)}");

                var regionsDto = _autoMapper.Map<List<RegionDto>>(regionsDomain);
                if (regionsDto.Any())
                {
                    return Ok(regionsDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }


            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "No regionsDomain found.");
        }

        [HttpGet("GetById/{id:guid}")]
        [MapToApiVersion("2.0")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsDomain = await _regionService.GetByIdAsync(id);
            //var regionsDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};
            var regionsDto = _autoMapper.Map<RegionDto>(regionsDomain);

            if (regionsDto != null)
            {
                return Ok(regionsDto);
            }
            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Region not found.");
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto model)
        {
            //var regionDomainModel = new Region
            //{
            //    Code = model.Code,
            //    Name = model.Name,
            //    RegionImageUrl = model.RegionImageUrl,
            //};
            if (ModelState.IsValid)
            {
                var regionDomainModel = _autoMapper.Map<Region>(model);
                regionDomainModel = await _regionService.CreateAsync(regionDomainModel);


                //var regionDto = new RegionDto()
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};
                var regionDto = _autoMapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("Update/{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromBody] UpdateRegionRequestDto model, [FromRoute] Guid id)
        {
            //var regionDomainModel = new Region()
            //{
            //    Code = model.Code,
            //    Name = model.Name,
            //    RegionImageUrl = model.RegionImageUrl,
            //};
            if (ModelState.IsValid)
            {

                var regionDomainModel = _autoMapper.Map<Region>(model);

                regionDomainModel = await _regionService.UpdateAsync(id: id, region: regionDomainModel);
                if (regionDomainModel == null)
                {
                    return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Region not found.");
                }
                //var regionDto = new RegionDto()
                //{
                //    Id = id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};
                var regionDto = _autoMapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("Delete/{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionService.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Region not found.");
            }


            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};
            var regionDto = _autoMapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
