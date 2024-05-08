using System.Linq.Expressions;
using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AleeBook.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
        // _db.Categories == dbSet
        _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
        //throw new NotImplementedException();
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query;
        if (tracked)
            query = dbSet;
        else
            query = dbSet.AsNoTracking();

        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties))
            foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp);
        return query.FirstOrDefault();
        //throw new NotImplementedException();
    }

    // Category, CoverType
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(includeProperties))
            foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp);
        return query.ToList();
        //throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
        //throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);

        //throw new NotImplementedException();
    }
}