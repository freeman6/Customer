using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override void Remove(客戶資料 entity)
        {
            entity.是否已刪除 = true;
            base.Commit();
        }
        public override IQueryable<客戶資料> GetAll()
        {
            return base.Query(x=>x.是否已刪除 == false);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}