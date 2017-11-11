using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReimbursementApp.Model;

namespace ReimbursementApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
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
        public string ReportingManager { get; set; }
        public bool SignedUp { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherDOB { get; set; }
        public string MotherDOB { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactDOB { get; set; }
        public string RoleName { get; set; }
        public bool isEditable { get; set; }
        public bool approvalRequired { get; set; }
    }
}
