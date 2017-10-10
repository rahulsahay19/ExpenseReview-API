using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseReview.Models
{
    /// <summary>
    /// Visa, CAB, Party, OnSite-Kit, etc..
    /// </summary>
   public class ExpenseCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
