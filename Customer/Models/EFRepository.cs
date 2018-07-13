using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Customer.Models
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		public IUnitOfWork UnitOfWork { get; set; }
		
		private DbSet<T> _objectset;
		private DbSet<T> ObjectSet
		{
			get
			{
				if (_objectset == null)
				{
					_objectset = UnitOfWork.Context.Set<T>();
				}
				return _objectset;
			}
		}

		public virtual IQueryable<T> GetAll()
		{
			return ObjectSet.AsQueryable();
		}

		public IQueryable<T> Query(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression);
		}

		public T GetSingle(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.SingleOrDefault(expression);
		}

		public bool Exists(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Any(expression);
		}

		public virtual void Create(T entity)
		{
			ObjectSet.Add(entity);
		}

		public virtual void Remove(T entity)
		{
			ObjectSet.Remove(entity);
		}

		public virtual void Remove(IQueryable<T> entity)
		{
			ObjectSet.RemoveRange(entity);
		}

		public virtual void ExecuteCommand(string sqlCmd, params object[] para)
        {
            UnitOfWork.Context.Database.ExecuteSqlCommand(sqlCmd, para);
        }
		
		public List<T> Execute(string sqlCmd, params object[] para)
        {
            return UnitOfWork.Context.Database.SqlQuery<T>(sqlCmd, para).ToList();
        }

		public virtual void Commit()
		{
			UnitOfWork.Commit();
		}
	}
}