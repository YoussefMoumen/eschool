using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eschool;
using PagedList;


namespace eschool.Controllers
{
    public class Student_InfosController : Controller
    {
        private eschoolEntities db = new eschoolEntities();
        eschoolEntities context = new eschoolEntities();
        // GET: Student_Infos
        public ActionResult Index()
        {
            //var resultquery = (from d in context.Student_Infos
            //                   select new Student_Infos()
            //                   {
            //                       Student_Fname = d.Student_Fname,
            //                   }).ToList<Student_Infos>();
            try
            {

                var student_Infos = db.Student_Infos.Include(s => s.Student_Classe).Include(s => s.Student_Filiere);
                return View(student_Infos.ToList());
            }
            catch (Exception e)
            {

                ViewBag.detailError = e.Message;
                return View("Error");
            }
            
        }


        public ActionResult Students(int? page, string searchName)
        {
            var student = from s in db.Student_Infos
                          select s;

            if (!String.IsNullOrEmpty(searchName))
            {
                student = student.Where(s => s.Student_Fname.Contains(searchName)
                                       || s.Student_Lname.Contains(searchName));
                return View(student.ToList().ToPagedList(page ?? 1, 9));
            }
             var students = db.Student_Infos.Include(s => s.Student_Classe).Include(s => s.Student_Filiere);
            return View(students.ToList().ToPagedList(page ?? 1, 9));
        }

        // GET: Student_Infos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Infos student_Infos = db.Student_Infos.Find(id);
            if (student_Infos == null)
            {
                return HttpNotFound();
            }
            return View(student_Infos);
        }

        // GET: Student_Infos/Create
        public ActionResult _Create()
        {
            ViewBag.Student_Classe_Id = new SelectList(db.Student_Classe, "Classe_Id", "Classe_Name");
            ViewBag.Student_Filiere_Id = new SelectList(db.Student_Filiere, "FiliereId", "FiliereName");
            return PartialView();
        }

        // POST: Student_Infos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "Student_Fname,Student_Lname,Student_Age,Student_Filiere_Id,Student_Classe_Id,Student_Phone,Student_Email,Student_Photo")] Student_Infos student_Infos)
        {
            if (ModelState.IsValid)
            {
                ViewBag.SuccessMessage = "Change succesfully";
                db.Student_Infos.Add(student_Infos);
                db.SaveChanges();
                return PartialView("_Create");
            }
            
            ViewBag.Student_Classe_Id = new SelectList(db.Student_Classe, "Classe_Id", "Classe_Name", student_Infos.Student_Classe_Id);
            ViewBag.Student_Filiere_Id = new SelectList(db.Student_Filiere, "FiliereId", "FiliereName", student_Infos.Student_Filiere_Id);
            return PartialView(student_Infos);
        }

        // GET: Student_Infos/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Infos student_Infos = db.Student_Infos.Find(id);
            if (student_Infos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student_Classe_Id = new SelectList(db.Student_Classe, "Classe_Id", "Classe_Name", student_Infos.Student_Classe_Id);
            ViewBag.Student_Filiere_Id = new SelectList(db.Student_Filiere, "FiliereId", "FiliereName", student_Infos.Student_Filiere_Id);
            return PartialView(student_Infos);
        }

        // POST: Student_Infos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "Student_Id,Student_Fname,Student_Lname,Student_Age,Student_Filiere_Id,Student_Classe_Id,Student_Phone,Student_Email,Student_Photo")] Student_Infos student_Infos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Infos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_Classe_Id = new SelectList(db.Student_Classe, "Classe_Id", "Classe_Name", student_Infos.Student_Classe_Id);
            ViewBag.Student_Filiere_Id = new SelectList(db.Student_Filiere, "FiliereId", "FiliereName", student_Infos.Student_Filiere_Id);
            return PartialView(student_Infos);
        }
        // GET: Student_Infos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Infos student_Infos = db.Student_Infos.Find(id);
            if (student_Infos == null)
            {
                return HttpNotFound();
            }
            return View(student_Infos);
        }

        // POST: Student_Infos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Infos student_Infos = db.Student_Infos.Find(id);
            db.Student_Infos.Remove(student_Infos);
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
