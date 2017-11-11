using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExpenseReview.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ReimbursementApp.Helpers;
using ReimbursementApp.Model;

namespace ReimbursementApp.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ApproverController : Controller
    {
        private IExpenseReviewUOW UOW;

        public ApproverController(IExpenseReviewUOW uow)
        {
            UOW = uow;
        }

        [HttpGet("")]
        public IQueryable Get()
        {
            var model = UOW.Approvers.GetAll().OrderByDescending(emp => emp.Id);
            return model;
        }

        [HttpPut("")]
        public async Task<HttpResponseMessage> Put([FromBody]Employee employee)
        {
            var empl = UOW.Employees.GetAll().Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            empl.SignedUp = true;
            UOW.Employees.Update(empl);
            UOW.Commit();
            var manager = UOW.Employees.GetAll().Where(emp => emp.UserName.Equals(User.Identity.Name));
            var managerName = manager.FirstOrDefault().EmployeeName;
            //TODO Change my name with employee name
            await EmailHelper.SendEmailAsync("rahul.sahay@kdi.kongsberg.com", "Sign Up Approved by:- " + managerName, "Now, you can use the system thanks!");
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

    }
}
