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
    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }
    public string CurrentFilter { get; set; }



    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

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

        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s => s.Authors.FirstName.Contains(searchString) || s.Authors.LastName.Contains(searchString) || s.Title.Contains(searchString));
        }

            if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();

            BookD.Categories = book.BookCategories
                .Select(s => s.Category);
        }
        switch(sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Authors.FullName);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Authors.FullName);
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }
    }

}

