using System;
using ExpenseReview.Data.Contracts;
using ExpenseReview.Models;
using ReimbursementApp.DatabaseHelpers;
using ReimbursementApp.DbContext;
using ReimbursementApp.Model;

namespace ReimbursementApp.EFRepository
{
    public class ExpenseReviewUOW : IExpenseReviewUOW, IDisposable
    {
        public ExpenseReviewUOW(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        public IRepository<Expense> Expenses { get { return GetStandardRepo<Expense>(); } }
        public IRepository<Employee> Employees { get { return GetStandardRepo<Employee>(); } }
        public IRepository<Approver> Approvers { get { return GetStandardRepo<Approver>(); } }
        public IRepository<ExpenseCategory> ExpCategories { get { return GetStandardRepo<ExpenseCategory>(); } }
        public IRepository<ExpenseCategorySet> ExpenseCategorySets { get { return GetStandardRepo<ExpenseCategorySet>(); } }
        public IRepository<ApproverList> ApproverLists { get { return GetStandardRepo<ApproverList>(); } }
        public IRepository<Documents> DocumentLists { get { return GetStandardRepo<Documents>(); } }
        public IRepository<Role> Roles { get { return GetStandardRepo<Role>(); } }

        public IRepository<Admin> Admins { get { return GetStandardRepo<Admin>(); } }

        public IRepository<Bill> Bills { get { return GetStandardRepo<Bill>(); } }


        public void Commit()
        {
            DbContext.SaveChanges();
        }



        protected void CreateDbContext()
        {
            DbContext = new ExpenseReviewDbContext();
        }


        protected IRepositoryProvider RepositoryProvider { get; set; }


        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }
        private ExpenseReviewDbContext DbContext { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}
