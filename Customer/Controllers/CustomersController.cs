using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class CustomersController : BaseController
    {
        客戶資料Repository customer;
        
        public CustomersController()
        {
            customer = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: Customers
        public ActionResult Index()
        {
            //return View(db.客戶資料.ToList());
            return View(customer.GetAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = customer.GetSingle(x=>x.Id == id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                customer.Create(data);
                customer.Commit();

                return RedirectToAction("Index");
            }

            return View(data);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 result = customer.GetSingle(x => x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                customer.SetModifyStatus(data);
                customer.Commit();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 result = customer.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 result = customer.GetSingle(x => x.Id == id);
            customer.Remove(result);
            customer.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EF.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return View("Index", customer.GetAll());
            }

            if (customer.Exists(x => x.客戶名稱.Contains(keyword)) == false)
            {
                return HttpNotFound();
            }

            IEnumerable<客戶資料> result = customer.Query(x => x.客戶名稱.Contains(keyword));
            return View("Index", result);
        }
    }
}
