using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.SPA;
using OldViewModel = ViewModel;
using MVCSevenDays.Filters;

namespace MVCSevenDays.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            //EmployeeListViewModel v = new EmployeeListViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "StepByStepSchools";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index",v);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (Employee item in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = item.FirstName + "|" + item.LastName;
                empViewModel.Salary = item.Salary.ToString("C");
                if (item.Salary>15000)
                {
                    empViewModel.SalaryColor = "blue";
                }
                else
                {
                    empViewModel.SalaryColor = "red";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            return PartialView("EmployeeList", employeeListViewModel);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee",v);
        }

        public ActionResult SaveEmployee(Employee emp)
        {
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            empBal.SaveEmployee(emp);
            EmployeeViewModel empViewModel = new EmployeeViewModel();
            empViewModel.EmployeeName = emp.FirstName + "|" + emp.LastName;
            empViewModel.Salary = emp.Salary.ToString("C");
            if (emp.Salary>15000)
            {
                empViewModel.SalaryColor = "red";
            }
            else
            {
                empViewModel.SalaryColor = "blue";
            }
            return Json(empViewModel);
        }
    }
}