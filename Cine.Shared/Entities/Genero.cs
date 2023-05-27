using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cine.Shared.Entities
{
    public class Genero
    {
        public int Id { get; set; }

        [Display(Name = "Genero")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public ICollection<Pelicula>? Pelicula { get; set; }

        [Display(Name = "Pelicula")]

        public int PeliculaNumber => Pelicula == null ? 0 : Pelicula.Count;

        
    }
}