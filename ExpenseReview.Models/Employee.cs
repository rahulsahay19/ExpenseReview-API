using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ReimbursementApp.Model
{
    //TODO:- Sign info will present below screen. This is one time activity.
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //TODO on Expense page, EMP_Id and Approver name will get auto populated
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        //This will get populated via windows login
        public string UserName { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string SkillSet { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherDOB { get; set; }
        public string MotherDOB { get; set; }

        public bool SignedUp { get; set; }

        //Emergency details, can have Kin details
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactDOB { get; set; }
        //TODO:- Need to think on this relationship
        //  public virtual Approver ReportingManager { get; set; }
        public string ReportingManager { get; set; }
        public string RoleName { get; set; }
        public bool isEditable { get; set; }
        public bool approvalRequired { get; set; }
    }
}
