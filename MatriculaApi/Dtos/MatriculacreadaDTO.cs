using System.ComponentModel.DataAnnotations;

namespace MatriculaApi.Dtos
{
    public class MatriculacreadaDTO
    {
        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int CursoId { get; set; }

        [Required]
        public DateTime FechaMatricula { get; set; }
    }
}
