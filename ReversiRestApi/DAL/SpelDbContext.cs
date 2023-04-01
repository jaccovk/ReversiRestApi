using Microsoft.EntityFrameworkCore;
using ReversieISpelImplementatie.Model;

namespace ReversiRestApi.DAL
{
    public class SpelDbContext : DbContext
    {
        public SpelDbContext(DbContextOptions<SpelDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Spel> Spel { get; set; }
    }
}
