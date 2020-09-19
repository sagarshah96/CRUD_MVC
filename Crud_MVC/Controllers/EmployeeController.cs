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
                    List<Employee> empList = db.Employees.ToList();
                    return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                using (DBModel db = new DBModel())
                {
                    ModelEmployee emp = new ModelEmployee();

                    emp = db.Employees.Where(x => x.EmployeeId == id).Select(x => new ModelEmployee
                    {
                        EmployeeId = x.EmployeeId,
                        Name = x.Name,
                        Position = x.Position,
                        Office = x.Office,
                        Age = x.Age,
                        Salary = x.Salary
                    }).FirstOrDefault();
                    return View(emp);

                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ModelEmployee memp)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Employee emp = new Employee();
                    if (memp.EmployeeId == 0)
                    {
                        emp.Name = memp.Name;
                        emp.Position = memp.Position;
                        emp.Office = memp.Office;
                        emp.Age = memp.Age;
                        emp.Salary = memp.Salary;
                        db.Employees.Add(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var empp = db.Employees.Find(memp.EmployeeId);
                        empp.Name = memp.Name;
                        empp.Position = memp.Position;
                        empp.Office = memp.Office;
                        empp.Age = memp.Age;
                        empp.Salary = memp.Salary;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Employee emp = new Employee();
                    emp = db.Employees.Find(id);
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    return Json(new { success = true,message = "Successfully Delete..!"},JsonRequestBehavior.AllowGet);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}