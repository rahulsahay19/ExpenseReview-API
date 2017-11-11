using System;

namespace ExpenseReview.Models
{
    [Flags()]
    public enum TicketState
    {

        //Approval --> This is for offline, 
        //Submitted --> Pending for approval, Next-->AdminCheck-->FinanceCheck Status (Processed,Pending with reason)
        //Need to create two flows for admin and Bills
        Submitted = 1,

        ApprovedFromManager = 2, //either from admin, finance, manager, reason also required, different pendings

        ApprovedFromAdmin = 3, //different approvals with reason

        ApprovedFromFinance = 4,

        Rejected = 5,

        Closed = 6

        //Expense categories. Let it open, so that admin can add new category.
        //Categories won't have enum types as this will be generic 
    }
}