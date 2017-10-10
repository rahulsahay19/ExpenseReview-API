namespace ExpenseReview.Models
{
   public class TicketStatus
    {
        public int Id { get; set; }
        public virtual TicketState State { get; set; }

        //Admin can enter reason and reply say pending with reason
        public string Reason { get; set; }
    }
}
