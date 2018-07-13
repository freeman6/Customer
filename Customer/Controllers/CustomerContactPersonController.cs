using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
using Customer.Service;

namespace Customer.Controllers
{
    public class CustomerContactPersonController : BaseController
    {
        //private Entities db = new Entities();
        客戶聯絡人Repository customerContactPerson;
        客戶資料Repository customer;

        public CustomerContactPersonController()
        {
            customerContactPerson = RepositoryHelper.Get客戶聯絡人Repository();
            customer = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: CustomerContactPerson
        public ActionResult Index()
        {
            var result = customerContactPerson.GetAll().Include(x => x.客戶資料);
            var i = 0;
            SelectList selectList = new SelectList(customerContactPerson.GetAll().Select(x=> new { id = i+1,  職稱 = x.職稱 }).Distinct().ToList(), "職稱", "職稱");
            ViewBag.contactTitle = selectList;

            return View(result);
        }

        // GET: CustomerContactPerson/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 result = customerContactPerson.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: CustomerContactPerson/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerContactPerson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                customerContactPerson.Create(data);
                customerContactPerson.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        // GET: CustomerContactPerson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶聯絡人 result = customerContactPerson.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", result.客戶Id);
            return View(result);
        }

        // POST: CustomerContactPerson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                customerContactPerson.SetModifyStatus(data);
                customerContactPerson.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(customer.GetAll(), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        // GET: CustomerContactPerson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶聯絡人 result = customerContactPerson.GetSingle(x=>x.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: CustomerContactPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 result = customerContactPerson.GetSingle(x=>x.Id == id);
            customerContactPerson.Remove(result);
            customerContactPerson.Commit();
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
                return View("Index", customerContactPerson.GetAll());
            }

            if (customerContactPerson.Exists(x => x.姓名.Contains(keyword)) == false)
            {
                return HttpNotFound();
            }

            IEnumerable<客戶聯絡人> result = customerContactPerson.Query(x => x.姓名.Contains(keyword));
            return View("Index", result);
        }

        [HttpPost]
        public ActionResult SearchByContactTitle(string Title)
        {
            var i = 0;

            if (string.IsNullOrEmpty(Title))
            {
                return View("Index", customerContactPerson.GetAll());
            }

            if (customerContactPerson.Exists(x => x.職稱.Contains(Title)) == false)
            {
                return HttpNotFound();
            }

            IEnumerable<客戶聯絡人> result = customerContactPerson.Query(x => x.職稱.Contains(Title));

            SelectList selectList = new SelectList(customerContactPerson.GetAll().Select(x => new { id = i + 1, 職稱 = x.職稱 }).Distinct().ToList(), "職稱", "職稱");
            ViewBag.contactTitle = selectList;

            return View("Index", result);
        }
    }
}
