using Cine.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Cine.Shared.Entities
{
    public class Ciudad
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad/Municipio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public ICollection<Ciudad>? Ciudades { get; set; }

        [Display(Name = "Ciudad/Municipio")]
        public int CiudadNumber => Ciudades == null ? 0 : Ciudades.Count;


        public int ColombiaId { get; set; }

        public Colombia? Colombia { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
