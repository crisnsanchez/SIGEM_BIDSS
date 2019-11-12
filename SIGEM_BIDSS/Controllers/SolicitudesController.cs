using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    public class SolicitudesController : Controller
    {
        // GET: Solicitudes
        public ActionResult SolicitudAnticipoViatico()
        {
            return View();
        }
    }
}