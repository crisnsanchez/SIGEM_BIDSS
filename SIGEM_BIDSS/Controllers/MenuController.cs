using SIGEM_BIDSS.Models;
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
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Catalogo()
        {
            TempData["getswalfunction"] = TempData["swalfunction"];
            return View();
        }
        public ActionResult Empleados()
        {
            return View();
        }
        public ActionResult Solicitud()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            var tiposol = (from _dbt in db.tbTipoSolicitud select(_dbt.tipsol_Descripcion) );
            return View(tiposol);
        }
    }
}