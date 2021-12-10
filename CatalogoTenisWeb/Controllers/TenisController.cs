using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models.ViewModels;
using CatalogoTenisWeb.Models;
using CatalogoTenisWeb.Repositories;
namespace CatalogoTenisWeb.Controllers
{
    public class TenisController : Controller
    {
        public CatalogoTenisContext Context { get; }
        public TenisController(CatalogoTenisContext context, IHostingEnvironment env)
        {
            Context = context;
			Environment = env;
        }
        public IHostingEnvironment Environment { get; set; }
       
        [Route("Tenis/")]
        [Route("Tenis/Index")]
        public IActionResult Index(int? id)
        {
            ViewBag.IdCategoria = id;
            TenisRepository repository = new(Context);
            return View(repository.GetProductosByIdMarca(id));
        }
		public IActionResult Agregar()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Agregar(Tenis p)
		{
			TenisRepository repos = new (Context);		
			if (ModelState.IsValid)
			{
				
				repos.Insert(p);
				if (p.Archivo != null)
				{
					if (p.Archivo.ContentType != "image/jpeg")
					{
						ModelState.AddModelError("", "Solo puede cargar imagenes en formato JPG.");
						return View(p);
					}

					if (p.Archivo.Length > 1024 * 1024)
					{
						ModelState.AddModelError("", "El tamaño máximo del archivo debe ser 1MB.");
						return View(p);
					}

					repos.GuardarArchivo(p.Id, p.Archivo, Environment.WebRootPath);
				}
				return RedirectToAction("Index");

			}
			else
			{
				return View(p);
			}
		}




		public IActionResult Editar(int id)
		{
			TenisRepository productos = new(Context);
			var p = productos.GetById(id);

			if (p == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(p);
			}
		}

		[HttpPost]
		public IActionResult Editar(Tenis p)
		{

			if (ModelState.IsValid)
			{
				TenisRepository productos = new(Context);
				productos.Update(p);

				if (p.Archivo != null)
				{
					if (p.Archivo.ContentType != "image/jpeg")
					{
						ModelState.AddModelError("", "Solo puede cargar imagenes en formato JPG.");
						return View(p);
					}

					if (p.Archivo.Length > 1024 * 1024)
					{
						ModelState.AddModelError("", "El tamaño máximo del archivo debe ser 1MB.");
						return View(p);
					}

					productos.GuardarArchivo(p.Id, p.Archivo, Environment.WebRootPath);
				}
				return RedirectToAction("Index");
			}
			else
			{
				return View(p);
			}
		}
		public IActionResult Eliminar(int id)
		{
			TenisRepository repos = new (Context);
			var cat = repos.GetById(id);

			if (cat == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(cat);
			}
		}
		[HttpPost]
		public IActionResult Eliminar(Tenis c)
		{
			TenisRepository repos = new (Context);
			var cat = repos.GetById(c.Id);

			if (cat == null)
			{
				ModelState.AddModelError("", "El producto no existe o ya ha sido eliminada.");
				return View(c);
			}
			else
			{
				if (repos.GetTotalProductosByMarca(c) == 0)
				{
					repos.Delete(cat);
					return RedirectToAction("Index");
					//Toast
				}
				else
				{
					ModelState.AddModelError("", "La categoria tiene productos asociados. Elimine o mueva los productos antes de eliminar la categoria.");
					return View(c);
				}
			}
		}
	}
}
