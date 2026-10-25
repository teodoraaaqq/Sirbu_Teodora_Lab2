using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sirbu_Teodora_Lab2.Models;

namespace Sirbu_Teodora_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Sirbu_Teodora_Lab2Context _context;

    public IndexModel(Sirbu_Teodora_Lab2Context context) 
    { 
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? AuthorId { get; set; }



    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();

        ViewData["AuthorsID"] = new SelectList(
            _context.Authors, "ID", "LastName");

        BookD.Books = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (AuthorId != null)
        {
            BookD.Books = BookD.Books
                .Where(b => b.AuthorsID == AuthorId.Value)
                .ToList();
        }

        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();

            BookD.Categories = book.BookCategories
                .Select(s => s.Category);
        }
    }

}

