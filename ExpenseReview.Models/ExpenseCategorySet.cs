using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseReview.Models
{
   public class ExpenseCategorySet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
