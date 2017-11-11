using System.ComponentModel.DataAnnotations;

namespace ExpenseReview.Models
{
    public class Reason
    {
        [Key]
        public int ReasonId { get; set; }
        public string Reasoning { get; set; }
        public virtual int EmployeeId { get; set; }
    }
}
