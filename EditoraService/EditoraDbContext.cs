using EditoraDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EditoraService
{
    public class EditoraDbContext : DbContext
    {
        public EditoraDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Livro> livros { get; set; }
        public DbSet<Autor> autores { get; set; }
    }
}