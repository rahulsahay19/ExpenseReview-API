using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using ExpenseReview.Data.Contracts;
using ExpenseReview.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReimbursementApp.Model;
using ReimbursementApp.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Win32.SafeHandles;
using ReimbursementApp.Helpers;

namespace ReimbursementApp.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]

    public class ExpenseController : Controller
    {
        private IExpenseReviewUOW UOW;
        private IHostingEnvironment Host;
        private DocumentSettings Options;


        public ExpenseController(
            IExpenseReviewUOW uow,
            IHostingEnvironment host,
            IOptionsSnapshot<DocumentSettings> options)
        {
            UOW = uow;
            Host = host;
            Options = options.Value;
        }

        // GET api/expense
        //TODO:- Search all is not working. Need to check
        [HttpGet("")]
        public IQueryable Get()
        {
            var model = UOW.Expenses.GetAll().OrderByDescending(exp => exp.TotalAmount)
                    .Select(exp => new ExpenseViewModel
                    {
                        EmployeeId = exp.Employees.EmployeeId,
                        ApproverId = exp.Approvers.ApproverId,
                        EmployeeName = exp.Employees.EmployeeName,
                        ApproverName = exp.Approvers.Name,
                        ExpenseDate = exp.ExpenseDate,
                        SubmitDate = exp.SubmitDate,
                        ApprovedDate = exp.Approvers.ApprovedDate,
                        Amount = exp.Amount,
                        TotalAmount = exp.TotalAmount,
                        ExpenseDetails = exp.ExpenseDetails,
                        ExpCategory = exp.ExpCategory,
                        TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                        ExpenseId = exp.Id,
                        reason = exp.Reason.Reasoning

                    });
            //  await EmailHelper.SendEmailAsync("rahul.sahay@kdi.kongsberg.com", "dummy subject", "some message");
            return model;

        }

        // GET api/expense/1
        [HttpGet("{id}")]
        public IQueryable Get(int id)
        {
            var model = UOW.Expenses.GetAll().Where(exp => exp.Id == id)
                .Select(exp => new ExpenseViewModel
                {
                    EmployeeId = exp.Employees.EmployeeId,
                    ApproverId = exp.Approvers.ApproverId,
                    EmployeeName = exp.Employees.EmployeeName,
                    ApproverName = exp.Approvers.Name,
                    ExpenseDate = exp.ExpenseDate,
                    SubmitDate = exp.SubmitDate,
                    ApprovedDate = exp.Approvers.ApprovedDate,
                    Amount = exp.Amount,
                    TotalAmount = exp.TotalAmount,
                    ExpenseDetails = exp.ExpenseDetails,
                    ExpCategory = exp.ExpCategory,
                    TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                    ExpenseId = exp.Id,
                    reason = exp.Reason.Reasoning

                });
            return model;

        }


        // GET api/expense/GetByName/saket
        [HttpGet("~/api/expense/GetByName/{EmployeeName}")]
        public IQueryable GetByName(string EmployeeName)
        {
            /* var model = UOW.Expenses.GetAll().Where(e => e.Employees.EmployeeName.StartsWith(EmployeeName));
             return model;*/
            var model = UOW.Expenses.GetAll().Where(e => e.Employees.EmployeeName.StartsWith(EmployeeName))
                .Select(exp => new ExpenseViewModel
                {
                    EmployeeId = exp.Employees.EmployeeId,
                    ApproverId = exp.Approvers.ApproverId,
                    ApprovedDate = exp.Approvers.ApprovedDate,
                    EmployeeName = exp.Employees.EmployeeName,
                    ApproverName = exp.Approvers.Name,
                    ExpenseDate = exp.ExpenseDate,
                    SubmitDate = exp.SubmitDate,
                    ExpCategory = exp.ExpCategory,
                    Amount = exp.Amount,
                    TotalAmount = exp.TotalAmount,
                    ExpenseDetails = exp.ExpenseDetails,
                    TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                    ExpenseId = exp.Id,
                    reason = exp.Reason.Reasoning

                });
            return model;

        }

        // GET api/expense/GetByDesignation/SDE
        [HttpGet("~/api/expense/GetByDesignation/{Desig}")]
        public IQueryable GetByDesignation(string Desig)
        {
            var model = UOW.Expenses.GetAll().Where(e => e.Employees.Designation.StartsWith(Desig))
                .Select(exp => new ExpenseViewModel
                {
                    EmployeeId = exp.Employees.EmployeeId,
                    ApproverId = exp.Approvers.ApproverId,
                    EmployeeName = exp.Employees.EmployeeName,
                    ApproverName = exp.Approvers.Name,
                    ExpenseDate = exp.ExpenseDate,
                    SubmitDate = exp.SubmitDate,
                    ApprovedDate = exp.Approvers.ApprovedDate,
                    Amount = exp.Amount,
                    TotalAmount = exp.TotalAmount,
                    ExpenseDetails = exp.ExpenseDetails,
                    ExpCategory = exp.ExpCategory,
                    TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                    ExpenseId = exp.Id,
                    reason = exp.Reason.Reasoning

                });
            return model;

        }

        // GET api/expense/GetByManager/Dhaval
        [HttpGet("~/api/expense/GetByManager/{Manager}")]
        public IQueryable GetByManager(string Manager)
        {
            var model = UOW.Expenses.GetAll().Where(e => e.Employees.ReportingManager.StartsWith(Manager))
                .Select(exp => new ExpenseViewModel
                {
                    EmployeeId = exp.Employees.EmployeeId,
                    ApproverId = exp.Approvers.ApproverId,
                    EmployeeName = exp.Employees.EmployeeName,
                    ApproverName = exp.Approvers.Name,
                    ExpenseDate = exp.ExpenseDate,
                    SubmitDate = exp.SubmitDate,
                    ApprovedDate = exp.Approvers.ApprovedDate,
                    Amount = exp.Amount,
                    TotalAmount = exp.TotalAmount,
                    ExpenseDetails = exp.ExpenseDetails,
                    ExpCategory = exp.ExpCategory,
                    TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                    ExpenseId = exp.Id,
                    reason = exp.Reason.Reasoning
                });
            return model;

        }

        // GET api/expense/GetByManager/Dhaval
        
        [HttpGet("~/api/expense/MyExpenses/")]
        public IQueryable MyExpenses()
        {
            var model = UOW.Expenses.GetAll().Where(e => e.Employees.UserName.Equals(User.Identity.Name))
                .Select(exp => new ExpenseViewModel
                {
                    EmployeeId = exp.Employees.EmployeeId,
                    ApproverId = exp.Approvers.ApproverId,
                    EmployeeName = exp.Employees.EmployeeName,
                    ApproverName = exp.Approvers.Name,
                    ExpenseDate = exp.ExpenseDate,
                    SubmitDate = exp.SubmitDate,
                    ApprovedDate = exp.Approvers.ApprovedDate,
                    Amount = exp.Amount,
                    TotalAmount = exp.TotalAmount,
                    ExpenseDetails = exp.ExpenseDetails,
                    ExpCategory = exp.ExpCategory,
                    TicketStatus = exp.Status.State.ToString().GetMyEnum().ToString(),
                    ExpenseId = exp.Id,
                    reason = exp.Reason.Reasoning
                });
            return model;

        }
        // Post a new Expense
        // POST /api/expense
        //TODO: MailKit setup for sending mail notofication
        // [EnableCors("CorsPolicy")]

        /*[Authorize(Policy = "PostMethods")]*/
      
        [HttpPost("")]
        public async Task<int> Post([FromBody]ExpenseViewModel expenseViewModel)
        {
            var approver = UOW.Approvers.GetAll().Where(app => app.ApproverId == expenseViewModel.ApproverId);
            var approverName = approver.FirstOrDefault().Name;
            var check = User.Identity.IsAuthenticated;
            //This is to make sure that employee is submitting his/her expense only. Not on behalve
            var employee = UOW.Employees.GetAll().Where(emp => emp.UserName.Equals(User.Identity.Name));
            var emplName = employee.FirstOrDefault().EmployeeName;
            var empId = employee.FirstOrDefault().EmployeeId;
            var expCategory = UOW.ExpenseCategorySets.GetAll()
                .Where(exp => exp.CategoryId == expenseViewModel.ExpCategory.CategoryId);
            var catName = expCategory.FirstOrDefault().Category;

            //Check If there is any participants involve with some flag
            //TODO:- Since, its required to have expense id, Hence if else condition won't work
            //Rather than, it will work in continutaion, after expense id created
            //This flow needs dicusission
            /*        if (expenseViewModel.ParticipantFlag)
                    {
                        //Loop through all the participants, and then create a new expense id
                        for (var i=0;i<expenseViewModel.Participants.Count;i++)
                        {
                            var ExpenseObj = new Expense
                            {
                                Amount = expenseViewModel.Amount,
                                TotalAmount = expenseViewModel.TotalAmount,
                                ExpenseDate = expenseViewModel.ExpenseDate,
                                SubmitDate = expenseViewModel.SubmitDate,
                                Status = new TicketStatus { State = TicketState.Submitted, Reason = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now },
                                Approvers = new Approver { ApproverId = expenseViewModel.ApproverId, Name = approverName },// approver.FirstOrDefault(),
                                Employees = employee.FirstOrDefault(),
                                ExpenseDetails = expenseViewModel.ExpenseDetails,
                                ExpCategory = new ExpenseCategory { CategoryId = expenseViewModel.ExpCategory.CategoryId, Category = catName },
                                Reason = new Reason { EmployeeId = empId, Reasoning = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now },
                                Participants = new[] {new Participant
                                {
                                    ApproverId = expenseViewModel.Participants.FirstOrDefault().ApproverId,
                                    EmployeeId = expenseViewModel.Participants.FirstOrDefault().EmployeeId,
                                    //TODO, How to get expense ID, 
                                    ExpenseId = expenseViewModel.Participants.FirstOrDefault().ExpenseId
                                }}

                            };

                            UOW.Commit();
                        }

                    }
                    else
                    {*/
            var ExpenseObj = new Expense
            {
                Amount = expenseViewModel.Amount,
                TotalAmount = expenseViewModel.TotalAmount,
                ExpenseDate = expenseViewModel.ExpenseDate,
                SubmitDate = expenseViewModel.SubmitDate,
                Status = new TicketStatus { State = TicketState.Submitted, Reason = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now },
                Approvers = new Approver { ApproverId = expenseViewModel.ApproverId, Name = approverName },// approver.FirstOrDefault(),
                Employees = employee.FirstOrDefault(),
                ExpenseDetails = expenseViewModel.ExpenseDetails,
                ExpCategory = new ExpenseCategory { CategoryId = expenseViewModel.ExpCategory.CategoryId, Category = catName },
                Reason = new Reason { EmployeeId = empId, Reasoning = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now }

            };

            UOW.Expenses.Add(ExpenseObj);
            UOW.Commit();
            //  }
            //TODO:- Fetch approvers mail id and based on flow send the mail
            var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expenseViewModel.ApproverId);
            var approverMail = approverEmp.FirstOrDefault().Email;
            await EmailHelper.SendEmailAsync(approverMail, "Ticket Submitted", "New Expense Request Submitted by:- " + emplName);
            return Response.StatusCode = (int)HttpStatusCode.Created;
        }

        /*        [HttpPost("")]
                public IActionResult Post([FromBody]ExpenseViewModel expenseViewModel,IFormFile file)
                {
                    //Make Upload arrangements
                    if (file == null) return BadRequest("File not valid");
                    if (file.Length == 0)  return BadRequest("Empty File");
                    if (file.Length > Options.MaxBytes) return BadRequest("File exceeded 10 MB size!");

                    if (!Options.IsSupported(file.FileName)) return BadRequest("Invalid File Type");
                    var uploadsFolder = Path.Combine(Host.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filepath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var doc = new Documents { DocName = fileName };

                    var approver = UOW.Approvers.GetAll().Where(app => app.ApproverId == expenseViewModel.ApproverId);
                    var approverName = approver.FirstOrDefault().Name;
                   // This is to make sure that employee is submitting his / her expense only. Not on behalve
                      var employee = UOW.Employees.GetAll().Where(emp => emp.UserName.Equals(User.Identity.Name));
                    var emplName = employee.FirstOrDefault().EmployeeName;
                    var empId = employee.FirstOrDefault().EmployeeId;
                    var expCategory = UOW.ExpenseCategorySets.GetAll()
                        .Where(exp => exp.CategoryId == expenseViewModel.ExpCategory.CategoryId);
                    var catName = expCategory.FirstOrDefault().Category;
                    var ExpenseObj = new Expense
                    {
                        Amount = expenseViewModel.Amount,
                        TotalAmount = expenseViewModel.TotalAmount,
                        ExpenseDate = expenseViewModel.ExpenseDate,
                        SubmitDate = expenseViewModel.SubmitDate,
                        Status = new TicketStatus { State = TicketState.Submitted, Reason = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now },
                        Approvers = new Approver { ApproverId = expenseViewModel.ApproverId, Name = approverName },// approver.FirstOrDefault(),
                        Employees = employee.FirstOrDefault(),
                        ExpenseDetails = expenseViewModel.ExpenseDetails,
                        ExpCategory = new ExpenseCategory { CategoryId = expenseViewModel.ExpCategory.CategoryId, Category = catName },
                        Reason = new Reason { EmployeeId = empId, Reasoning = "Expense submitted by -" + User.Identity.Name + " - " + DateTime.Now },
                        Docs = doc

                    };

                    UOW.Expenses.Add(ExpenseObj);
                    UOW.Commit();
                    return Ok(doc);
                }*/
        [HttpPut("")]
        public async Task<HttpResponseMessage> Put([FromBody]ExpenseViewModel expense)
        {
            var expenseFetched = UOW.Expenses.GetAll().Where(exp => exp.Id == expense.ExpenseId)
                .Include(expense1 => expense1.Approvers)
                .Include(expense2 => expense2.Employees)
                .Include(expense3 => expense3.ExpCategory)
                .Include(expense4 => expense4.Reason)
                .Include(expense5 => expense5.Status)
                .Include(expense6 => expense6.Docs);

            var expObj = expenseFetched.FirstOrDefault();

            //TODO: If status is anything other than rejected, he should not be able to edit
            if (expObj.Status.State == TicketState.Rejected)
            {
                expObj.Status.State = TicketState.Submitted;
                expObj.Status.Reason = expense.reason + "- Modified By - " + User.Identity.Name + " - " + DateTime.Now;
                expObj.ExpenseDate = expense.ExpenseDate;
                expObj.SubmitDate = expense.SubmitDate;
                expObj.ExpCategory.CategoryId = expense.ExpCategory.CategoryId;
                expObj.ExpCategory.Category = expense.ExpCategory.Category;
                expObj.Amount = expense.Amount;
                expObj.TotalAmount = expense.TotalAmount;
                expObj.ExpenseDetails = expense.ExpenseDetails;
                expObj.Reason.Reasoning = expense.reason + "- Modified By - " + User.Identity.Name + " - " + DateTime.Now;
                UOW.Expenses.Update(expObj);
                UOW.Commit();
                //Mail needs to be triggered here
                var emplName = expObj.Employees.EmployeeName;
                var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expense.ApproverId);
                var approverMail = approverEmp.FirstOrDefault().Email;
                await EmailHelper.SendEmailAsync(approverMail, "Ticket:- " + expense.ExpenseId + "Modified", "Expense Modified by " + emplName + ". Check Ticket for more details.");
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            if (expense.rejectedFlag == "Rejected")
            {
                expObj.Status.State = TicketState.Rejected;
                expObj.Status.Reason = expense.reason;
                expObj.Approvers.ApprovedDate = expense.ApprovedDate;
                expObj.Approvers.Remarks = expense.reason + "- Submitted by - " + User.Identity.Name + " - " + DateTime.Now;
                expObj.Reason = new Reason { EmployeeId = expense.EmployeeId, Reasoning = expense.reason };
                UOW.Expenses.Update(expObj);
                UOW.Commit();
                //Mail needs to be triggered here
                //TODO:- This Logic will change, below is there to fetch manager. 
                //TODO:- Here, employee needs to be notified means fetch employee from expense
                var manager = UOW.Employees.GetAll().Where(emp => emp.UserName.Equals(User.Identity.Name));
                var managerName = manager.FirstOrDefault().EmployeeName;
                var empMail = expenseFetched.FirstOrDefault().Employees.Email;
                await EmailHelper.SendEmailAsync(empMail, "Ticket:-" + expense.ExpenseId + " Rejected", "Check Reasoning in Ticket. Ticket Rejected by:- " + managerName);

            }
            else
            {
                //Update status flow
                UpdateStatus(expObj.Status, expense.reason);
                expObj.Approvers.ApprovedDate = expense.ApprovedDate;
                expObj.Approvers.Remarks = expense.reason + "- Submitted by - " + User.Identity.Name;
                expObj.Reason = new Reason { EmployeeId = expense.EmployeeId, Reasoning = expense.reason };
                UOW.Expenses.Update(expObj);
                UOW.Commit();
                //Mail needs to be triggered here. Check expobj status and then trigger accordingly 
                //If ticket is in submit state, it means it should go to manager
                if (expObj.Status.State == TicketState.Submitted)
                {
                    var employee = UOW.Employees.GetAll().Where(emp => emp.UserName.Equals(User.Identity.Name));
                    var emplName = employee.FirstOrDefault().EmployeeName;
                    var empMail = employee.FirstOrDefault().Email;
                    var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expense.ApproverId);
                    var approverMail = approverEmp.FirstOrDefault().Email;
                    await EmailHelper.SendEmailAsync("rahul.sahay@kdi.kongsberg.com", "Ticket Submitted", "New Expense Request Submitted by:- " + emplName);
                }
                //If ticket is approved by manager, then it will go to admin.
                if (expObj.Status.State == TicketState.ApprovedFromManager)
                {
                    var emplName = expenseFetched.FirstOrDefault().Employees.EmployeeName;
                    var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expense.ApproverId);
                    var approverName = approverEmp.FirstOrDefault().EmployeeName;
                    //   SendEmailAsync("salma.nigaar@kdi.kongsberg.com", "Ticket:- "+expense.ExpenseId+ " Approved by:- "+ approverName, "New Expense Request Submitted by:- " + emplName);
                    await EmailHelper.SendEmailAsync("rahul.sahay@kdi.kongsberg.com", "Ticket:- " + expense.ExpenseId + " Approved by:- " + approverName, "New Expense Request Submitted by:- " + emplName);
                }
                //If ticket is approved by admin, then it will go to finance.
                if (expObj.Status.State == TicketState.ApprovedFromAdmin)
                {
                    var emplName = expenseFetched.FirstOrDefault().Employees.EmployeeName;
                    var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expense.ApproverId);
                    var approverName = approverEmp.FirstOrDefault().EmployeeName;
                    await EmailHelper.SendEmailAsync("naveen.bathina@kdi.kongsberg.com", "Ticket:- " + expense.ExpenseId + " Approved by:- " + approverName, "New Expense Request Submitted by:- " + emplName);
                }
                //If ticket is approved by finance, then it will get closed
                if (expObj.Status.State == TicketState.ApprovedFromFinance)
                {
                    var emplName = expenseFetched.FirstOrDefault().Employees.EmployeeName;
                    var emplMail = expenseFetched.FirstOrDefault().Employees.Email;
                    var approverEmp = UOW.Employees.GetAll().Where(e => e.EmployeeId == expense.ApproverId);
                    var approverName = approverEmp.FirstOrDefault().EmployeeName;
                    await EmailHelper.SendEmailAsync("rahul.sahay@kdi.kongsberg.com", "Ticket:- " + expense.ExpenseId + " Closed " + approverName, "Ticket closed Submitted by:- " + emplName);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        private static void UpdateStatus(TicketStatus expObjStatus, string expenseReason)
        {
            switch (expObjStatus.State)
            {
                //TODO: Reject flow needs to be designed 
                /* case TicketState.Rejected:
                     expObjStatus.State = TicketState.Submitted;
                     expObjStatus.Reason = expenseReason;
                     break;*/
                case TicketState.Submitted:
                    expObjStatus.State = TicketState.ApprovedFromManager;
                    expObjStatus.Reason = expenseReason;
                    break;
                case TicketState.ApprovedFromManager:
                    expObjStatus.State = TicketState.ApprovedFromAdmin;
                    expObjStatus.Reason = expenseReason;
                    break;
                case TicketState.ApprovedFromAdmin:
                    expObjStatus.State = TicketState.ApprovedFromFinance;
                    expObjStatus.Reason = expenseReason;
                    break;
                //TODO 
                case TicketState.ApprovedFromFinance:
                    expObjStatus.State = TicketState.Closed;
                    expObjStatus.Reason = expenseReason;
                    break;
            }
        }


        // DELETE api/expense/5
        [HttpDelete("{Id}")]
        public HttpResponseMessage Delete(int Id)
        {
            UOW.Expenses.Delete(Id);
            UOW.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

    }
}

