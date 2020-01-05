using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class LiquidacionAnticipoViaticoDetalleController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();


        // GET: LiquidacionAnticipoViaticoDetalle
        public ActionResult Index()
        {
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            var tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Include(t => t.tbLiquidacionAnticipoViatico).Include(t => t.tbTipoViatico);
            return View(tbLiquidacionAnticipoViaticoDetalle.ToList());
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Create
        public ActionResult Create( int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = new tbLiquidacionAnticipoViaticoDetalle();
            tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id = tbLiquidacionAnticipoViatico.Lianvi_Id;
      
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View();

        }


       
     

        // POST: LiquidacionAnticipoViaticoDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult SaveLiquidacionAnticipoDetalle(tbLiquidacionAnticipoViaticoDetalle LIQUIDACIONDETALLE )
        {
            List<tbLiquidacionAnticipoViaticoDetalle> sessionLiquidaciondetalle = new List<tbLiquidacionAnticipoViaticoDetalle>();
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["tbLiquidacionAnticipoViaticoDetalle"];
            if (list == null)
            {
                ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                sessionLiquidaciondetalle.Add(LIQUIDACIONDETALLE);
                Session["tbLiquidacionAnticipoViaticoDetalle"] = sessionLiquidaciondetalle;
            }
            else
            {
                ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                list.Add(LIQUIDACIONDETALLE);
                Session["tbLiquidacionAnticipoViaticoDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_Archivo,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle)
        {


            {
                string UserName = "";
              
                try
                {
                    if (ModelState.IsValid)
                    {
                        int EmployeeID = Function.GetUser(out UserName);
                       

                        IEnumerable<object> _List = null;
                        string ErrorMessage = "";
                        _List = db.UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert(tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id,
                                                                              tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id,
                                                                              tbLiquidacionAnticipoViaticoDetalle.Lianvide_FechaGasto,
                                                                              tbLiquidacionAnticipoViaticoDetalle.tpv_Id,
                                                                              tbLiquidacionAnticipoViaticoDetalle.Lianvide_MontoGasto,
                                                                              tbLiquidacionAnticipoViaticoDetalle.Lianvide_Concepto,
                                                                              tbLiquidacionAnticipoViaticoDetalle.Lianvide_Archivo,
                                                                         
                                                                              EmployeeID, Function.DatetimeNow());
                        foreach (UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert_Result Area in _List)
                            ErrorMessage = Area.MensajeError;
                        if (ErrorMessage.StartsWith("-1"))
                        {
                            Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, ErrorMessage);
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbLiquidacionAnticipoViaticoDetalle);
                        }

                        else
                        {
                                                        TempData["swalfunction"] = "true";
                            //return RedirectToAction("Create", "tbLiquidacionAnticipoViatico.Lianvi_Id", "LiquidacionAnticipoViaticoDetalle");

                            //return RedirectToAction("Create", new RouteValueDictionary(new { controller = LiquidacionAnticipoViaticoDetalle, action = "Main", Id = Id }));
                            return View(tbLiquidacionAnticipoViaticoDetalle);



                        }

                    }
                }

                catch (Exception Ex)
                {
                    Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, Ex.Message.ToString());

                    return RedirectToAction("Create", "tbLiquidacionAnticipoViatico.Lianvi_Id", "LiquidacionAnticipoViaticoDetalle");
                }
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // GET: LiquidacionAnticipoViaticoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_Archivo,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLiquidacionAnticipoViaticoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // GET: LiquidacionAnticipoViaticoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            db.tbLiquidacionAnticipoViaticoDetalle.Remove(tbLiquidacionAnticipoViaticoDetalle);
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
        [HttpPost]
        public JsonResult RemoveDetalle(tbLiquidacionAnticipoViaticoDetalle LiquidacionAnticipoViaticoDetalle)
        {
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["tbLiquidacionAnticipoViaticoDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.Lianvide_Id == LiquidacionAnticipoViaticoDetalle.Lianvide_Id);
                list.Remove(itemToRemove);
                Session["tbLiquidacionAnticipoViaticoDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
    }


}
