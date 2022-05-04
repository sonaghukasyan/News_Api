using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News_Api.DTOs;
using News_Api.Repositories;
using News_Api.UOW;
using System;
using System.Threading.Tasks;

namespace News_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork uow, ILogger<NewsController> logger)
        {
            this._unitOfWork = uow;
            this._logger = logger;
        }

        [HttpPut("Search news by category")]
        public async Task<IActionResult> SearchByCategory([FromBody] CategoryDto category)
        {
            try
            {
                return Ok(await _unitOfWork.News.SearchByCategory(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search news by text")]
        public async Task<IActionResult> SearchByCategory(String text)
        {
            try
            {
                return Ok(await _unitOfWork.News.SearchByText(text));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search news by dates")]
        public async Task<IActionResult> SearchByCategory(DateTime start, DateTime end)
        {
            try
            {
                return Ok(await _unitOfWork.News.SearchByDates(start,end));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get all news")]
        public async Task<IActionResult> Get()
        {
            try
            {
               return Ok( await _unitOfWork.News.Get());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get the news with id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.News.Get(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Save new news")]
        public async Task<IActionResult> Save([FromBody] NewsDto news)
        {
            try
            {
                _logger.LogInformation($"new news is added by title {news.Title}");
                await _unitOfWork.News.Save(news);
                return Ok(await _unitOfWork.News.Get());
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
                _logger.LogInformation($" news with id {id} is deleted.");
                await _unitOfWork.News.Delete(id);
                return Ok(await _unitOfWork.News.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update the news")]
        public async Task<IActionResult> Update(int id,[FromBody] NewsDto news)
        {
            try
            {
                _logger.LogInformation($" news with id {id} is updated.");
                await _unitOfWork.News.Update(id,news);
                return Ok(await _unitOfWork.News.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
