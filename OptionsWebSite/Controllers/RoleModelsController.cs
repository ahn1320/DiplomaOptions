using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Role;
using OptionsWebSite.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace OptionsWebSite.Controllers
{
    public class RoleModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoleModels
        public ActionResult Index()
        {
            var model = db.Roles.AsEnumerable()
                .Select(m => new RoleModels
                {
                    RoleID = m.Id,
                    RoleName = m.Name
                });

            return View(model);
        }


        // GET: RoleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: RoleModels/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleModels roleModels)
        {
            string name = roleModels.RoleName;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(name))
                roleManager.Create(new IdentityRole(name));

            return RedirectToAction("Index");
        }



        // GET: RoleModels/Delete/5
        public ActionResult Delete(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var targetToDel = db.Roles.Where(m => m.Id == id).FirstOrDefault();

            db.Roles.Remove(targetToDel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
