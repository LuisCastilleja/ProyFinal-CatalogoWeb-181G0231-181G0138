using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoTenisWeb.Controllers
{
    public class AdministradorController : Controller
    {
        [Route("Administrador/")]
        [Route("Administrador/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
