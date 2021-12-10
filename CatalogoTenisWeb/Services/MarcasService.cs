using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models;
using CatalogoTenisWeb.Repositories;
namespace CatalogoTenisWeb.Services
{
    public class MarcasService
    {
		public CatalogoTenisContext Context { get; }
		public MarcasService(CatalogoTenisContext context)
		{
			Context = context;
		}
		public IEnumerable<string> GetNombreMarcas()
		{
			MarcasRepository repos = new MarcasRepository(Context);
			return repos.GetNombresMarcas();
		}

		public IEnumerable<Marca> GetCategorias()
		{
			MarcasRepository repos = new MarcasRepository(Context);
			return repos.GetAll().OrderBy(x => x.Nombre);
		}
	}
}
