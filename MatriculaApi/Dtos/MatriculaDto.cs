using System.ComponentModel.DataAnnotations;

namespace MatriculaApi.Dtos
{
    public class MatriculaDto
    {
        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int CursoId { get; set; }

        [Required]
        public DateTime FechaMatricula { get; set; }

        // Opcionales
        public string? Estado { get; set; }
        public string? NombreCurso { get; set; }
        public string? NombreEstudiante { get; set; }
    }

}
