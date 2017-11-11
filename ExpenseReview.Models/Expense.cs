using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ReimbursementApp.Model;

namespace ExpenseReview.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public string ExpenseDate { get; set; }
        [Required]
        public string SubmitDate { get; set; }
        [Required]
        [StringLength(500)]
        public string ExpenseDetails { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        /*public virtual int EmployeeId { get; set; }
        public virtual int ApproverId { get; set; }*/
        public virtual Employee Employees { get; set; }

        public virtual Approver Approvers { get; set; }

        public virtual TicketStatus Status { get; set; }

        public virtual ExpenseCategory ExpCategory { get; set; }
        public virtual ICollection<Documents> Docs { get; set; }
        public virtual Reason Reason { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
        public Expense()
        {
            Docs = new Collection<Documents>();
            Participants = new Collection<Participant>();
            //  Employees= new Employee();
            // Approvers= new Approver();
            //  Status = new TicketStatus();
            //Reason= new Reason();
        }

    }
}
