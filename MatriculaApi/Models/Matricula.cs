using System.ComponentModel.DataAnnotations.Schema;

namespace MatriculaApi.Models
{
    
    public class Matricula
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Estado { get; set; }
        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
    }
}
