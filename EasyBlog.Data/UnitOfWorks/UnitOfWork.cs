using EasyBlog.Data.Context;
using EasyBlog.Data.Repositories.Abstractions;
using EasyBlog.Data.Repositories.Concretes;

namespace EasyBlog.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    public int Save() => _dbContext.SaveChanges();

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

    IRepository<T> IUnitOfWork.GetRepository<T>() => new Repository<T>(_dbContext);
}