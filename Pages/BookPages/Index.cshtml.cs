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

    public async Task OnGetAsync(int? authorId)
    {
        ViewData["AuthorsID"] = new SelectList(_context.Authors, "ID", "LastName");

        var books = _context.Book.Include(b => b.Publisher).Include(b => b.Authors);

        if (authorId != null)
        {
            Book = await books.Where(b => b.AuthorsID == authorId).ToListAsync();
        }
        else
        {
            Book = await books.ToListAsync();
        }
    }
}

