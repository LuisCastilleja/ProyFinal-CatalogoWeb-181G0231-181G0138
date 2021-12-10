using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoTenisWeb.Models
{
    public partial class Tenis
    {
        [NotMapped]
        public IFormFile Archivo { get; set; }
    }
}
