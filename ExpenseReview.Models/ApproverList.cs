using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseReview.Models
{
    public class ApproverList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApproverId { get; set; }
        public string Name { get; set; }

    }
}
