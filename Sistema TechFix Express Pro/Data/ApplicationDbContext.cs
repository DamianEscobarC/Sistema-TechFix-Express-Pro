using Microsoft.EntityFrameworkCore;
using Sistema_TechFix_Express_Pro.Models;

namespace Sistema_TechFix_Express_Pro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrdenServicio> OrdenesServicio { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}