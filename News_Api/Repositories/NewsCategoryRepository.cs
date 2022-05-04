using Microsoft.EntityFrameworkCore;
using News_Api.DTOs;
using News_Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace News_Api.Repositories
{
    public class NewsCategoryRepository : IRepositoryMany
    {

        private readonly NewsDBContext _context;
        public NewsCategoryRepository(NewsDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int categoryId, int newsId)
        {
            try
            {
                var newsCat = await _context.CategoryNews.SingleOrDefaultAsync(
                    nc => nc.NewsId == newsId && nc.CategoryId == categoryId);

                if (newsCat == null)
                {
                    throw new Exception("No news-category found with that id");
                }


                _context.CategoryNews.Remove(newsCat);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("More than one news-category have same id");
            }
        }

        public async Task<NewsCategoryDto> Get(int categoryId, int newsId)
        {
            var newsCat = await (from cn in _context.CategoryNews
                                 where cn.NewsId == newsId 
                                 && cn.CategoryId == categoryId
                                 join news in _context.News
                                 on cn.NewsId equals news.NewsId
                                 join category in _context.Categories
                                 on cn.CategoryId equals category.CategoryId
                                 select new NewsCategoryDto
                                 {
                                    Category = new CategoryDto
                                    {
                                      Name = category.Name
                                    },
                                 News = new NewsDto
                                 {
                                     Title = news.Title,
                                     DateAdded = news.DateAdded,
                                     Text = news.Text,
                                 }
                                 }).SingleOrDefaultAsync();
            if(newsCat == null)
            {
                throw new Exception("No pair is found");
            }
            return newsCat;
        }

        public async Task<IEnumerable<NewsCategoryDto>> Get()
        {
            if (await _context.CategoryNews.CountAsync() == 0)
            {
                throw new Exception("no list found.");
            }
            var newsCat = from cn in _context.CategoryNews
                          join news in _context.News
                          on cn.NewsId equals news.NewsId
                          join category in _context.Categories
                          on cn.CategoryId equals category.CategoryId
                          select new NewsCategoryDto
                          {
                              Category = new CategoryDto
                              {
                                  Name = category.Name
                              },
                              News = new NewsDto
                              {
                                  Title = news.Title,
                                  DateAdded = news.DateAdded,
                                  Text = news.Text,
                              }
                          };
            return newsCat;
        }

        public async Task<IEnumerable<NewsCategoryDto>> GetCategoriesOfNews(int newsId)
        {

            if (await _context.CategoryNews.CountAsync() == 0)
            {
                throw new Exception("no list found.");
            }
            var newsCat = from cn in _context.CategoryNews
                          where cn.NewsId == newsId
                          join news in _context.News
                          on cn.NewsId equals news.NewsId
                          join category in _context.Categories
                          on cn.CategoryId equals category.CategoryId
                          select new NewsCategoryDto
                          {
                              Category = new CategoryDto
                              {
                                  Name = category.Name
                              },
                              News = new NewsDto
                              {
                                  Title = news.Title,
                                  DateAdded = news.DateAdded,
                                  Text = news.Text,
                              }
                          };
            return newsCat;
        }

        public async Task<bool> Save(int catId, int newsId)
        {
            CategoryNews catNews = new CategoryNews
            {
                NewsId = newsId,
                CategoryId = catId
            };
            _context.CategoryNews.Add(catNews);
            await _context.SaveChangesAsync();
            return true;
        
        }
    }
}
