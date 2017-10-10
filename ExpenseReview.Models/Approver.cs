using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseReview.Models
{
   public class Approver
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApproverId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string ApprovedDate { get; set; }
        //TODO:Designation may get added, but not required so far.
        public int ExpenseId { get; set; }
    }
}
