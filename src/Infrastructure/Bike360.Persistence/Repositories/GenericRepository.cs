using Bike360.Application.Contracts.Persistence;
using Bike360.Domain;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly Bike360DatabaseContext _context;

    public GenericRepository(Bike360DatabaseContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsDifferentFromDatabaseValue<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        var entityFromDatabase = await _context.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == entity.Id);

        var excludedProperties = new HashSet<string>
        {
            "Id", "TimeCreatedInUtc", "CreatedBy", "TimeLastModifiedInUtc", "LastModifiedBy"
        };

        if (entityFromDatabase == null)
        {
            return true;
        }

        foreach (var property in typeof(TEntity).GetProperties())
        {
            if (excludedProperties.Contains(property.Name))
                continue;

            var currentValue = property.GetValue(entity);

            if (currentValue == default)
                continue;

            var databaseValue = property.GetValue(entityFromDatabase);

            if (!Equals(currentValue, databaseValue))
            {
                return true;
            }
        }

        return false;
    }
}
