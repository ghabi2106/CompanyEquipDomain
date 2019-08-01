using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Context;
using Models;

namespace Domain.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext context;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking();
            }
            else
            {
                return query.AsNoTracking();
            }
        }


        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
        }


        public void Update(TEntity entity)
        {
            var dbEntityEntry = context.Entry(entity);
            //context.Attach(entity);
            dbSet.Attach(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (context.Entry(entity).State == EntityState.Detached)
            {
                //context.Attach(entity);
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public ResultApiModel<IEnumerable<T>> Paginate<T>(IQueryable<T> query, int page = 0, int take = 0)
        {
            var result = new ResultApiModel<IEnumerable<T>>();
            var total = query.Count();
            query = query.Skip(Math.Max(0, page - 1) * take);
            if (take != 0)
            {
                query = query.Take(take);
            }
            var items = query.ToList();
            result.Data = items;
            result.TotalCount = total;
            result.Success = true;
            return result;
        }

        public bool IsExist(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable().ToList();
        }
        public int count()
        {
            return dbSet.Count();

        }
    }
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int count();
        int SaveChanges();
        ResultApiModel<IEnumerable<T>> Paginate<T>(IQueryable<T> query, int page = 0, int take = 0);
        bool IsExist(Expression<Func<TEntity, bool>> filter);
    }
}
