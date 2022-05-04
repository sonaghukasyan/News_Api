using News_Api.DTOs;
using News_Api.Models;
using News_Api.Repositories;

namespace News_Api.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsDBContext _context;

        public UnitOfWork()
        {
            _context = new NewsDBContext();
            News = new NewsRepository(_context);
            Category = new CategoryRepository(_context);
            NewsCategory = new NewsCategoryRepository(_context);
        }
        public INewsRepository News
        {
            get;
            private set;
        }


        public IRepository<CategoryDto> Category
        {
            get;
            private set;
        }

        public IRepositoryMany NewsCategory
        {
            get;
            private set;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
