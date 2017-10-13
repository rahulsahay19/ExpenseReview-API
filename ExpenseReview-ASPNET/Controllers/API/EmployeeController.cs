using System.Linq;
using System.Net;
using System.Net.Http;
using ExpenseReview.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ReimbursementApp.Model;
using ReimbursementApp.ViewModels;

namespace ReimbursementApp.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class EmployeeController : Controller
    {
        private IExpenseReviewUOW UOW;

        public EmployeeController(IExpenseReviewUOW uow)
        {
            UOW = uow;
        }

        [HttpGet("~/api/employee/GetEmplNames/")]
        public IQueryable GetEmplNames()
        {
            var model = UOW.Employees.GetAll().OrderByDescending(emp => emp.Id)
                .Select(emp => new EmployeeViewModel
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.EmployeeName
                });

            return model;
        }

        [HttpGet("")]
        public IQueryable Get()
        {
            var model = UOW.Employees.GetAll().OrderByDescending(emp => emp.Id);
            return model;
        }
        [HttpGet("{id}")]
        public IQueryable<Employee> Get(int id)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.EmployeeId == id);
            return model;
        }


        [HttpGet("~/api/employee/GetByName/{EmployeeName}")]
        public IQueryable<Employee> GetByName(string EmployeeName)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.EmployeeName.StartsWith(EmployeeName));
            return model;
        }

        [HttpGet("~/api/employee/GetByUserName/")]
        public IQueryable<Employee> GetByUserName()
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.UserName.StartsWith(User.Identity.Name));
            return model;
        }

        [HttpGet("~/api/employee/CheckUserLoginStatus/")]
        public bool CheckUserLoginStatus()
        {
            bool flag = false;
            var model = UOW.Employees.GetAll().Where(e => e.UserName.StartsWith(User.Identity.Name));
            foreach (var i in model)
            {
                if (i.SignedUp == true)
                {
                    flag = true;
                }

            }
            return flag;
        }


        [HttpGet("~/api/employee/GetByDesignation/{Desig}")]
        public IQueryable<Employee> GetByDesignation(string Desig)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.Designation.StartsWith(Desig));
            return model;
        }

        [HttpGet("~/api/employee/GetByManager/{Manager}")]
        public IQueryable<Employee> GetByManager(string Manager)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.ReportingManager.StartsWith(Manager));
            return model;
        }
        // Post a new Employee
        // POST /api/employee
        //TODO: Need to think on populating Employee and Approver Id
        [HttpPost("")]
        public int Post([FromBody]EmployeeViewModel employee)
        {

            var empObj = new Employee
            {
                //TODO: User can upload image as well
                EmployeeId = employee.EmployeeId,
                UserName = User.Identity.Name,
                EmployeeName = employee.EmployeeName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                //Skillset will be comma-separated, so that later it can be listed as that.
                SkillSet = employee.SkillSet,
                Email = employee.Email,
                DOB = employee.DOB,
                Mobile = employee.Mobile,
                AlternateNumber = employee.AlternateNumber,
                AddressLine1 = employee.AddressLine1,
                AddressLine2 = employee.AddressLine2,
                AddressLine3 = employee.AddressLine3,
                ZipCode = employee.ZipCode,
                Country = employee.Country,
                State = employee.State,
                //Need to check on below logic. May be this should be drop-down for 1st case
                //Below logic will change
                ReportingManager = employee.ReportingManager,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                FatherDOB = employee.FatherDOB,
                MotherDOB = employee.MotherDOB,
                EmergencyContactName = employee.EmergencyContactName,
                EmergencyContactNumber = employee.EmergencyContactNumber,
                EmergencyContactRelation = employee.EmergencyContactRelation,
                EmergencyContactDOB = employee.EmergencyContactDOB,
                //Upon, sign up, this flag will automatically set to true.
                //TODO:- This will remain in False state till approved by reporting manager
                SignedUp = false
            };

            UOW.Employees.Add(empObj);
            UOW.Commit();
            return Response.StatusCode = (int)HttpStatusCode.Created;
        }

        // Update an existing employee
        // PUT /api/employee/
        //TODO:- Need to check if inifinite loop is happening
        [HttpPut("")]
        public HttpResponseMessage Put([FromBody]Employee employeeViewModel)
        {
            UOW.Employees.Update(employeeViewModel);
            UOW.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE api/employee/5
        [HttpDelete("{EmployeeId}")]
        public HttpResponseMessage Delete(int EmployeeId)
        {
            UOW.Employees.Delete(EmployeeId);
            UOW.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }


    }
}
