using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cine.Shared.Entities
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Display(Name = "Pelicula")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int GeneroId { get; set; }

        public Genero? Genero { get; set; }


        public ICollection<Funcion>? Funciones { get; set; }

        [Display(Name = "Funcion")]

        public int FuncionesNumber => Funciones == null ? 0 : Funciones.Count;
    
}
}
