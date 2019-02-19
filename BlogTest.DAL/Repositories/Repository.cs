using System;
using System.Collections.Generic;
using System.Text;
using BlogTest.DAL.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogTest.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity :class
    {
        private readonly DbContext DbContext_;
        public Repository(DbContext dbContext)
        {
            DbContext_ = dbContext;
        }
        protected DbSet<TEntity> DbSet_
        {
            get
            {
                return DbContext_.Set<TEntity>();
            }
        }

        public void Add(TEntity element)
        {
            DbSet_.Add(element);
        }
        public void AddRange(IEnumerable<TEntity> elements)
        {
            DbSet_.AddRange(elements);
        }
        
        public void Remove(TEntity element)
        {
            DbSet_.Remove(element);
        }
        public bool Remove(long id)
        {
            var entity = DbSet_.Find(id);
            if (entity == null)
                return false;
            DbSet_.Remove(entity);
            return true;
        }
        public void RemoveRange(IEnumerable<TEntity> elements)
        {
            DbSet_.RemoveRange(elements);
        }

        public void Update(TEntity element)
        {
            DbSet_.Update(element);
        }

        public TEntity Get(long id)
        {
            return DbSet_.Find(id);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet_.FirstOrDefault(predicate);
        }
        public IEnumerable<TEntity> GetRange(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet_.Where(predicate).ToList();
        }
    }
}
