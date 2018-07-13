using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork EF;

        public BaseController()
        {
            EF = RepositoryHelper.GetUnitOfWork();
        }
    }
}