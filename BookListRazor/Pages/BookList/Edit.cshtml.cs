using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet(int id)
        {
            Book = _db.Book.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var bookFromDb = await _db.Book.FindAsync(Book.Id);
                bookFromDb.Name = Book.Name;
                bookFromDb.ISBN = Book.ISBN;
                bookFromDb.Author = Book.Author;

                await _db.SaveChangesAsync();
                Message = "Book Successfully edited";
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}