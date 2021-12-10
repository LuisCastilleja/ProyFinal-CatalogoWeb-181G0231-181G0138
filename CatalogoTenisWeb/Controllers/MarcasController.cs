using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Repositories;
using CatalogoTenisWeb.Models;
using CatalogoTenisWeb.Models.ViewModels;
namespace CatalogoTenisWeb.Controllers
{
    public class MarcasController : Controller
    {
        public CatalogoTenisContext Context { get; }
        public MarcasController(CatalogoTenisContext context)
        {
            Context = context;
        }
        [Route("Marcas/")]
        [Route("Marcas/Index")]
        public IActionResult Index()
        {
            MarcasRepository repository = new MarcasRepository(Context);
            return View(repository.GetAll());
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(MarcaViewModel mv)
        {
            if (ModelState.IsValid)
            {
                MarcasRepository repos = new MarcasRepository(Context);
                var mar = repos.GetMarcaByNombre(mv.Nombre);
                if (mar == null)
                {
                    repos.Insert(mv);
                    return RedirectToAction("Index", "Marcas");
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe una marca con el nombre especificado.");
                    return View(mv);
                }
            }
            else
            {
                return View(mv);
            }
        }
        public IActionResult Editar(int id)
        {
            MarcasRepository repos = new MarcasRepository(Context);

            var mar = repos.GetMarcaById(id);

            if (mar == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(mar);
            }
        }
        [HttpPost]
        public IActionResult Editar(MarcaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                MarcasRepository repos = new MarcasRepository(Context);
                var mar = repos.GetMarcaByNombre(vm.Nombre);
                if (mar == null)
                {
                    repos.Update(vm);
                    return RedirectToAction("Index", "Marcas");
                }
                else if (mar.Id == vm.Id)
                {
                    mar.Nombre = vm.Nombre;
                    repos.Update(mar);
                    return RedirectToAction("Index", "Marcas");
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe una marca con el nombre especificado.");
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }
        public IActionResult Eliminar(int id)
        {
            MarcasRepository repos = new MarcasRepository(Context);
            var mar = repos.GetById(id);

            if (mar == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(mar);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(Marca c)
        {
            MarcasRepository repos = new MarcasRepository(Context);
            var mar = repos.GetById(c.Id);

            if (mar == null)
            {
                ModelState.AddModelError("", "La marca no existe o ya ha sido eliminada.");
                return View(c);
            }
            else
            {
                if (repos.GetTotalProductosByMarca(c) == 0)
                {
                    repos.Delete(mar);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "La marca tiene productos asociados. Elimine o mueva los productos antes de eliminar la marca.");
                    return View(c);
                }
            }
        }
    }
}

