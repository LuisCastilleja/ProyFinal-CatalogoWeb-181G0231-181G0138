using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoTenisWeb.Models.ViewModels
{
    public class TenisViewModel
    {
          
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre del producto.")]
        [StringLength(45, ErrorMessage = "El nombre del tenis no debe sobrepasar los 45 caracteres de longitud.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe especificar el color del tenis.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Debe especificar la talla del calzado.")]
        public float Talla { get; set; }
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "Debe escribir el precio del producto.")]

        public decimal Precio { get; set; }
    }
}
