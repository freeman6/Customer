using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override void Remove(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
            base.Commit();
        }
        public override IQueryable<客戶銀行資訊> GetAll()
        {
            return base.Query(x => x.是否已刪除 == false);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}