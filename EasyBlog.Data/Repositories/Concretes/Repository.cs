using EasyBlog.Core.Entities;
using EasyBlog.Data.Context;
using EasyBlog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace EasyBlog.Data.Repositories.Concretes;

public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _dbContext;
    public Repository(AppDbContext dbContext) => _dbContext = dbContext;

    private DbSet<T> Table { get => _dbContext.Set<T>(); }


    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table.Where(predicate);

        if (includeProperties.Any())
            foreach (var include in includeProperties)
                query = query.Include(include);

        return await query.SingleAsync();
    }

    public async Task<T> GetByGuidAsync(Guid id) => await Table.FindAsync(id);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await Table.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null) => await Table.CountAsync(predicate);

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate = null,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;
        if (predicate != null)
            query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var include in includeProperties)
                query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        NullException(entity);
        await Table.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        NullException(entity);
        await Task.Run(() => Table.Update(entity));
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        NullException(entity);
        await Task.Run(() => Table.Remove(entity));
        await SaveChangesAsync();
    }

    public void NullException(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}