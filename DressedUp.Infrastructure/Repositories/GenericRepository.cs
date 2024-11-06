using System.Linq.Expressions;
using DressedUp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            // Null durumunu ele alın, örneğin hata döndürün veya default değer verin
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return entity;        
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync(); // Değişiklikleri kaydeder

    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
         _context.SaveChangesAsync(); // Değişiklikleri kaydeder

    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
         _context.SaveChangesAsync(); // Değişiklikleri kaydeder

    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy, bool ascending = true)
    {
        var query = _dbSet.AsQueryable();

        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    
    public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }
}
