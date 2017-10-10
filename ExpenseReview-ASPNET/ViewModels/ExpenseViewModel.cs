using ExpenseReview.Models;

namespace ReimbursementApp.ViewModels
{
    public class ExpenseViewModel
    {
        public int EmployeeId { get; set; }
        public int ApproverId { get; set; }
        public string EmployeeName { get; set; }
        public string ApproverName { get; set; }
        public string ExpenseDate { get; set; }
        public string SubmitDate { get; set; }
        public string ApprovedDate { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        public string ExpenseDetails { get; set; }
        public string TicketStatus { get; set; }
        public ExpenseCategory ExpCategory { get; set; }
        public int ExpenseId { get; set; }
        public string reason { get; set; }
        public string rejectedFlag { get; set; }
        public string DocName { get; set; }
      //  public bool ParticipantFlag { get; set; }
       // public ICollection<Participant> Participants { get; set; }
    }
}
