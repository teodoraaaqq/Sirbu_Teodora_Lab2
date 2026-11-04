using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sirbu_Teodora_Lab2.Models;

namespace Sirbu_Teodora_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Sirbu_Teodora_Lab2Context _context;

    public CreateModel(Sirbu_Teodora_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var bookList = _context.Book
            .Include(b => b.Authors)
            .Select(x => new
        {
            x.ID,
            BookFullName = x.Title + " - " + x.Authors.LastName + " " +x.Authors.FirstName
        });

        ViewData["MemberID"] = new SelectList( _context.Member.ToList(), "ID", "FullName");

        ViewData["BookID"] = new SelectList( _context.Book.ToList(), "ID", "Title");

        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
