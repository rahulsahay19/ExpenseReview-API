using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseReview.Data.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace ReimbursementApp.Controllers.API
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ExpCategoryController : Controller
    {
        private IExpenseReviewUOW UOW;

        public ExpCategoryController(IExpenseReviewUOW uow)
        {
            UOW = uow;
        }

        [HttpGet("")]
        public IQueryable Get()
        {
            var model = UOW.ExpCategories.GetAll().OrderByDescending(exp => exp.CategoryId);
            return model;
        }
    }
}
