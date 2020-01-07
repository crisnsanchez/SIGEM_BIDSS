using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    [SessionManager]
    public class RequisionCompraController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: RequisionCompra
        public async Task<ActionResult> Index()
        {
            var tbRequisionCompra = db.tbRequisionCompra.Include(t => t.tbArea).Include(t => t.tbEmpleado).Include(t => t.tbEstado);
            return View(await tbRequisionCompra.ToListAsync());
        }

        // GET: RequisionCompra/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Create
        public ActionResult Create()
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            return View();
        }

        // POST: RequisionCompra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "are_Id,Reqco_GralFechaSolicitud,Reqco_Comentario,Reqco_RazonRechazo")] tbRequisionCompra tbRequisionCompra)
        {
            //string UserName = "", ErrorEmail = "", ErrorMessage = "";
            //bool Result = false, ResultAdm = false;
            //IEnumerable<object> Insert = null;

            //try
            //{
            //    cGetUserInfo GetEmployee = null;
            //    int EmployeeID = Function.GetUser(out UserName);

            //    IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
            //                                    where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
            //                                    select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();


            //    ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            //    ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            //    ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);

            //    tbRequisionCompra.emp_Id = EmployeeID;
            //    tbRequisionCompra.Reqco_GralFechaSolicitud = Function.DatetimeNow();
            //    tbRequisionCompra.est_Id = GeneralFunctions.Enviada;

            //    var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();


            //    if (String.IsNullOrEmpty(tbRequisionCompra.Reqco_Comentario)) { tbRequisionCompra.Reqco_Comentario = GeneralFunctions.stringDefault; }

            //    if (ModelState.IsValid)
            //    {

            //        Insert = db.UDP_Adm_tbAnticipoSalario_Insert(EmployeeID,
            //                                                    tbAnticipoSalario.Ansal_JefeInmediato,
            //                                                    Function.DatetimeNow(),
            //                                                    tbAnticipoSalario.Ansal_MontoSolicitado,
            //                                                    tbAnticipoSalario.tpsal_id,
            //                                                    tbAnticipoSalario.Ansal_Justificacion,
            //                                                    tbAnticipoSalario.Ansal_Comentario,
            //                                                    tbAnticipoSalario.est_Id,
            //                                                    tbAnticipoSalario.Ansal_RazonRechazo,
            //                                                    EmployeeID,
            //                                                    Function.DatetimeNow());
            //        foreach (UDP_Adm_tbAnticipoSalario_Insert_Result Res in Insert)
            //            ErrorMessage = Res.MensajeError;
            //        if (ErrorMessage.StartsWith("-1"))
            //        {
            //            Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
            //            ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
            //        }
            //        else
            //        {
            //            GetEmployee = Function.GetUserInfo(EmployeeID);

            //            Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
            //            ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, _Parameters.par_CorreoRRHH);

            //            if (!Result) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
            //            if (!ResultAdm) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);

            //            TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
            //            return RedirectToAction("Index");
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
            //}
            //return View(tbAnticipoSalario);

            if (ModelState.IsValid)
            {
                db.tbRequisionCompra.Add(tbRequisionCompra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);
            return View(tbRequisionCompra);
        }

        // POST: RequisionCompra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Reqco_Id,Reqco_Correlativo,emp_Id,are_Id,Reqco_GralFechaSolicitud,Reqco_Comentario,est_Id,Reqco_RazonRechazo,Reqco_UsuarioCrea,Reqco_FechaCrea,Reqco_UsuarioModifica,Reqco_FechaModifica")] tbRequisionCompra tbRequisionCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRequisionCompra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);
            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisionCompra);
        }

        // POST: RequisionCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            db.tbRequisionCompra.Remove(tbRequisionCompra);
            await db.SaveChangesAsync();
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
