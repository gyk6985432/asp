using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSevenDays.Models;
using DataAccessLayer;


namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            #region
            //List<Employee> employees = new List<Employee>();
            //Employee emp = new Employee();
            //emp.FirstName = "john";
            //emp.LastName = "fernado";
            //emp.Salary = 14000;
            //employees.Add(emp);

            //emp = new Employee();
            //emp.FirstName = "Kate";
            //emp.LastName = "Machal";
            //emp.Salary = 16000;
            //employees.Add(emp);

            //emp = new Employee();
            //emp.FirstName = "rebort";
            //emp.LastName = "jackson";
            //emp.Salary = 24000;
            //employees.Add(emp);
            #endregion

            SalesERPDAL salesDal = new SalesERPDAL();
            List<Employee> employees = new List<Employee>();
            try { return salesDal.Employees.ToList(); }
            catch
            {
                return employees;
            }
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            try
            {
                salesDal.Employees.Add(e);
                salesDal.SaveChanges();
            }
            catch (Exception)
            {
            }
            return e;

        }

        //public bool IsValidUser(UserDetails u)
        //{
        //    if (u.UserName=="Admin"&&u.Password=="Admin")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName=="Admin"&&u.Password=="Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if(u.UserName=="gyk"&&u.Password=="123456")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }
    }
}