using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.Models
{
    public class PageDataViewModel
    {
        public string SortColumnName { get; set; }
        public bool IsDesc { get; set; }
        public int PageNo { get; set; }
    }
}