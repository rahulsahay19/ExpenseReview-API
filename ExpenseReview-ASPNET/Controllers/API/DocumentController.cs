
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using ExpenseReview.Data.Contracts;
using ExpenseReview.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReimbursementApp.Model;

namespace ReimbursementApp.Controllers.API
{
    [Authorize]
    [Route("/api/expense/{Id}/files")]
    [EnableCors("CorsPolicy")]
    public class DocumentController :Controller
    {
        private IHostingEnvironment _host;
        private IExpenseReviewUOW _uow;
        private DocumentSettings _options;

        public DocumentController(IHostingEnvironment host, IExpenseReviewUOW uow, IOptionsSnapshot<DocumentSettings> options)
        {
            _host = host;
            _uow = uow;
            _options = options.Value;
        }

        [HttpPost]
        public IActionResult Upload(int Id, IFormFile file)
        {
            var expense = _uow.Expenses.GetById(Id);
            if (expense == null)
            {
                return NotFound();
            }

            if (file == null) return BadRequest("File not valid");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > _options.MaxBytes) return BadRequest("File exceeded 10 MB size!");

            if (!_options.IsSupported(file.FileName)) return BadRequest("Invalid File Type");
            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var doc = new Documents {DocName = fileName};
            expense.Docs.Add(doc);
            _uow.Commit();
            return Ok(doc);
        }

        [HttpGet]
        public IQueryable<Documents>[] Get(int id)
        {
            IQueryable<Documents>[] docs = new[] { _uow.DocumentLists.GetAll().Where(m => m.ExpenseId == id) };
            if (docs != null) return docs;
            throw new Exception(new HttpResponseMessage(HttpStatusCode.NotFound).ToString());
        }
    }


   
}
