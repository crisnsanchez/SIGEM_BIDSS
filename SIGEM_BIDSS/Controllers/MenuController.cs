using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Catalogo()
        {
            return View();
        }
        public ActionResult Empleados()
        {
            return View();
        }
        public ActionResult Solicitud()
        {
            return View();
        }
    }
}