using System;
using System.Collections.Generic;

#nullable disable

namespace CatalogoTenisWeb.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Tenis = new HashSet<Tenis>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Tenis> Tenis { get; set; }
    }
}
