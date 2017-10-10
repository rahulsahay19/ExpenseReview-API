using System.Linq;
using ExpenseReview.Models;
using ReimbursementApp.DbContext;
using ReimbursementApp.Model;

namespace ReimbursementApp.SampleData
{
    public class InitialData
    {
        private ExpenseReviewDbContext _dbContext;

        public InitialData(ExpenseReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Making sure that database has nothing before seeding
        public void SeedData()
        {
            if (!_dbContext.Expenses.Any())
            {
                var approver1 = new Employee
                {
                    UserName = "KONGSBERG\\dhavalt",
                    EmployeeName = "Dhaval Trivedi",
                    Gender = "M",
                    Designation = "Manager",
                    SkillSet = "HTML 5, AngularJS, JavaScript",
                    EmployeeId = 1234,
                    Email = "dhaval.trivedi@kdi.kongsberg.com",
                    DOB = "05/05/1986",
                    Mobile = "2345678901",
                    AlternateNumber = "1234567890",
                    AddressLine1 = "Flat No 203, Suncity Apartment",
                    AddressLine2 = "Off Sarjapur Road",
                    AddressLine3 = "Near Wipro Office",
                    ZipCode = "560102",
                    Country = "India",
                    State = "Karnataka",
                    FatherName = "Rakesh Kumar",
                    MotherName = "Uma Devi",
                    FatherDOB = "18/11/1957",
                    MotherDOB = "06/05/1961",
                    EmergencyContactName = "Kumar Gautham",
                    EmergencyContactNumber = "3456789012",
                    EmergencyContactRelation = "Brother",
                    EmergencyContactDOB = "15/10/1985",
                    ReportingManager = "Mrinal"
                };
                _dbContext.Employees.Add(approver1);
                _dbContext.SaveChanges();

                var approver2 = new Employee
                {
                    UserName = "KONGSBERG\\mrinalp",
                    EmployeeName = "Mrinal Pandya",
                    Gender = "M",
                    Designation = "Manager",
                    SkillSet = "HTML 5, AngularJS, JavaScript",
                    EmployeeId = 3456,
                    Email = "mrinal.pandya@kdi.kongsberg.com",
                    DOB = "05/05/1986",
                    Mobile = "2345678901",
                    AlternateNumber = "1234567890",
                    AddressLine1 = "Flat No 203, Suncity Apartment",
                    AddressLine2 = "Off Sarjapur Road",
                    AddressLine3 = "Near Wipro Office",
                    ZipCode = "560102",
                    Country = "India",
                    State = "Karnataka",
                    FatherName = "Rakesh Kumar",
                    MotherName = "Uma Devi",
                    FatherDOB = "18/11/1957",
                    MotherDOB = "06/05/1961",
                    EmergencyContactName = "Kumar Gautham",
                    EmergencyContactNumber = "3456789012",
                    EmergencyContactRelation = "Brother",
                    EmergencyContactDOB = "15/10/1985",
                    ReportingManager = "Mrinal"
                };
                _dbContext.Employees.Add(approver2);
                _dbContext.SaveChanges();

                var approver3 = new Employee
                {
                    UserName = "KONGSBERG\\deepaks",
                    EmployeeName = "Deepak Kumar Swain",
                    Gender = "M",
                    Designation = "Manager",
                    SkillSet = "HTML 5, AngularJS, JavaScript",
                    EmployeeId = 2345,
                    Email = "deepak.kumar.swain@kdi.kongsberg.com",
                    DOB = "05/05/1986",
                    Mobile = "2345678901",
                    AlternateNumber = "1234567890",
                    AddressLine1 = "Flat No 203, Suncity Apartment",
                    AddressLine2 = "Off Sarjapur Road",
                    AddressLine3 = "Near Wipro Office",
                    ZipCode = "560102",
                    Country = "India",
                    State = "Karnataka",
                    FatherName = "Rakesh Kumar",
                    MotherName = "Uma Devi",
                    FatherDOB = "18/11/1957",
                    MotherDOB = "06/05/1961",
                    EmergencyContactName = "Kumar Gautham",
                    EmergencyContactNumber = "3456789012",
                    EmergencyContactRelation = "Brother",
                    EmergencyContactDOB = "15/10/1985",
                    ReportingManager = "Mrinal"
                };
                _dbContext.Employees.Add(approver3);
                _dbContext.SaveChanges();

                //Add New Set
                var expense = new Expense
                {
                    ExpenseDate = "12/09/2017",
                    SubmitDate = "13/09/2017",
                    Amount = 5200,
                    Employees = new Employee
                    {
                        UserName = "KONGSBERG\\saketk",
                        EmployeeName = "Saket Kumar",
                        Gender = "M",
                        Designation = "SDE 1",
                        SkillSet = "HTML 5, AngularJS, JavaScript, .NET",
                        EmployeeId = 93865,
                        Email = "saket.kumar@kdi.kongsberg.com",
                        DOB = "15/01/1985",
                        Mobile = "8147602853",
                        AlternateNumber = "7975645963",
                        AddressLine1 = "Flat No 205, Shobha Garnet",
                        AddressLine2 = "Off Sarjapur Road",
                        AddressLine3 = "Near Wipro Office",
                        ZipCode = "560102",
                        Country = "India",
                        State = "Karnataka",
                        FatherName = "Anil Kumar",
                        MotherName = "Anila Kumari",
                        FatherDOB = "15/03/1957",
                        MotherDOB = "05/04/1961",
                        EmergencyContactName = "Nirja Kumari",
                        EmergencyContactNumber = "1234567890",
                        EmergencyContactRelation = "Wife",
                        EmergencyContactDOB = "17/07/1985",
                        ReportingManager = "Dhaval"

                    },
                    ExpenseDetails = "Random",
                    TotalAmount = 5200,
                    ExpCategory = new ExpenseCategory
                    {
                        CategoryId = 1,
                        Category = "Visa"
                    },
                    Approvers = new Approver
                    {

                        ApproverId = 1234,
                        ApprovedDate = "13/09/2017",
                        Name = "Dhaval",
                        Remarks = "Approved"

                    },
                    Status = new TicketStatus { State = TicketState.ApprovedFromFinance, Reason = "Claim Approved" },
                    Reason = new Reason { Reasoning = "Approved From Finance", EmployeeId = 93865 }

                };
                _dbContext.Expenses.Add(expense);
                _dbContext.Employees.AddRange(expense.Employees);
                _dbContext.Approvers.AddRange(expense.Approvers);
                _dbContext.SaveChanges();

                var expense1 = new Expense
                {
                    ExpenseDate = "11/09/2017",
                    SubmitDate = "12/09/2017",
                    Amount = 5300,
                    Employees = new Employee
                    {
                        UserName = "User 2",
                        EmployeeName = "Shyam Sinha",
                        Gender = "M",
                        Designation = "SDE 2",
                        SkillSet = "C#, .NET",
                        EmployeeId = 93868,
                        Email = "shyam.sinha@kdi.kongsberg.com",
                        DOB = "19/05/1983",
                        Mobile = "8147612345",
                        AlternateNumber = "7975612345",
                        AddressLine1 = "Flat No 206, Suncity Apartment",
                        AddressLine2 = "Off Sarjapur Road",
                        AddressLine3 = "Near Wipro Office",
                        ZipCode = "560102",
                        Country = "India",
                        State = "Karnataka",
                        FatherName = "Ram Kumar",
                        MotherName = "Anita Kumari",
                        FatherDOB = "05/10/1961",
                        MotherDOB = "06/12/1961",
                        EmergencyContactName = "Kritika Kumari",
                        EmergencyContactNumber = "2345678901",
                        EmergencyContactRelation = "Sister",
                        EmergencyContactDOB = "15/07/1983",
                        ReportingManager = "Deepak"

                    },
                    ExpenseDetails = "Another Expense",
                    TotalAmount = 5300,
                    ExpCategory = new ExpenseCategory
                    {
                        CategoryId = 2,
                        Category = "Party"
                    },
                    Approvers = new Approver
                    {
                        ApproverId = 2345,
                        ApprovedDate = "13/09/2017",
                        Name = "Deepak",
                        Remarks = "nothing"

                    },
                    Status = new TicketStatus { State = TicketState.ApprovedFromAdmin, Reason = "Claim Pending for document submission." },
                    Reason = new Reason { Reasoning = "Pending From Finance", EmployeeId = 93868 }
                };
                _dbContext.Expenses.Add(expense1);
                _dbContext.Employees.AddRange(expense1.Employees);
                _dbContext.Approvers.AddRange(expense1.Approvers);
                _dbContext.SaveChanges();

                var expense2 = new Expense
                {
                    ExpenseDate = "10/09/2017",
                    SubmitDate = "11/09/2017",
                    Amount = 5400,
                    Employees = new Employee
                    {
                        UserName = "User 3",
                        EmployeeName = "Sheena Kumari",
                        Gender = "F",
                        Designation = "Developer",
                        SkillSet = "HTML 5, AngularJS, JavaScript",
                        EmployeeId = 93869,
                        Email = "sheena.kumar1@kdi.kongsberg.com",
                        DOB = "19/05/1986",
                        Mobile = "8147602843",
                        AlternateNumber = "7975645965",
                        AddressLine1 = "Flat No 205, Suncity Apartment",
                        AddressLine2 = "Off Sarjapur Road",
                        AddressLine3 = "Near Wipro Office",
                        ZipCode = "560102",
                        Country = "India",
                        State = "Karnataka",
                        FatherName = "Rakesh Kumar",
                        MotherName = "Uma Devi",
                        FatherDOB = "18/11/1957",
                        MotherDOB = "06/05/1961",
                        EmergencyContactName = "Kumar Gautham",
                        EmergencyContactNumber = "3456789012",
                        EmergencyContactRelation = "Brother",
                        EmergencyContactDOB = "15/10/1985",
                        ReportingManager = "Mrinal"

                    },
                    ExpenseDetails = "Misel",
                    TotalAmount = 5400,
                    ExpCategory = new ExpenseCategory
                    {
                        CategoryId = 3,
                        Category = "Cab"
                    },
                    Approvers = new Approver
                    {
                        ApproverId = 3456,
                        ApprovedDate = "13/09/2017",
                        Name = "Mrinal",
                        Remarks = "Reviewing"

                    },
                    Status = new TicketStatus { State = TicketState.Submitted, Reason = "Claim Submitted!" },
                    Reason = new Reason { Reasoning = "Claim Submitted", EmployeeId = 93869 }

                };
                _dbContext.Expenses.Add(expense2);
                _dbContext.Employees.AddRange(expense2.Employees);
                _dbContext.Approvers.AddRange(expense2.Approvers);
                _dbContext.SaveChanges();

             }
          

            //Seed Expense CategorySet
            if (!_dbContext.ExpenseCategorySets.Any())
            {
                //Add New Set of Expense Category
                var expenseCat = new ExpenseCategorySet
                {
                   CategoryId = 1,
                   Category = "Visa"

                };
                _dbContext.ExpenseCategorySets.Add(expenseCat);
                _dbContext.SaveChanges();

                var expenseCat2 = new ExpenseCategorySet
                {
                    CategoryId = 2,
                    Category = "Party"
                };
                _dbContext.ExpenseCategorySets.Add(expenseCat2);
                _dbContext.SaveChanges();
                var expenseCat3 = new ExpenseCategorySet
                {
                    CategoryId = 3,
                    Category = "Cab"

                };
                _dbContext.ExpenseCategorySets.Add(expenseCat3);
                _dbContext.SaveChanges();
                var expenseCat4 = new ExpenseCategorySet
                {
                    CategoryId = 4,
                    Category = "OnSite-Kit"

                };
                _dbContext.ExpenseCategorySets.Add(expenseCat4);
                _dbContext.SaveChanges();
            }

            //Seed Approver Lists
            if (!_dbContext.ApproverLists.Any())
            {
                //Add New Set of Expense Category
                var approverCat = new ApproverList
                {
                    ApproverId = 1234,
                    Name = "Dhaval",
                   
                };
                _dbContext.ApproverLists.Add(approverCat);
                _dbContext.SaveChanges();
                var approverCat2 = new ApproverList
                {
                    ApproverId = 2345,
                    Name = "Deepak",

                };
                _dbContext.ApproverLists.Add(approverCat2);
                _dbContext.SaveChanges();
                var approverCat3 = new ApproverList
                {
                    ApproverId = 3456,
                    Name = "Mrinal",

                };
                _dbContext.ApproverLists.Add(approverCat3);
                _dbContext.SaveChanges();
                var approverCat4 = new ApproverList
                {
                    ApproverId = 4567,
                    Name = "Vesta",

                };
                _dbContext.ApproverLists.Add(approverCat4);
                _dbContext.SaveChanges();
            }
        }
    }
}

