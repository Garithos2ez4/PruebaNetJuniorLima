using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatriculaApi.Models
{
   
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
     
        public Collection<Matricula> Matriculas { get; set; }
    }
}
