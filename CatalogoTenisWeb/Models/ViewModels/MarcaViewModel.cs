using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoTenisWeb.Models.ViewModels
{
    public class MarcaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe escribir el nombre de la marca.")]
        [StringLength(45, ErrorMessage = "La categoria no debe sobrepasar los 45 caracteres de longitud.")]
        public string Nombre { get; set; }
    }
}
