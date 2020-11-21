using CRUDDBFirst.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDDBFirst.Controllers
{
    public class StudentController : Controller
    {
        //This name derived from Connection String in Web.config
        CrudOperationsEntities dbObj = new CrudOperationsEntities();

        // GET: Student
        public ActionResult Student()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                //Student class
                Student studentObj = new Student();
                studentObj.Name = student.Name;
                studentObj.Fname = student.Fname;
                studentObj.Email = student.Email;
                studentObj.Mobile = student.Mobile;
                studentObj.Description = student.Description;

                dbObj.Students.Add(studentObj);
                dbObj.SaveChanges();
            }

            ModelState.Clear();
            return View("Student");
        }

        public ActionResult StudentList()
        {
            var list = dbObj.Students.ToList();
            return View(list);
        }
    }
}