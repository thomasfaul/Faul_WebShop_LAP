using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace ClonestoneMVC.Controllers
{
    public class ProfileController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var tbllogins = db.tbllogins.Include(t => t.tblperson);
            return View(tbllogins.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbllogin tbllogin = db.tbllogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idlogin,email,passcode")] tbllogin tbllogin)
        {
            if (ModelState.IsValid)
            {
                db.tbllogins.Add(tbllogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname", tbllogin.idlogin);
            return View(tbllogin);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProfileData pd = new ProfileData();

            tbllogin l = db.tbllogins.Find(id);

            pd.IdUser = l.idlogin;

            return View(pd);
            
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileData pd)
        {   
            if (ModelState.IsValid)
            {
                if (pd.Password != null)
                {
                    string hashpass = getHashSha512(pd.Password);

                    tbllogin l = db.tbllogins.Find(pd.IdUser);
                    l.passcode = hashpass;
                    //db.Entry(l).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["pchange"] = "Password changed!";
                    return RedirectToAction("Edit", pd.IdUser);
                }

                if (pd.Email != null)
                {
                    tbllogin l = db.tbllogins.Find(pd.IdUser);
                    l.email = pd.Email;
                    db.SaveChanges();

                    TempData["echange"] = "Email changed!";
                    return RedirectToAction("Edit", pd.IdUser);
                }



            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string getHashSha512(string pass)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            SHA512Managed hashstring = new SHA512Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
