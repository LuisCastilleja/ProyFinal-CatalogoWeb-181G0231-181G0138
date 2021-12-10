using CatalogoTenisWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CatalogoTenisWeb.Repositories
{
    public class TenisRepository : Repository<Tenis>
    {
        public TenisRepository(CatalogoTenisContext context) : base(context)
        {

        }
        public IEnumerable<TenisViewModel>
            GetProductosByMarca(string categoria)
        {
            return Context.Set<Tenis>()
                .Include(x => x.IdMarcaNavigation)
                .Where(x => x.IdMarcaNavigation.Nombre == categoria)
                .OrderBy(x => x.Nombre)
                .Select(x => new TenisViewModel
                {
                    Id = x.Id,
                    IdMarca = x.IdMarca,
                    Nombre = x.Nombre,
                    Color = x.Color,
                    Talla = x.Talla,
                    Precio = x.Precio,
                }); 
        }


        public Tenis GetProductoByNombre(string nombre)
        {
            nombre = nombre.Replace("-", " ");

            return Context.Set<Tenis>().Include(x => x.IdMarcaNavigation)
                .FirstOrDefault(x => x.Nombre.ToLower() == nombre);
        }

        public IEnumerable<Tenis> GetProductosByIdMarca(int? id)
        {

            return Context.Set<Tenis>().Include(x => x.IdMarcaNavigation)
                .Where(x => id == null || x.IdMarca == id).OrderBy(x => x.Nombre);
        }

        public void GuardarArchivo(int idProducto, IFormFile archivo, string ruta)
        {
            var path = Path.Combine(ruta, "img_tenis", idProducto + ".jpg");
            FileStream fs = File.Create(path);
            archivo.CopyTo(fs);
            fs.Close();
        }
        public int GetTotalProductosByMarca(Tenis c)
        {
            return Context.Set<Tenis>().Count(x => x.IdMarca == c.Id);
        }
    }
}
