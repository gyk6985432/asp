using BusinessLayer;
using Models;
using MVCSevenDays.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace MVCSevenDays.Areas.SPA.Controllers
{
    public class SPABulkUploadController : AsyncController
    {
        // GET: SPA/SPABulkUpload
        [AdminFilter]
        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>(() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            EmployeeListViewModel vm = new EmployeeListViewModel();
            vm.Employees = new List<EmployeeViewModel>();
            foreach (Employee item in employees)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = item.FirstName + "&" + item.LastName;
                evm.Salary = item.Salary.ToString("C");
                if (item.Salary>15000)
                {
                    evm.SalaryColor = "red";
                }
                else
                {
                    evm.SalaryColor = "blue";
                }
                vm.Employees.Add(evm);
            }
            return Json(vm);
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            //csvreader.ReadLine();
            while (!csvreader.EndOfStream) 
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }
    }
}