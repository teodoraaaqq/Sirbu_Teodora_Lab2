using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sirbu_Teodora_Lab2.Models;

namespace Sirbu_Teodora_Lab2.Pages.AuthorsPages;

public class IndexModel : PageModel
{
    private readonly Sirbu_Teodora_Lab2Context _context;

    public IndexModel(Sirbu_Teodora_Lab2Context context)
    {
        _context = context;
    }

    public IList<Authors> Authors { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.ToListAsync();
    }
}
