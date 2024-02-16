using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private ApplicationDbContext ApplicationDbContext { get; }
    private DbSet<T> DbSet { get; }

    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = applicationDbContext.Set<T>();
    }

    public void Delete(T entity)
    {
        if(ApplicationDbContext.Entry(entity).State == EntityState.Detached)
            DbSet.Attach(entity);
        DbSet.Remove(entity);
    }

    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach(var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        query = query.Where(entity => entity.Id == id);

        foreach (var include in includes)
            query = query.Include(include);

        return await query.SingleOrDefaultAsync();
    }

    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var filter in filters)
            query = query.Where(filter);

        foreach (var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<int> InsertAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task SaveChangesAsync()
    {
        await ApplicationDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        DbSet.Attach(entity);
        ApplicationDbContext.Entry(entity).State = EntityState.Modified;
    }
}
