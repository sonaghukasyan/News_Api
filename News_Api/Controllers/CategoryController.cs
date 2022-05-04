using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News_Api.DTOs;
using News_Api.UOW;
using System;
using System.Threading.Tasks;

namespace News_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork uow, ILogger<CategoryController> logger)
        {
            this._unitOfWork = uow;
            this._logger = logger;
        }

        [HttpGet("Get all Categories")]
        public async Task<IActionResult> Get()
        {
            try
            {
               return Ok(await _unitOfWork.Category.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get the category with id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.Category.Get(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Save new news")]
        public async Task<IActionResult> Save([FromBody] CategoryDto category)
        {
            try
            {
                await _unitOfWork.Category.Save(category);
                return Ok(await _unitOfWork.Category.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete news with id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"category is added by id {id} is deleted");
                await _unitOfWork.Category.Delete(id);
                return Ok(await _unitOfWork.Category.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update the news")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
        {
            try
            {
                _logger.LogInformation($"category is added by id {id} is updated");
                await _unitOfWork.Category.Update(id, category);
                return Ok(await _unitOfWork.Category.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
