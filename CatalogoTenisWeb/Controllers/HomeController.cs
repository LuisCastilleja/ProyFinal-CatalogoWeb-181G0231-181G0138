using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models;
using CatalogoTenisWeb.Repositories;
namespace CatalogoTenisWeb.Controllers
{
    public class HomeController : Controller
    {
        public CatalogoTenisContext Context { get; }
        public HomeController(CatalogoTenisContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("{id}")]
        public IActionResult Marca(string id)
        {
            ViewBag.Categoria = id;
            TenisRepository repository = new(Context);
            return View(repository.GetProductosByMarca(id));
        }

        [Route("producto/{id}")]
        public IActionResult Ver(string id)
        {
            TenisRepository repos = new (Context);
            var p = repos.GetProductoByNombre(id);

            if (p == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }
    }
}
