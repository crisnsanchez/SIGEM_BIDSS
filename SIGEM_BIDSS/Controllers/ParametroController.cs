using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class ParametroController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions functions = new GeneralFunctions();
        // GET: tbParametroes
        public ActionResult Index()
        {
            var conteo = db.UDP_Conf_tbParametro_Count(1).ToList();
            var parametro = db.tbParametro.ToList();
            int? par = 0;
            byte? idparametro = 0;

            foreach (UDP_Conf_tbParametro_Count_Result contarparametro in conteo)
                par = contarparametro.Conteo;
            foreach (tbParametro id in parametro)
                idparametro = id.par_Id;
            if (par > 0)
            {
                return RedirectToAction("Details/" + idparametro, "Parametro");
            }
            else
            {
                return RedirectToAction("Create", "Parametro");
            }
        }
    

        // GET: tbParametroes/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // GET: tbParametroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbParametroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_CorreoEmisor,par_Password,par_Servidor,par_Puerto,par_PathLogo")] tbParametro tbParametro,
           HttpPostedFileBase FotoPath
           )
        {
           
            var path = "";
            if (FotoPath == null)
            {
                TempData["smserror"] = "Imagen requerida.";
                ViewBag.smserror = TempData["smserror"];

                return View(tbParametro);
            }
            if (ModelState.IsValid)
            {

                try
                {
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                        {
                            if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png")
                            {
                                string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                                string Archivo = tbParametro.par_Id + Extension;
                                path = Path.Combine(Server.MapPath("~/Content/img/"), Archivo);
                                FotoPath.SaveAs(path);
                                tbParametro.par_PathLogo = "~/Content/img/" + Archivo;
                            }
                            else
                            {
                                ModelState.AddModelError("FotoPath", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .jpg");

                                return View("Index");
                            }
                        }
                    }

                    IEnumerable<object> List = null;
                    var MsjError = "";
                   
                    List = db.UDP_Conf_tbParametro_Insert(tbParametro.par_NombreEmpresa, tbParametro.par_TelefonoEmpresa, tbParametro.par_CorreoEmpresa, tbParametro.par_CorreoEmisor, tbParametro.par_CorreoRRHH, tbParametro.par_Password, tbParametro.par_Servidor, tbParametro.par_Puerto, tbParametro.par_PathLogo
                        );
                    foreach (UDP_Conf_tbParametro_Insert_Result parametro in List)
                        MsjError = parametro.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {

                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbParametro);
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }


                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {

                            ModelState.AddModelError("", ve.ErrorMessage.ToString() + " " + ve.PropertyName.ToString());
                            return View("Index");
                        }
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return RedirectToAction("Index");
                }
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                }
            }

            return View(tbParametro);
        }
            // GET: tbParametroes/Edit/5
            public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // POST: tbParametroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id, [Bind(Include = "par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_CorreoEmisor, par_Password ,par_Servidor,par_Puerto,par_PathLogo")] tbParametro tbParametro,
       HttpPostedFileBase FotoPath)

        {
            var path = "";
            var UsFoto = db.tbParametro.Select(s => new { s.par_Id, s.par_PathLogo }).Where(x => x.par_Id == tbParametro.par_Id).ToList();
            if (UsFoto.Count() != 0 && UsFoto != null)
            {
                for (int i = 0; i < UsFoto.Count(); i++)
                    path = Convert.ToString(UsFoto[i].par_PathLogo);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    tbParametro.par_PathLogo = path;
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                        {
                            if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png")
                            {
                                string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                                string Archivo = tbParametro.par_Id + Extension;
                                path = Path.Combine(Server.MapPath("~/Content/img/"), Archivo);
                                FotoPath.SaveAs(path);
                                tbParametro.par_PathLogo = "~/Content/img/" + Archivo;
                            }
                            else
                            {
                                if (path != null)
                                    tbParametro.par_PathLogo = path;
                                ModelState.AddModelError("FotoPath", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .png ó .jpg");
                                return View(tbParametro);
                            }
                        }
                    }
                    tbParametro vtbparametro = db.tbParametro.Find(id);

                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Conf_tbParametro_Update(tbParametro.par_Id, tbParametro.par_NombreEmpresa, tbParametro.par_TelefonoEmpresa, tbParametro.par_CorreoEmpresa, tbParametro.par_CorreoEmisor, tbParametro.par_CorreoRRHH,tbParametro.par_Password, tbParametro.par_Servidor, tbParametro.par_Puerto, tbParametro.par_PathLogo);
                    foreach (UDP_Conf_tbParametro_Update_Result parametro in List)
                        MsjError = parametro.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {

                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit/" + MsjError);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Index");
                }
               
            }

            if (path != null)
                tbParametro.par_PathLogo = path;
            return View(tbParametro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: tbParametroes/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // POST: tbParametroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbParametro tbParametro = db.tbParametro.Find(id);
            db.tbParametro.Remove(tbParametro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
