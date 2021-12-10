using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models;
using CatalogoTenisWeb.Models.ViewModels;
namespace CatalogoTenisWeb.Repositories
{
    public class MarcasRepository : Repository<Marca>
    {
        public MarcasRepository(CatalogoTenisContext context):base(context)
        {

        }
        public IEnumerable<string> GetNombresMarcas()
        {
            var data = GetAll().OrderBy(x => x.Nombre).Select(x => x.Nombre);
            return data;
        }
        public Marca GetMarcaByNombre(string nombre)
        {
            return Context.Set<Marca>()
                .FirstOrDefault(x => x.Nombre.ToUpper() == nombre.ToUpper());
        }
        public MarcaViewModel GetMarcaById(int id)
        {
            return Context.Set<Marca>().Where(x => x.Id == id)
                .Select(x => new MarcaViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).FirstOrDefault();
        }
        public void Insert(MarcaViewModel mvm)
        {
            Marca m = new Marca { Nombre = mvm.Nombre };
            Insert(m);
        }
        public void Update(MarcaViewModel mvm)
        {
            Marca m = new Marca { Id = mvm.Id, Nombre = mvm.Nombre };
            Update(m);
        }
        public int GetTotalProductosByMarca(Marca m)
        {
            return Context.Set<Tenis>().Count(x => x.IdMarca == m.Id);
        }
    }
}
