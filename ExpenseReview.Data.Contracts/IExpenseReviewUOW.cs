using ExpenseReview.Models;
using ReimbursementApp.Model;

namespace ExpenseReview.Data.Contracts
{
    public interface IExpenseReviewUOW
    {
        /// <summary>
        /// This is extendable pattern. If required, more things can be added in future
        /// </summary>
        void Commit();
        IRepository<Expense> Expenses { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Approver> Approvers { get; }
        IRepository<ExpenseCategory> ExpCategories { get; }
        IRepository<ExpenseCategorySet> ExpenseCategorySets { get; }
        IRepository<ApproverList> ApproverLists { get; }
        IRepository<Documents> DocumentLists { get; }
        IRepository<Role> Roles { get; }
    }
}
