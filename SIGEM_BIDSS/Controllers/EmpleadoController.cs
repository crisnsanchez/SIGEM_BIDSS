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
    public class EmpleadoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        private GeneralFunctions GFC = new GeneralFunctions();

        // GET: Empleado
        public ActionResult Index()
        {
            var tbEmpleado = db.tbEmpleado.Include(t => t.tbMunicipio).Include(t => t.tbPuesto).Include(t => t.tbTipoSangre);
            return View(tbEmpleado.ToList());
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
                list = db.UDP_rrhh_tbEmpleado_Update(tbEmpleado.emp_Id, empleado.emp_Nombres, empleado.emp_Apellidos, empleado.emp_Sexo, empleado.emp_FechaNacimiento, empleado.emp_Identificacion, empleado.emp_Telefono, empleado.emp_CorreoElectronico, empleado.emp_EsJefe, tbEmpleado.emp_RazonInactivacion, GeneralFunctions.empleadoinactivo, empleado.tps_Id, empleado.pto_Id, empleado.emp_FechaIngreso, empleado.emp_Direccion, empleado.emp_PathImage, empleado.mun_codigo, 1);
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
            tbEmpleado.emp_RazonInactivacion = GeneralFunctions.stringDefault;
            IEnumerable<Object> List = null;
            string Msj = "";
            List = db.UDP_rrhh_tbEmpleado_Update(tbEmpleado.emp_Id, tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo, tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico,tbEmpleado.emp_EsJefe, tbEmpleado.emp_RazonInactivacion, GeneralFunctions.empleadoactivo, tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_codigo, 1);
            foreach (UDP_rrhh_tbEmpleado_Update_Result Empleado in List)
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


            //string lvMensajeError = "";

            //var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            //string fullName = userClaims?.FindFirst("name")?.Value;
            //string[] names = fullName.Split(' ');
            //ViewBag.firstName = names.First();
            //ViewBag.lastName = names.Last();

          
            //ViewBag.Email = userClaims?.FindFirst("preferred_username")?.Value;
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre");
            ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description");
    
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");

            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono," +
            "emp_CorreoElectronico, emp_EsJefe ,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_Estado,emp_PathImage,mun_codigo,emp_UsuarioCrea,emp_FechaCrea," +
            "emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado ,  HttpPostedFileBase FotoPath)
        {
            ViewBag.muni = "true";
            tbEmpleado.emp_PathImage = "----";
            ViewBag.selectedMun = tbEmpleado.mun_codigo;

            if (FotoPath == null)
            {
                TempData["smserror"] = "Imagen requerida.";
                ViewBag.smserror = TempData["smserror"];



                //string lvMensajeError = "";

                //var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                //string fullName = userClaims?.FindFirst("name")?.Value;
                //string[] names = fullName.Split(' ');
                //ViewBag.firstName = names.First();
                //ViewBag.lastName = names.Last();

                //var _emailAD = userClaims?.FindFirst("preferred_username")?.Value;





                //var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity; 
                //You get the user’s first and last name below: ViewBag.Name = userClaims?.FindFirst("name")?.Value;




                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
             
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion",tbEmpleado.est_Id);
                string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);

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
                                int _NamePicture = (from _tbemp in db.tbEmpleado select _tbemp.emp_Id).Count() + 1;
                                string Archivo = _NamePicture + Extension;
                                //string Archivo = tbEmpleado.emp_Id + Extension;
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
                    List = db.UDP_rrhh_tbEmpleado_Insert(tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo, tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico, tbEmpleado.emp_EsJefe,tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_codigo, 1);
                    foreach (UDP_rrhh_tbEmpleado_Insert_Result Empleado in List)
                        Msj = Empleado.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                        ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                        ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                        ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                    
                        ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                        string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);

                        return View(tbEmpleado);
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador." + Ex.Message.ToString());
                    ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                    ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                    ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                    ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                 
                    ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                    string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);

                  
                    return View(tbEmpleado);
                }
            else
            {
                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);

                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);

                return View(tbEmpleado);
            }
     
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(short? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            string _depto = db.tbMunicipio.Find(tbEmpleado.mun_codigo).dep_codigo;

            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio.Where(x => x.dep_codigo == _depto), "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
            ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre",db.tbMunicipio.Find(tbEmpleado.mun_codigo).dep_codigo);
            return View(tbEmpleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono,emp_CorreoElectronico,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,est_Id,emp_PathImage,mun_codigo,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica,emp_EsJefe")] tbEmpleado tbEmpleado, HttpPostedFileBase FotoPath)
         {
            ViewBag.muni = "true";
           
            ViewBag.selectedMun = tbEmpleado.mun_codigo;

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
                    List = db.UDP_rrhh_tbEmpleado_Update(tbEmpleado.emp_Id, tbEmpleado.emp_Nombres, tbEmpleado.emp_Apellidos, tbEmpleado.emp_Sexo,tbEmpleado.emp_FechaNacimiento, tbEmpleado.emp_Identificacion, tbEmpleado.emp_Telefono, tbEmpleado.emp_CorreoElectronico,tbEmpleado.emp_EsJefe, tbEmpleado.emp_RazonInactivacion, tbEmpleado.est_Id, tbEmpleado.tps_Id, tbEmpleado.pto_Id, tbEmpleado.emp_FechaIngreso, tbEmpleado.emp_Direccion, tbEmpleado.emp_PathImage, tbEmpleado.mun_codigo,1);
                    foreach (UDP_rrhh_tbEmpleado_Update_Result Empleado in List)
                        Msj = Empleado.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                        ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                        ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                        ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                        ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                        string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);

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
                    ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                    ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                    ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                    ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                    ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                    string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);


                    return View(tbEmpleado);
                }
            else
            {
                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbEmpleado.mun_codigo);
                ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
                ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbEmpleado.est_Id);
                ViewBag.emp_Sexo = new SelectList(GFC.Sexo(), "ge_Id", "ge_Description", tbEmpleado.emp_Sexo);
                string _dep_Codigo = db.tbMunicipio.Where(x => x.mun_codigo == tbEmpleado.mun_codigo).Select(s => s.dep_codigo).FirstOrDefault();
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", _dep_Codigo);


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
