using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data;
using System.Threading.Tasks;

namespace DocumentManagement.Common.GenericRepository
{
    public class GenericRepository<TC, TContext> : IGenericRepository<TC>
        where TC : class
        where TContext : DbContext
    {
        protected readonly TContext Context;
        internal readonly DbSet<TC> DbSet;
        protected IUnitOfWork<TContext> _uow;
        protected GenericRepository(IUnitOfWork<TContext> uow
            )
        {
            Context = uow.Context;
            this._uow = uow;
            DbSet = Context.Set<TC>();
        }
        public IQueryable<TC> GetAll() 
        {  
            return DbSet.AsQueryable(); 
        }
        public TC GetById(object id)
        {
            return DbSet.Find(id);
        }
        public async Task<TC> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task AddAsync(TC entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TC entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
        public IQueryable<TC> Include(params Expression<Func<TC, object>>[] includeProperties)
        {
            IQueryable<TC> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        public IQueryable<TC> GetByCondition(Expression<Func<TC, bool>> expression)
        {
            return DbSet.Where(expression).AsQueryable();
        }
        public bool Any(Expression<Func<TC, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public IQueryable<TC> All => Context.Set<TC>();
        
        public void Add(TC entity)
        {
            Context.Add(entity);
        }

        public IQueryable<TC> AllIncluding(params Expression<Func<TC, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        //public async Task<IEnumerable<TC>> AllIncludingAsync(params Expression<Func<TC, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    IEnumerable<TC> results = await query.ToListAsync();
        //    return results;
        //}

        public IQueryable<TC> FindByInclude(Expression<Func<TC, bool>> predicate, params Expression<Func<TC, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate);
        }

        //public async Task<IEnumerable<TC>> FindByIncludeAsync(Expression<Func<TC, bool>> predicate, params Expression<Func<TC, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    IEnumerable<TC> results = await query.Where(predicate).ToListAsync();
        //    return results;
        //}

        //public  IQueryable<TC> FindByInclude(Expression<Func<TC, bool>> predicate, params Expression<Func<TC, object>>[] includeProperties)
        //{
        //    var query = GetAllIncluding(includeProperties);
        //    return query.Where(predicate);
        //}

        public IQueryable<TC> FindBy(Expression<Func<TC, bool>> predicate)
        {
            IQueryable<TC> queryable = DbSet.AsNoTracking();
            return queryable.Where(predicate);
        }

        //public IQueryable<TC> FindOnly(Expression<Func<TC, bool>> predicate)
        //{
        //    IQueryable<TC> queryable = DbSet.AsNoTracking();
        //    return queryable.Where(predicate);
        //}

        //public async Task<IEnumerable<TC>> FindByAsync(Expression<Func<TC, bool>> predicate)
        //{
        //    IQueryable<TC> queryable = DbSet.AsNoTracking();
        //    IEnumerable<TC> results = await queryable.Where(predicate).ToListAsync();
        //    return results;
        //}

        private IQueryable<TC> GetAllIncluding(params Expression<Func<TC, object>>[] includeProperties)
        {
            IQueryable<TC> queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        public TC Find(Guid id)
        {
            return Context.Set<TC>().Find(id);
        }

        public async Task<TC> FindAsync(Guid id)
        {
            return await Context.Set<TC>().FindAsync(id);
        }
        public virtual void Update(TC entity)
        {
            Context.Update(entity);
        }
        public virtual void UpdateRange(List<TC> entities)
        {
            Context.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<TC> lstEntities)
        {
            Context.Set<TC>().RemoveRange(lstEntities);
        }

        public void AddRange(IEnumerable<TC> lstEntities)
        {
            Context.Set<TC>().AddRange(lstEntities);
        }

        public void InsertUpdateGraph(TC entity)
        {
            Context.Set<TC>().Add(entity);
            //Context.ApplyStateChanges(user);
        }
        public virtual void Delete(Guid id)
        {
            var entity = Context.Set<TC>().Find(id) as BaseEntity;
            if (entity != null)
            {
                entity.IsDeleted = true;
                Context.Update(entity);
            }
        }
        public virtual void Delete(TC entityData)
        {
            var entity = entityData as BaseEntity;
            entity.IsDeleted = true;
            Context.Update(entity);
        }
        public virtual void Remove(TC entity)
        {
            Context.Remove(entity);
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
