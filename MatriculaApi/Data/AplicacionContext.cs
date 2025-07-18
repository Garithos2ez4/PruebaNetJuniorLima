using Microsoft.EntityFrameworkCore;
using MatriculaApi.Models;

namespace MatriculaApi.Data

{
    public class AplicacionContext : DbContext
    {
        public AplicacionContext(DbContextOptions<AplicacionContext> options) : base(options)
        {
        }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(m => m.EstudianteId);
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoId);
        }
    }
}
