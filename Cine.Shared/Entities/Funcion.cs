using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Cine.Shared.Entities
{
    public class Funcion
    {
        public int Id { get; set; }

        [Display(Name = "Funciones")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int PeliculaId { get; set; }

        public Pelicula? Pelicula { get; set; }

    }
}
