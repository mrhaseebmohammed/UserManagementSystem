using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserManagementSystem.Entities;
using UserManagementSystem.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace UserManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private UserManagementSystemEntities db = new UserManagementSystemEntities();

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,Username,Password,Salt,Email,IsEmailVerified,IsActive")] User user)
        public ActionResult Create([Bind(Include = "Username,Password,ConfirmPassword, Email")] UserCreateViewModel userVM)
        {
            if (db.Users.Any(u => u.Email == userVM.Email))
            {
                ModelState.AddModelError("Email", "Email in use");
            }

            if (db.Users.Any(u => u.Username == userVM.Username))
            {
                ModelState.AddModelError("Username", "Username in use");
            }

            if (ModelState.IsValid)
            {

                User user = new User();
                user.Username = userVM.Username;
                user.Email = userVM.Email;
                string pwdToHash = userVM.Password + "*)&h9"; //Security through obscurity
                user.Password = BCryptHelper.HashPassword(pwdToHash, BCryptHelper.GenerateSalt());
                FormsAuthentication.SetAuthCookie(userVM.Username, false);
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(userVM);
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.Name != null)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username, Password")] UserLoginViewModel userLVM)
        {
            if (ModelState.IsValid)
            {
                if (BCryptHelper.CheckPassword(userLVM.Password + "*)&h9", db.Users.First(u => u.Username == userLVM.Username).Password))
                {
                    FormsAuthentication.SetAuthCookie(userLVM.Username, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(userLVM);
        }
    }


    //// GET: Users
    //public ActionResult Index()
    //{
    //    return View(db.Users.ToList());
    //}

    //// GET: Users/Details/5
    //public ActionResult Details(int? id)
    //{
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    User user = db.Users.Find(id);
    //    if (user == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(user);
    //}

    //// GET: Users/Create
    //public ActionResult Create()
    //{
    //    return View();
    //}

    //// POST: Users/Create
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "UserID,Username,Password,Email,IsEmailVerified,IsActive")] User user)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.Users.Add(user);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    return View(user);
    //}

    //// GET: Users/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    User user = db.Users.Find(id);
    //    if (user == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(user);
    //}

    //// POST: Users/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "UserID,Username,Password,Email,IsEmailVerified,IsActive")] User user)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.Entry(user).State = EntityState.Modified;
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }
    //    return View(user);
    //}

    //// GET: Users/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    User user = db.Users.Find(id);
    //    if (user == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(user);
    //}

    //// POST: Users/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //    User user = db.Users.Find(id);
    //    db.Users.Remove(user);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //}

    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        db.Dispose();
    //    }
    //    base.Dispose(disposing);
    //}
}

