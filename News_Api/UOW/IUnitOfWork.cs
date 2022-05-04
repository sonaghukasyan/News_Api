using News_Api.DTOs;
using News_Api.Repositories;
using System;

namespace News_Api.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        INewsRepository News { get; }
        IRepository<CategoryDto> Category { get; }
        IRepositoryMany NewsCategory { get; }
    }
}
