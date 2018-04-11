using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ReimbursementApp.Data.Contracts;
using ReimbursementApp.Model;
using ReimbursementApp.ViewModels;

namespace ReimbursementApp.Controllers.API
{
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
                }).OrderBy(d => d.EmployeeName); ;

            return model;
        }

        [HttpGet("")]
        public IQueryable Get()
        {
            var model = UOW.Employees.GetAll().OrderByDescending(emp => emp.Id).OrderBy(d => d.EmployeeName);
            return model;
        }

        [HttpGet("{id}")]
        public IQueryable<Employee> Get(int id)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.EmployeeId == id).OrderBy(d => d.EmployeeName);
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
                if (i.SignedUp)
                {
                    flag = true;
                }

            }
            return flag;
        }

        [HttpGet("~/api/employee/search/{Key}")]
        public IQueryable<Employee> GetEmployee(string Key)
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e =>
            e.EmployeeName.Contains(Key) ||
            e.Designation.Contains(Key) ||
            e.EmployeeId.ToString().StartsWith(Key)
            ).OrderBy(d => d.EmployeeName);
            return model;
        }
       
        [HttpGet("~/api/employee/GetPendingApprovals/")]
        public IQueryable<Employee> GetPendingApprovals()
        {
            IQueryable<Employee> model = UOW.Employees.GetAll().Where(e => e.SignedUp == false).OrderBy(d => d.EmployeeName);
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
                SignedUp = false,
                RoleName = string.Empty

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
            //check if role is other than user, then insert user in approvers list as well.
            //approval flow
            if (employeeViewModel.approvalRequired)
            {   //Insert employees into approver's list other than user role.
                if (employeeViewModel.RoleName.ToLower() != "user")
                {
                    //Insert employee in approvers' list
                    var approverObj = new ApproverList
                    {
                        ApproverId = employeeViewModel.EmployeeId,
                        Name = employeeViewModel.EmployeeName
                    };
                    employeeViewModel.SignedUp = true;
                    UOW.Employees.Update(employeeViewModel);
                    UOW.ApproverLists.Add(approverObj);
                    UOW.Commit();
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                employeeViewModel.SignedUp = true;
                UOW.Employees.Update(employeeViewModel);
                UOW.Commit();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
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
