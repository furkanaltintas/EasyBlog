using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Data.Repositories.Abstractions;

namespace EasyBlog.Data.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();

    Task<int> SaveAsync();
    int Save();
}