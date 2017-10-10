using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseReview.Models
{
   public class Participant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //TODO on Expense page, EMP_Id and Approver name will get auto populated
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        public int  ApproverId { get; set; }
        public int ExpenseId { get; set; }
    }
}
