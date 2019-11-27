using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        private GeneralFunctions GFC = new GeneralFunctions();

        // GET: Empleado
        public ActionResult Index()
        {
            try
            {
                var tbEmpleado = db.tbEmpleado.Include(t => t.tbMunicipio).Include(t => t.tbPuesto).Include(t => t.tbTipoSangre);
                return View(tbEmpleado.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }


        // GET: Municipio
        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = (from x in db.tbMunicipio where x.dep_codigo == CodDepartamento select new { mun_codigo = x.mun_codigo, mun_nombre = x.mun_nombre}).ToList();
                /*db.tbMunicipio.Where(x=> x.dep_codigo==CodDepartamento).ToList();*/
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //INACTIVAR EMPLEADO
        [HttpPost]
        public JsonResult InactivarEmpleado(tbEmpleado tbEmpleado)
        {

            IEnumerable<Object>  list = null;
            try {



                tbEmpleado empleado = db.tbEmpleado.Find(tbEmpleado.emp_Id);
                 list = db.UDP_Gral_tbEmpleado_Update(tbEmpleado.emp_Id, empleado.emp_Nombres, empleado.emp_Apellidos, empleado.emp_Sexo, empleado.emp_FechaNacimiento, empleado.emp_Identificacion, empleado.emp_Telefono, empleado.emp_CorreoElectronico, tbEmpleado.emp_RazonInactivacion, GeneralFunctions.empleadoinactivo, empleado.tps_Id, empleado.pto_Id, empleado.emp_FechaIngreso, empleado.emp_Direccion, empleado.emp_PathImage, empleado.mun_Id, empleado.emp_UsuarioCrea).ToList();


            }
            catch 
                (Exception Ex)
            {


               
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: Empleado/Details/5
        public ActionResult Details(short? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
                if (tbEmpleado == null)
                {
                    return HttpNotFound();
                }
                return View(tbEmpleado);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Empleado/Activar/5
        public ActionResult Activate(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            tbEmpleado.emp_RazonInactivacion = "";
            IEnumerable<Object> List = null;
            string Msj = "";
            List = db.UDP_Gral_tbEmpleado_Update(tbEmpleado.emp_Id, tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo, tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico, tbEmpleado.emp_RazonInactivacion, GeneralFunctions.empleadoactivo, tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_Id, 1);
            foreach (UDP_Gral_tbEmpleado_Update_Result Empleado in List)
                Msj = Empleado.MensajeError;
            if (Msj.StartsWith("-1"))
            {
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            try
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                string fullName = userClaims?.FindFirst("name")?.Value;
                ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre");
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description");
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");

                return View();
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono,emp_CorreoElectronico,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_Estado,emp_PathImage,mun_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado ,  HttpPostedFileBase FotoPath)
        {

             tbEmpleado.emp_PathImage = "----";

            if (FotoPath == null)
            {
                TempData["smserror"] = "Imagen requerida.";
                ViewBag.smserror = TempData["smserror"];

                ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion",tbEmpleado.est_Id);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbEmpleado);




            }
            if (ModelState.IsValid)
                try
                {

                    var path = "";
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                    {
                        if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png")
                        {
                            string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                            string Archivo = tbEmpleado.emp_Id + Extension;
                            path = Path.Combine(Server.MapPath("~/Content/Profile_Pics"), Archivo);
                            FotoPath.SaveAs(path);
                            tbEmpleado.emp_PathImage = "~/Content/Profile_Pics/" + Archivo;
                        }
                        else
                        {
                            ModelState.AddModelError("FotoPath", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .jpg");
                            return View("Index");
                        }



                        }
                    }
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbEmpleado_Insert(tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo, tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico, tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_Id, 1);
                    foreach (UDP_Gral_tbEmpleado_Insert_Result Empleado in List)
                        Msj = Empleado.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                        ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                        ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                        ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                        ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        return View(tbEmpleado);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                    ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                    ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                    ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                    ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");

                    return View(tbEmpleado);
                }
            else
            {
                ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbEmpleado);
            }
     
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(short? id)
        {
            try
            {





                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
                if (tbEmpleado == null)
                {
                    return HttpNotFound();
                }
                ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", db.tbMunicipio.Find(tbEmpleado.mun_Id).dep_codigo);
                return View(tbEmpleado);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono,emp_CorreoElectronico,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,est_Id,emp_PathImage,mun_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado, HttpPostedFileBase FotoPath)
        {
            if (ModelState.IsValid)
                try
                {

                    var path = "";
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                        {
                            if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png")
                            {
                                string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                                string Archivo = tbEmpleado.emp_Id + Extension;
                                path = Path.Combine(Server.MapPath("~/Content/Profile_Pics"), Archivo);
                                FotoPath.SaveAs(path);
                                tbEmpleado.emp_PathImage = "~/Content/Profile_Pics/" + Archivo;
                            }
                            else
                            {
                                ModelState.AddModelError("FotoPath", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .jpg");
                                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);

                                return View("Index");
                            }



                        }
                    }



                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbEmpleado_Update(tbEmpleado.emp_Id, tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo,tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico, tbEmpleado.emp_RazonInactivacion, tbEmpleado.est_Id, tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_Id,1);
                    foreach (UDP_Gral_tbEmpleado_Update_Result Empleado in List)
                        Msj = Empleado.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                        ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                        ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                        ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                        ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);

                        return View(tbEmpleado);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                    ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                    ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                    ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                    ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);

                    return View(tbEmpleado);
                }
            else
            {
                ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_Id);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);

                return View(tbEmpleado);
            }
         
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            db.tbEmpleado.Remove(tbEmpleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
