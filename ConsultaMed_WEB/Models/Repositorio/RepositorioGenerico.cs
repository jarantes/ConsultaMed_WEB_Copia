using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ConsultaMed_WEB.Models.Repositorio
{
    public class RepositorioGenerico<TEntity> where TEntity : class
    {
        internal CmContext Context;
        internal DbSet<TEntity> DbSet;

        public RepositorioGenerico(CmContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int GetIdByUserName(string username)
        {
            using (var user = new UsersContext())
            {
                var selUserId = (from b in user.UserProfiles
                                 where b.UserName == username
                                 select new { b.UserId }).First();

                var userId = Convert.ToInt32(selUserId.UserId);

                user.Dispose();

                return userId;
            }
        }

    }
}