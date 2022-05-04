using Microsoft.EntityFrameworkCore;
using News_Api.DTOs;
using News_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDBContext _context;
        public NewsRepository(NewsDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var news = await _context.News.SingleOrDefaultAsync(n => n.NewsId == id);
                if (news == null)
                {
                    throw new Exception("No news found with that id");
                }

                var newsCategories = from n in _context.CategoryNews
                                     where n.NewsId == id
                                     select n;
                if (await newsCategories.AnyAsync())
                {
                    throw new Exception("First delete news-category pairs with this id.");
                }

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("More than one news have same id");
            }
        }

        public async Task<NewsDto> Get(int id)
        {
            try
            {
                var news = await (from n in _context.News
                                  where n.NewsId == id
                                  select new NewsDto
                                  {
                                      Title = n.Title,
                                      Text = n.Text,
                                      DateAdded = n.DateAdded
                                  }).SingleOrDefaultAsync();

                if (news == null)
                {
                    throw new Exception("No news found with that id");
                }

                return news;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("More than one news have same id");
            }
        }

        public async Task<IEnumerable<NewsDto>> Get()
        {
            if (await _context.News.CountAsync() == 0)
            {
                throw new Exception("no list found.");
            }
            var news = from n in _context.News
                       select new NewsDto
                       {
                           Title = n.Title,
                           Text = n.Text,
                           DateAdded = n.DateAdded
                       };


            return news;
        }

        public async Task<bool> Save(NewsDto entity)
        {
            News news = new News
            {
                Text = entity.Text,
                Title = entity.Title,
                DateAdded = entity.DateAdded,
            };

            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, NewsDto entity)
        {
            try
            {
                var news = await _context.News.SingleOrDefaultAsync(n => n.NewsId == id);
                if (news == null)
                {
                    throw new Exception("No news found with that id");
                }

                news.DateAdded = entity.DateAdded;
                news.Title = entity.Title;
                news.Text = entity.Text;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("More than one news have same id");
            }
        }

        public async Task<IEnumerable<NewsDto>> SearchByCategory(CategoryDto category)
        {
            var news = from cn in _context.CategoryNews
                       join newss in _context.News
                       on cn.NewsId equals newss.NewsId
                       join categoryy in _context.Categories
                       on category.Name equals categoryy.Name
                       select new NewsDto
                       {
                           Title = newss.Title,
                           Text = newss.Text,
                           DateAdded = newss.DateAdded
                       };

            if (news == null)
            {
                throw new Exception("No news found with that category");

            }

            return await news.ToListAsync();
        }

        public async Task<IEnumerable<NewsDto>> SearchByDates(DateTime startDate, DateTime endDate)
        {
            var news = from n in _context.News
                       where n.DateAdded >= startDate && n.DateAdded <= endDate
                       select new NewsDto
                       {
                           Title = n.Title,
                           Text = n.Text,
                           DateAdded = n.DateAdded
                       };


            if (news == null)
            {
                throw new Exception("No news found with those dates");

            }

            return await news.ToListAsync();
        }

        public async Task<IEnumerable<NewsDto>> SearchByText(String text)
        {
            var news = from n in _context.News
                       where n.Title.Contains(text) || n.Text.Contains(text)
                       select new NewsDto
                       {
                           Title = n.Title,
                           Text = n.Text,
                           DateAdded = n.DateAdded
                       };


            if (news == null)
            {
                throw new Exception("No news found with that text");

            }

            return await news.ToListAsync();
        }
    }
}
