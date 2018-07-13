

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Customer.Models
{ 
	public interface IRepository<T> 
	{
		IUnitOfWork UnitOfWork { get; set; }
		IQueryable<T> GetAll();
		IQueryable<T> Query(Expression<Func<T, bool>> expression);
		T GetSingle(Expression<Func<T, bool>> expression);
		void Create(T entity);
		bool Exists(Expression<Func<T, bool>> expression);
		void Remove(T entity);
		void Remove(IQueryable<T> entity);
		void ExecuteCommand(string sqlCmd, params object[] para);
		void SetModifyStatus(T entity);
		List<T> Execute(string sqlCmd, params object[] para);
		void Commit();
	}
}

