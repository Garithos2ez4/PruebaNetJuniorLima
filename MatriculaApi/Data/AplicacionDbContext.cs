using MatriculaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MatriculaApi.Data
{
    public class AplicacionDbContext: DbContext
    {
        public AplicacionDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Estudiante> estudiante { get; set; }
        public DbSet<Curso> curso { get; set; }
        public DbSet<Matricula> matricula { get; set; }
    }
}
