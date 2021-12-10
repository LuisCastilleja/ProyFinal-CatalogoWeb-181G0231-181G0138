using System;
using System.Collections.Generic;

#nullable disable

namespace CatalogoTenisWeb.Models
{
    public partial class Tenis
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public float Talla { get; set; }
        public int IdMarca { get; set; }
        public decimal Precio { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; }
    }
}
