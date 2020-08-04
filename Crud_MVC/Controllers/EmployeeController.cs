using Crud_MVC.Data;
using Crud_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    List<Employee> empList = db.Employees.ToList<Employee>();
                    return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrEdit(ModelEmployee memp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Employee emp = new Employee();
                    emp.Name = memp.Name;
                    emp.Position = memp.Position;
                    emp.Office = memp.Office;
                    emp.Age = memp.Age;
                    emp.Salary = memp.Salary;
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
    }
}