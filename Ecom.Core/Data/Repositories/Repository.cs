﻿using Microsoft.EntityFrameworkCore;
using ECom.Contracts.Data.Repositories;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace ECom.Core.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public T Get(object id)
        {
            var x = _dbSet.Find(id);
            return x;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _dbSet.Where(condition).AsNoTracking();
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}