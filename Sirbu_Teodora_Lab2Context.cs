using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sirbu_Teodora_Lab2.Data;
using Sirbu_Teodora_Lab2.Models;

public class Sirbu_Teodora_Lab2Context(DbContextOptions<Sirbu_Teodora_Lab2Context> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Sirbu_Teodora_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; } = default!;
    public DbSet<Authors> Authors { get; set; } = default!;
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<Member> Member { get; set; } = default!;
    public DbSet<Borrowing> Borrowing { get; set; } = default!;
}
