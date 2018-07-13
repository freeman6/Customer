using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class viwCustomerSummaryRepository : EFRepository<viwCustomerSummary>, IviwCustomerSummaryRepository
	{

	}

	public  interface IviwCustomerSummaryRepository : IRepository<viwCustomerSummary>
	{

	}
}