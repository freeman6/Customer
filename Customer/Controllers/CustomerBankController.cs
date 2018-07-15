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
    public class CustomerBankController : BaseController
    {
        //private Entities db = new Entities();
        客戶銀行資訊Repository customerBank;
        客戶資料Repository customer;

        public CustomerBankController()
        {
            customerBank = RepositoryHelper.Get客戶銀行資訊Repository();
            customer = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: CustomerBank
        public ActionResult Index()
        {
            var result = customerBank.GetAll().Include(x=>x.客戶資料);
            return View(result);
        }

        // GET: CustomerBank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 result = customerBank.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: CustomerBank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerBank/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                customerBank.Create(data);
                customerBank.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        // GET: CustomerBank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 result = customerBank.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", result.客戶Id);
            return View(result);
        }

        // POST: CustomerBank/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                customerBank.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        // GET: CustomerBank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 result = customerBank.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: CustomerBank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 result = customerBank.GetSingle(x=>x.Id == id);
            customerBank.Remove(result);
            customerBank.Commit();

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
                return View("Index", customerBank.GetAll());
            }

            if (customer.Exists(x => x.客戶名稱.Contains(keyword)) == false)
            {
                return HttpNotFound();
            }

            var id = customer.Query(x => x.客戶名稱.Contains(keyword)).Select(x=>x.Id).ToList();
            var result = customerBank.GetAll().Where(x => id.Contains(x.客戶Id));
            return View("Index", result);
        }
    }
}
