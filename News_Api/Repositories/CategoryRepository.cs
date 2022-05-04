using Microsoft.EntityFrameworkCore;
using News_Api.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using News_Api.DTOs;
using System.Collections.Generic;

namespace News_Api.Repositories
{
    public class CategoryRepository: IRepository<CategoryDto>
    {
        private readonly NewsDBContext _context;
        public CategoryRepository(NewsDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var categories = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
                if (categories == null)
                {
                    throw new Exception("No category found with that id");
                }

                var newsCategories = from n in _context.CategoryNews
                                     where n.NewsId == id
                                     select n;
                if (await newsCategories.AnyAsync())
                {
                    throw new Exception("First delete news-category pairs with this id.");
                }

                _context.Categories.Remove(categories);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException )
            {
                throw new Exception("More than one category have same id");
            }
        }

        public async Task<CategoryDto> Get(int id)
        {
            try
            {
                var category = await (from c in _context.Categories
                                  where c.CategoryId == id
                                  select new CategoryDto
                                  {
                                      Name = c.Name
                                  }).SingleOrDefaultAsync();

                if (category == null)
                {
                    throw new Exception("No category found with that id");
                }

                return category;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("More than one category have same id");
            }
        }

        public async Task<IEnumerable<CategoryDto>> Get()
        {
            if (await _context.Categories.CountAsync() == 0)
            {
                throw new Exception("no list found.");
            }
            var category = from c in _context.Categories
                           select new CategoryDto
                           {
                                Name = c.Name
                           };

            return category;
        }

        public async Task<bool> Save(CategoryDto entity)
        {
            Category category = new Category
            {
                Name = entity.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, CategoryDto entity)
        {
            try
            {
                var category = await (from c in _context.Categories
                                      where c.CategoryId == id
                                      select new CategoryDto
                                      {
                                          Name = c.Name
                                      }).SingleOrDefaultAsync();

                if (category == null)
                {
                    throw new Exception("No category found with that id");
                }

                category.Name = entity.Name;
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("More than one category have same id");
            }
           
        }
    }
}
