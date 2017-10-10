using ExpenseReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReimbursementApp.Model;

namespace ReimbursementApp.DbContext
{
    public class ExpenseReviewDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private bool _b;
        private bool _c;

        public ExpenseReviewDbContext()
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Approver> Approvers { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Bill> Bills  { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ExpenseCategorySet> ExpenseCategorySets { get; set; }
        public virtual DbSet<ApproverList> ApproverLists { get; set; }
        public virtual DbSet<Documents> Documentses { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //While deploying to azure, make sure to change the connection string based on azure settings
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ExpenseReviewSPA;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        public ExpenseReviewDbContext(DbContextOptions<ExpenseReviewDbContext> options) : base(options)
        {
            //It will look for connection string from appsettings

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(a => new {a.EmployeeId, a.Id});
            modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeeId).IsUnique();
            //This means Multiple document can be uploaded against single expense id
            modelBuilder.Entity<Documents>().HasIndex(documents => documents.ExpenseId).IsUnique(false);
            modelBuilder.Entity<Approver>().HasKey(a => new {a.ApproverId,a.Id});
            modelBuilder.Entity<ExpenseCategory>().HasKey(e => new {e.CategoryId, e.Id});
            modelBuilder.Entity<ExpenseCategorySet>().HasKey(e => new { e.CategoryId, e.Id });
            modelBuilder.Entity<Participant>().HasKey(a => new { a.EmployeeId, a.Id });
            base.OnModelCreating(modelBuilder);
        }
    }
}
