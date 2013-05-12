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
            Context = context;
            DbSet = context.Set<TEntity>();
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

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
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

                return userId;
            }
        }

        public int GetClincaByUserName(string username)
        {
            var selClinicaId = (from b in Context.Usuarios
                                    where b.UserName == username
                                    select new { b.ClinicaId }).First();
                var clinicaId = Convert.ToInt32(selClinicaId.ClinicaId);
                return clinicaId;

        }

        public List<Usuario> GetUsersforDoctor(int medicoId, DateTime hoje, DateTime semana)
        {
            var query = (from agend in Context.Agendamentos
                         join usuario in Context.Usuarios on agend.PacienteUserId equals usuario.UserId
                         where
                             agend.MedicoUserId == medicoId 
                             && agend.DataConsulta >= hoje
                             && agend.DataConsulta <= semana
                             && usuario.Perfil == "Paciente"
                         select usuario).Distinct();

            return query.ToList();
        }

        public List<Agendamento> GetAgendforDoctor(int? medicoId, DateTime hoje, DateTime semana)
        {
            var query = (from agend in Context.Agendamentos
                         join user in Context.Usuarios on agend.PacienteUserId equals user.UserId
                         where
                            agend.MedicoUserId == medicoId
                             && agend.DataConsulta >= hoje
                             && agend.DataConsulta <= semana
                             && user.Perfil == "Paciente"
                         select agend);

            return query.ToList();
        }

        public List<Agendamento> GetAgendForClinica(int? clinicaId, DateTime hoje, DateTime semana)
        {
            var query = (from agend in Context.Agendamentos
                         join user in Context.Usuarios on agend.MedicoUserId equals user.UserId
                         join clin in Context.Clinicas on user.ClinicaId equals clin.ClinicaId
                         where 
                            clin.ClinicaId == clinicaId
                             && agend.DataConsulta >= hoje
                             && agend.DataConsulta <= semana
                         select agend);

            return query.ToList();
        }

    }
}