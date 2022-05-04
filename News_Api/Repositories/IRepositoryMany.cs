using News_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Api.Repositories
{
    public interface IRepositoryMany
    {
        Task<NewsCategoryDto> Get(int categoryId, int newsId);
        Task<IEnumerable<NewsCategoryDto>> Get();
        Task<IEnumerable<NewsCategoryDto>> GetCategoriesOfNews(int newsId);
        Task<bool> Save(int categoryId, int newsId);
        Task<bool> Delete(int categoryId, int newsId);
    }
}
