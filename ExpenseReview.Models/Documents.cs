using System.ComponentModel.DataAnnotations;

namespace ExpenseReview.Models
{
   public class Documents
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string DocName { get; set; }
        public int ExpenseId { get; set; }

    }
}
