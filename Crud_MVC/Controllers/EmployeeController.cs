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
                    var empList = (from m in db.Employees
                                   join cn in db.Countries on m.CountryId equals cn.CountryId
                                   join st in db.States on m.StateId equals st.StateId
                                   join ct in db.Cities on m.CityId equals ct.CityId
                                   select new 
                                   {
                                       m.Name,
                                       m.Position,
                                       m.Office,
                                       m.Age,
                                       m.Salary,
                                       cn.CountryName,
                                       st.StateName,
                                       ct.CityName
                                   }).ToList();
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
                using (DBModel db = new DBModel())
                {
                    ViewBag.Country = new SelectList(db.Countries.ToList(), "CountryId", "CountryName");
                }
                return View();
            }
            else
            {
                using (DBModel db = new DBModel())
                {
                    ModelEmployee emp = new ModelEmployee();
                    ViewBag.Country = new SelectList(db.Countries.ToList(), "CountryId", "CountryName");
                    emp = db.Employees.Where(x => x.EmployeeId == id).Select(x => new ModelEmployee
                    {
                        EmployeeId = x.EmployeeId,
                        Name = x.Name,
                        Position = x.Position,
                        Office = x.Office,
                        Age = x.Age,
                        Salary = x.Salary,
                        CountryId = x.CountryId,
                        StateId = x.StateId,
                        CityId = x.CityId

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
                        emp.CountryId = memp.CountryId;
                        emp.StateId = memp.StateId;
                        emp.CityId = memp.CityId;
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
                        empp.CountryId = memp.CountryId;
                        empp.StateId = memp.StateId;
                        empp.CityId = memp.CityId;
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
                    return Json(new { success = true, message = "Successfully Delete..!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetState(int CountryId)
        {
            using (DBModel db = new DBModel())
            {
                IEnumerable<SelectListItem> States = db.States.Where(x => x.CountryId == CountryId).Select(c => new SelectListItem
                {
                    Value = c.StateId.ToString(),
                    Text = c.StateName
                }).ToList();
                return Json(States, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCity(int StateId)
        {
            using (DBModel db = new DBModel())
            {
                IEnumerable<SelectListItem> City = db.Cities.Where(x => x.StateId == StateId).Select(c => new SelectListItem
                {
                    Value = c.CityId.ToString(),
                    Text = c.CityName
                }).ToList();
                return Json(City, JsonRequestBehavior.AllowGet);
            }
        }
    }
}