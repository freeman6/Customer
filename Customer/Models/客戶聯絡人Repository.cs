using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override void Remove(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
            base.Commit();
        }
        public override IQueryable<客戶聯絡人> GetAll()
        {
            return base.Query(x => x.是否已刪除 == false);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}