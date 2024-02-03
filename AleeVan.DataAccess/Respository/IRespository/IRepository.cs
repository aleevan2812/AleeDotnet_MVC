﻿using System.Linq.Expressions;

namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface IRespository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        //void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}