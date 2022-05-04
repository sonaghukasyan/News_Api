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
    public class NewsCategoryController : ControllerBase
    {

        private readonly ILogger<NewsCategoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public NewsCategoryController(IUnitOfWork uow, ILogger<NewsCategoryController> logger)
        {
            this._unitOfWork = uow;
            this._logger = logger;
        }

        [HttpGet("Get all news-ategories")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _unitOfWork.NewsCategory.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get the category-news with ids")]
        public async Task<IActionResult> Get(int catId, int newsId)
        {
            try
            {
                return Ok(await _unitOfWork.NewsCategory.Get(catId, newsId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get the categories of the news")]
        public async Task<IActionResult> GetCategoriesOfNews(int newsId)
        {
            try
            {
                return Ok(await _unitOfWork.NewsCategory.GetCategoriesOfNews(newsId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Save new news-category")]
        public async Task<IActionResult> Save(int catId, int newsId)
        {
            try
            {
                _logger.LogInformation($"categor-news pair with newsDd {newsId} and categorId {catId} is added.");
                await _unitOfWork.NewsCategory.Save(catId, newsId);
                return Ok(await _unitOfWork.NewsCategory.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete news with id")]
        public async Task<IActionResult> Delete(int catId, int newsId)
        {
            try
            {
                _logger.LogInformation($"category-news pair with newsDd {newsId} and categorId {catId} is deleted.");
                await _unitOfWork.NewsCategory.Delete(catId, newsId);
                return Ok(await _unitOfWork.NewsCategory.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
