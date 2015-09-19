using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModel;
using BusinessLayer;
using MVCSevenDays.Filters;

namespace MVCSevenDays.Controllers
{
    public class Customer
    {
        public string Name { get; set; }
        public string Age { get; set; }
    }
    public class EmployeeController : Controller
    {
        // GET: Test
        [Authorize]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            //Employee emp = new Employee();
            //emp.FirstName = "gyk";
            //emp.LastName = "alex";
            //emp.Salary = 2000;
            //ViewBag.Employee = emp;    //ViewBag和ViewData可以相互调用，底层数据结构是同一个
            ////ViewData["Employee"] = emp;//  但这种类型并不好用，有安全和效率低的问题

            //EmployeeViewModel vmEmployee = new EmployeeViewModel();
            //vmEmployee.EmployeeName = emp.FirstName + " " + emp.LastName;
            //vmEmployee.Salary = emp.Salary.ToString("C");

            //if (emp.Salary > 15000)
            //{
            //    vmEmployee.SalaryColor = "yellow";
            //}
            //else
            //{
            //    vmEmployee.SalaryColor = "green";
            //}

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            //employeeListViewModel.UserName = User.Identity.Name;
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (Employee item in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = item.FirstName + "*" + item.LastName;
                empViewModel.Salary = item.Salary.ToString();
                if (item.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";
            //employeeListViewModel.FooterData.Year = DateTime.Now.ToString();

            return View("Index", employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            //employeeListViewModel.UserName = User.Identity.Name; 
            return View("CreateEmployee",employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    {
                        if (ModelState.IsValid)
                        {
                            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                            empBal.SaveEmployee(e);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                            //vm.FooterData = new FooterViewModel();
                           // vm.FooterData.CompanyName = "StepByStepSchools";
                           // vm.FooterData.Year = DateTime.Now.Year.ToString();
                            //vm.UserName = User.Identity.Name;
                            #region
                            //vm.FirstName = e.FirstName;
                            //vm.LastName = e.LastName;
                            //if (e.Salary!=0)
                            //{
                            //    vm.Salary = e.Salary.ToString();
                            //}
                            //else
                            //{
                            //    vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                            //}

                            #endregion
                            return View("CreateEmployee",vm);
                        }
                    }
                case "Cancel":
                    {
                        return RedirectToAction("Index");
                    }
                default:
                    break;
            }
            return new EmptyResult();
        }

        public string GetString()
        {
            Customer customer = new Customer();
            customer.Name = "gyk";
            customer.Age = "26";
            return customer.Name + "/" + customer.Age;
            //return "Hello World is old now. It's time for wassup bro ;)";
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
    }
}