using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class HomeController : BaseController
    {
        IviwCustomerSummaryRepository viwCustomerSummary;

        public ActionResult Index()
        {
            viwCustomerSummary = RepositoryHelper.GetviwCustomerSummaryRepository();

            return View(viwCustomerSummary.GetAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}