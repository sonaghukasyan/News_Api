using News_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Api.Repositories
{
    public interface INewsRepository: IRepository<NewsDto>
    {
        Task<IEnumerable<NewsDto>> SearchByDates(DateTime startDate, DateTime endDate);
        Task<IEnumerable<NewsDto>> SearchByCategory(CategoryDto category);
        Task<IEnumerable<NewsDto>> SearchByText(String text);

    }
}
