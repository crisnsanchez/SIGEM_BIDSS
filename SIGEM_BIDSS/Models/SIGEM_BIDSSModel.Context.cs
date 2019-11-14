﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGEM_BIDSS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SIGEM_BIDSSEntities : DbContext
    {
        public SIGEM_BIDSSEntities()
            : base("name=SIGEM_BIDSSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbArea> tbArea { get; set; }
        public virtual DbSet<tbDepartamento> tbDepartamento { get; set; }
        public virtual DbSet<tbEmpleado> tbEmpleado { get; set; }
        public virtual DbSet<tbEstado> tbEstado { get; set; }
        public virtual DbSet<tbMunicipio> tbMunicipio { get; set; }
        public virtual DbSet<tbPuesto> tbPuesto { get; set; }
        public virtual DbSet<tbSolicitud> tbSolicitud { get; set; }
        public virtual DbSet<tbTipoMoneda> tbTipoMoneda { get; set; }
        public virtual DbSet<tbTipoSangre> tbTipoSangre { get; set; }
        public virtual DbSet<tbTipoSolicitud> tbTipoSolicitud { get; set; }
        public virtual DbSet<tbTipoViatico> tbTipoViatico { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<spSelect_Result> spSelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spSelect_Result>("spSelect");
        }
    
        public virtual ObjectResult<UDP_Gral_tbArea_Insert_Result> UDP_Gral_tbArea_Insert(string are_Descripcion, Nullable<int> are_UsuarioCrea)
        {
            var are_DescripcionParameter = are_Descripcion != null ?
                new ObjectParameter("are_Descripcion", are_Descripcion) :
                new ObjectParameter("are_Descripcion", typeof(string));
    
            var are_UsuarioCreaParameter = are_UsuarioCrea.HasValue ?
                new ObjectParameter("are_UsuarioCrea", are_UsuarioCrea) :
                new ObjectParameter("are_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbArea_Insert_Result>("UDP_Gral_tbArea_Insert", are_DescripcionParameter, are_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbArea_Update_Result> UDP_Gral_tbArea_Update(Nullable<int> are_Id, string are_Descripcion, Nullable<int> are_UsuarioModifica)
        {
            var are_IdParameter = are_Id.HasValue ?
                new ObjectParameter("are_Id", are_Id) :
                new ObjectParameter("are_Id", typeof(int));
    
            var are_DescripcionParameter = are_Descripcion != null ?
                new ObjectParameter("are_Descripcion", are_Descripcion) :
                new ObjectParameter("are_Descripcion", typeof(string));
    
            var are_UsuarioModificaParameter = are_UsuarioModifica.HasValue ?
                new ObjectParameter("are_UsuarioModifica", are_UsuarioModifica) :
                new ObjectParameter("are_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbArea_Update_Result>("UDP_Gral_tbArea_Update", are_IdParameter, are_DescripcionParameter, are_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleado_Insert_Result> UDP_Gral_tbEmpleado_Insert(string emp_Nombres, string emp_Apellidos, string emp_Sexo, string emp_FechaNacimiento, string emp_Identificacion, string emp_Telefono, string emp_CorreoElectronico, Nullable<int> tps_Id, Nullable<int> pto_Id, string emp_FechaIngreso, string emp_Direccion, string emp_PathImage, string mun_Id, Nullable<int> emp_UsuarioCrea)
        {
            var emp_NombresParameter = emp_Nombres != null ?
                new ObjectParameter("emp_Nombres", emp_Nombres) :
                new ObjectParameter("emp_Nombres", typeof(string));
    
            var emp_ApellidosParameter = emp_Apellidos != null ?
                new ObjectParameter("emp_Apellidos", emp_Apellidos) :
                new ObjectParameter("emp_Apellidos", typeof(string));
    
            var emp_SexoParameter = emp_Sexo != null ?
                new ObjectParameter("emp_Sexo", emp_Sexo) :
                new ObjectParameter("emp_Sexo", typeof(string));
    
            var emp_FechaNacimientoParameter = emp_FechaNacimiento != null ?
                new ObjectParameter("emp_FechaNacimiento", emp_FechaNacimiento) :
                new ObjectParameter("emp_FechaNacimiento", typeof(string));
    
            var emp_IdentificacionParameter = emp_Identificacion != null ?
                new ObjectParameter("emp_Identificacion", emp_Identificacion) :
                new ObjectParameter("emp_Identificacion", typeof(string));
    
            var emp_TelefonoParameter = emp_Telefono != null ?
                new ObjectParameter("emp_Telefono", emp_Telefono) :
                new ObjectParameter("emp_Telefono", typeof(string));
    
            var emp_CorreoElectronicoParameter = emp_CorreoElectronico != null ?
                new ObjectParameter("emp_CorreoElectronico", emp_CorreoElectronico) :
                new ObjectParameter("emp_CorreoElectronico", typeof(string));
    
            var tps_IdParameter = tps_Id.HasValue ?
                new ObjectParameter("tps_Id", tps_Id) :
                new ObjectParameter("tps_Id", typeof(int));
    
            var pto_IdParameter = pto_Id.HasValue ?
                new ObjectParameter("pto_Id", pto_Id) :
                new ObjectParameter("pto_Id", typeof(int));
    
            var emp_FechaIngresoParameter = emp_FechaIngreso != null ?
                new ObjectParameter("emp_FechaIngreso", emp_FechaIngreso) :
                new ObjectParameter("emp_FechaIngreso", typeof(string));
    
            var emp_DireccionParameter = emp_Direccion != null ?
                new ObjectParameter("emp_Direccion", emp_Direccion) :
                new ObjectParameter("emp_Direccion", typeof(string));
    
            var emp_PathImageParameter = emp_PathImage != null ?
                new ObjectParameter("emp_PathImage", emp_PathImage) :
                new ObjectParameter("emp_PathImage", typeof(string));
    
            var mun_IdParameter = mun_Id != null ?
                new ObjectParameter("mun_Id", mun_Id) :
                new ObjectParameter("mun_Id", typeof(string));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleado_Insert_Result>("UDP_Gral_tbEmpleado_Insert", emp_NombresParameter, emp_ApellidosParameter, emp_SexoParameter, emp_FechaNacimientoParameter, emp_IdentificacionParameter, emp_TelefonoParameter, emp_CorreoElectronicoParameter, tps_IdParameter, pto_IdParameter, emp_FechaIngresoParameter, emp_DireccionParameter, emp_PathImageParameter, mun_IdParameter, emp_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEstado_Insert_Result> UDP_Gral_tbEstado_Insert(string est_Descripcion, Nullable<int> est_UsuarioCrea)
        {
            var est_DescripcionParameter = est_Descripcion != null ?
                new ObjectParameter("est_Descripcion", est_Descripcion) :
                new ObjectParameter("est_Descripcion", typeof(string));
    
            var est_UsuarioCreaParameter = est_UsuarioCrea.HasValue ?
                new ObjectParameter("est_UsuarioCrea", est_UsuarioCrea) :
                new ObjectParameter("est_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEstado_Insert_Result>("UDP_Gral_tbEstado_Insert", est_DescripcionParameter, est_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEstado_Update_Result> UDP_Gral_tbEstado_Update(Nullable<int> est_Id, string est_Descripcion, Nullable<int> est_UsuarioModifica)
        {
            var est_IdParameter = est_Id.HasValue ?
                new ObjectParameter("est_Id", est_Id) :
                new ObjectParameter("est_Id", typeof(int));
    
            var est_DescripcionParameter = est_Descripcion != null ?
                new ObjectParameter("est_Descripcion", est_Descripcion) :
                new ObjectParameter("est_Descripcion", typeof(string));
    
            var est_UsuarioModificaParameter = est_UsuarioModifica.HasValue ?
                new ObjectParameter("est_UsuarioModifica", est_UsuarioModifica) :
                new ObjectParameter("est_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEstado_Update_Result>("UDP_Gral_tbEstado_Update", est_IdParameter, est_DescripcionParameter, est_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbPuesto_Insert_Result> UDP_Gral_tbPuesto_Insert(Nullable<int> are_Id, string pto_Descripcion, Nullable<int> pto_UsuarioCrea)
        {
            var are_IdParameter = are_Id.HasValue ?
                new ObjectParameter("are_Id", are_Id) :
                new ObjectParameter("are_Id", typeof(int));
    
            var pto_DescripcionParameter = pto_Descripcion != null ?
                new ObjectParameter("pto_Descripcion", pto_Descripcion) :
                new ObjectParameter("pto_Descripcion", typeof(string));
    
            var pto_UsuarioCreaParameter = pto_UsuarioCrea.HasValue ?
                new ObjectParameter("pto_UsuarioCrea", pto_UsuarioCrea) :
                new ObjectParameter("pto_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbPuesto_Insert_Result>("UDP_Gral_tbPuesto_Insert", are_IdParameter, pto_DescripcionParameter, pto_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbPuesto_Update_Result> UDP_Gral_tbPuesto_Update(Nullable<int> pto_Id, Nullable<int> are_Id, string pto_Descripcion, Nullable<int> pto_UsuarioModifica)
        {
            var pto_IdParameter = pto_Id.HasValue ?
                new ObjectParameter("pto_Id", pto_Id) :
                new ObjectParameter("pto_Id", typeof(int));
    
            var are_IdParameter = are_Id.HasValue ?
                new ObjectParameter("are_Id", are_Id) :
                new ObjectParameter("are_Id", typeof(int));
    
            var pto_DescripcionParameter = pto_Descripcion != null ?
                new ObjectParameter("pto_Descripcion", pto_Descripcion) :
                new ObjectParameter("pto_Descripcion", typeof(string));
    
            var pto_UsuarioModificaParameter = pto_UsuarioModifica.HasValue ?
                new ObjectParameter("pto_UsuarioModifica", pto_UsuarioModifica) :
                new ObjectParameter("pto_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbPuesto_Update_Result>("UDP_Gral_tbPuesto_Update", pto_IdParameter, are_IdParameter, pto_DescripcionParameter, pto_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoMoneda_Insert_Result> UDP_Gral_tbTipoMoneda_Insert(string tmo_Abreviatura, string tmo_Nombre, Nullable<int> tmo_UsuarioCrea)
        {
            var tmo_AbreviaturaParameter = tmo_Abreviatura != null ?
                new ObjectParameter("tmo_Abreviatura", tmo_Abreviatura) :
                new ObjectParameter("tmo_Abreviatura", typeof(string));
    
            var tmo_NombreParameter = tmo_Nombre != null ?
                new ObjectParameter("tmo_Nombre", tmo_Nombre) :
                new ObjectParameter("tmo_Nombre", typeof(string));
    
            var tmo_UsuarioCreaParameter = tmo_UsuarioCrea.HasValue ?
                new ObjectParameter("tmo_UsuarioCrea", tmo_UsuarioCrea) :
                new ObjectParameter("tmo_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoMoneda_Insert_Result>("UDP_Gral_tbTipoMoneda_Insert", tmo_AbreviaturaParameter, tmo_NombreParameter, tmo_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoMoneda_Update_Result> UDP_Gral_tbTipoMoneda_Update(Nullable<short> tmo_Id, string tmo_Abreviatura, string tmo_Nombre, Nullable<int> tmo_UsuarioModifica)
        {
            var tmo_IdParameter = tmo_Id.HasValue ?
                new ObjectParameter("tmo_Id", tmo_Id) :
                new ObjectParameter("tmo_Id", typeof(short));
    
            var tmo_AbreviaturaParameter = tmo_Abreviatura != null ?
                new ObjectParameter("tmo_Abreviatura", tmo_Abreviatura) :
                new ObjectParameter("tmo_Abreviatura", typeof(string));
    
            var tmo_NombreParameter = tmo_Nombre != null ?
                new ObjectParameter("tmo_Nombre", tmo_Nombre) :
                new ObjectParameter("tmo_Nombre", typeof(string));
    
            var tmo_UsuarioModificaParameter = tmo_UsuarioModifica.HasValue ?
                new ObjectParameter("tmo_UsuarioModifica", tmo_UsuarioModifica) :
                new ObjectParameter("tmo_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoMoneda_Update_Result>("UDP_Gral_tbTipoMoneda_Update", tmo_IdParameter, tmo_AbreviaturaParameter, tmo_NombreParameter, tmo_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoSangre_Insert_Result> UDP_Gral_tbTipoSangre_Insert(string tps_Nombre, Nullable<int> tps_UsuarioCrea)
        {
            var tps_NombreParameter = tps_Nombre != null ?
                new ObjectParameter("tps_Nombre", tps_Nombre) :
                new ObjectParameter("tps_Nombre", typeof(string));
    
            var tps_UsuarioCreaParameter = tps_UsuarioCrea.HasValue ?
                new ObjectParameter("tps_UsuarioCrea", tps_UsuarioCrea) :
                new ObjectParameter("tps_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoSangre_Insert_Result>("UDP_Gral_tbTipoSangre_Insert", tps_NombreParameter, tps_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoSangre_Update_Result> UDP_Gral_tbTipoSangre_Update(Nullable<int> tps_Id, string tps_Nombre, Nullable<int> tps_UsuarioModifica)
        {
            var tps_IdParameter = tps_Id.HasValue ?
                new ObjectParameter("tps_Id", tps_Id) :
                new ObjectParameter("tps_Id", typeof(int));
    
            var tps_NombreParameter = tps_Nombre != null ?
                new ObjectParameter("tps_Nombre", tps_Nombre) :
                new ObjectParameter("tps_Nombre", typeof(string));
    
            var tps_UsuarioModificaParameter = tps_UsuarioModifica.HasValue ?
                new ObjectParameter("tps_UsuarioModifica", tps_UsuarioModifica) :
                new ObjectParameter("tps_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoSangre_Update_Result>("UDP_Gral_tbTipoSangre_Update", tps_IdParameter, tps_NombreParameter, tps_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoSolicitud_Insert_Result> UDP_Gral_tbTipoSolicitud_Insert(string tipsol_Descripcion, Nullable<int> tipsol_UsuarioCrea)
        {
            var tipsol_DescripcionParameter = tipsol_Descripcion != null ?
                new ObjectParameter("tipsol_Descripcion", tipsol_Descripcion) :
                new ObjectParameter("tipsol_Descripcion", typeof(string));
    
            var tipsol_UsuarioCreaParameter = tipsol_UsuarioCrea.HasValue ?
                new ObjectParameter("tipsol_UsuarioCrea", tipsol_UsuarioCrea) :
                new ObjectParameter("tipsol_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoSolicitud_Insert_Result>("UDP_Gral_tbTipoSolicitud_Insert", tipsol_DescripcionParameter, tipsol_UsuarioCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoSolicitud_Update_Result> UDP_Gral_tbTipoSolicitud_Update(Nullable<int> tipsol_Id, string tipsol_Descripcion, Nullable<int> tipsol_UsuarioModifica)
        {
            var tipsol_IdParameter = tipsol_Id.HasValue ?
                new ObjectParameter("tipsol_Id", tipsol_Id) :
                new ObjectParameter("tipsol_Id", typeof(int));
    
            var tipsol_DescripcionParameter = tipsol_Descripcion != null ?
                new ObjectParameter("tipsol_Descripcion", tipsol_Descripcion) :
                new ObjectParameter("tipsol_Descripcion", typeof(string));
    
            var tipsol_UsuarioModificaParameter = tipsol_UsuarioModifica.HasValue ?
                new ObjectParameter("tipsol_UsuarioModifica", tipsol_UsuarioModifica) :
                new ObjectParameter("tipsol_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoSolicitud_Update_Result>("UDP_Gral_tbTipoSolicitud_Update", tipsol_IdParameter, tipsol_DescripcionParameter, tipsol_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<SDP_tbMunicipio_Select_Result> SDP_tbMunicipio_Select(string mun_Codigo)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbMunicipio_Select_Result>("SDP_tbMunicipio_Select", mun_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDepartamento_Insert_Result> UDP_Gral_tbDepartamento_Insert(string dep_Codigo, string dep_Nombre, Nullable<int> dep_UsuarioCrea, Nullable<System.DateTime> dep_FechaCrea)
        {
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var dep_NombreParameter = dep_Nombre != null ?
                new ObjectParameter("dep_Nombre", dep_Nombre) :
                new ObjectParameter("dep_Nombre", typeof(string));
    
            var dep_UsuarioCreaParameter = dep_UsuarioCrea.HasValue ?
                new ObjectParameter("dep_UsuarioCrea", dep_UsuarioCrea) :
                new ObjectParameter("dep_UsuarioCrea", typeof(int));
    
            var dep_FechaCreaParameter = dep_FechaCrea.HasValue ?
                new ObjectParameter("dep_FechaCrea", dep_FechaCrea) :
                new ObjectParameter("dep_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDepartamento_Insert_Result>("UDP_Gral_tbDepartamento_Insert", dep_CodigoParameter, dep_NombreParameter, dep_UsuarioCreaParameter, dep_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDepartamento_Update_Result> UDP_Gral_tbDepartamento_Update(string dep_Codigo, string dep_Nombre, Nullable<int> dep_UsuarioModifica)
        {
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var dep_NombreParameter = dep_Nombre != null ?
                new ObjectParameter("dep_Nombre", dep_Nombre) :
                new ObjectParameter("dep_Nombre", typeof(string));
    
            var dep_UsuarioModificaParameter = dep_UsuarioModifica.HasValue ?
                new ObjectParameter("dep_UsuarioModifica", dep_UsuarioModifica) :
                new ObjectParameter("dep_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDepartamento_Update_Result>("UDP_Gral_tbDepartamento_Update", dep_CodigoParameter, dep_NombreParameter, dep_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Delete_Result> UDP_Gral_tbMunicipio_Delete(string mun_Codigo)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Delete_Result>("UDP_Gral_tbMunicipio_Delete", mun_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Insert_Result> UDP_Gral_tbMunicipio_Insert(string mun_Codigo, string dep_Codigo, string mun_Nombre, Nullable<int> mun_UsuarioCrea, Nullable<System.DateTime> mun_FechaCrea)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var mun_NombreParameter = mun_Nombre != null ?
                new ObjectParameter("mun_Nombre", mun_Nombre) :
                new ObjectParameter("mun_Nombre", typeof(string));
    
            var mun_UsuarioCreaParameter = mun_UsuarioCrea.HasValue ?
                new ObjectParameter("mun_UsuarioCrea", mun_UsuarioCrea) :
                new ObjectParameter("mun_UsuarioCrea", typeof(int));
    
            var mun_FechaCreaParameter = mun_FechaCrea.HasValue ?
                new ObjectParameter("mun_FechaCrea", mun_FechaCrea) :
                new ObjectParameter("mun_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Insert_Result>("UDP_Gral_tbMunicipio_Insert", mun_CodigoParameter, dep_CodigoParameter, mun_NombreParameter, mun_UsuarioCreaParameter, mun_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Update_Result> UDP_Gral_tbMunicipio_Update(string mun_Codigo, string dep_Codigo, string mun_Nombre, Nullable<int> mun_UsuarioModifica)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var mun_NombreParameter = mun_Nombre != null ?
                new ObjectParameter("mun_Nombre", mun_Nombre) :
                new ObjectParameter("mun_Nombre", typeof(string));
    
            var mun_UsuarioModificaParameter = mun_UsuarioModifica.HasValue ?
                new ObjectParameter("mun_UsuarioModifica", mun_UsuarioModifica) :
                new ObjectParameter("mun_UsuarioModifica", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Update_Result>("UDP_Gral_tbMunicipio_Update", mun_CodigoParameter, dep_CodigoParameter, mun_NombreParameter, mun_UsuarioModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleado_Update_Result> UDP_Gral_tbEmpleado_Update(Nullable<int> emp_Id, string emp_Nombres, string emp_Apellidos, string emp_Sexo, string emp_FechaNacimiento, string emp_Identificacion, string emp_Telefono, string emp_CorreoElectronico, string emp_RazonInactivacion, Nullable<int> est_Id, Nullable<int> tps_Id, Nullable<int> pto_Id, string emp_FechaIngreso, string emp_Direccion, string emp_PathImage, string mun_Id, Nullable<int> emp_UsuarioCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var emp_NombresParameter = emp_Nombres != null ?
                new ObjectParameter("emp_Nombres", emp_Nombres) :
                new ObjectParameter("emp_Nombres", typeof(string));
    
            var emp_ApellidosParameter = emp_Apellidos != null ?
                new ObjectParameter("emp_Apellidos", emp_Apellidos) :
                new ObjectParameter("emp_Apellidos", typeof(string));
    
            var emp_SexoParameter = emp_Sexo != null ?
                new ObjectParameter("emp_Sexo", emp_Sexo) :
                new ObjectParameter("emp_Sexo", typeof(string));
    
            var emp_FechaNacimientoParameter = emp_FechaNacimiento != null ?
                new ObjectParameter("emp_FechaNacimiento", emp_FechaNacimiento) :
                new ObjectParameter("emp_FechaNacimiento", typeof(string));
    
            var emp_IdentificacionParameter = emp_Identificacion != null ?
                new ObjectParameter("emp_Identificacion", emp_Identificacion) :
                new ObjectParameter("emp_Identificacion", typeof(string));
    
            var emp_TelefonoParameter = emp_Telefono != null ?
                new ObjectParameter("emp_Telefono", emp_Telefono) :
                new ObjectParameter("emp_Telefono", typeof(string));
    
            var emp_CorreoElectronicoParameter = emp_CorreoElectronico != null ?
                new ObjectParameter("emp_CorreoElectronico", emp_CorreoElectronico) :
                new ObjectParameter("emp_CorreoElectronico", typeof(string));
    
            var emp_RazonInactivacionParameter = emp_RazonInactivacion != null ?
                new ObjectParameter("emp_RazonInactivacion", emp_RazonInactivacion) :
                new ObjectParameter("emp_RazonInactivacion", typeof(string));
    
            var est_IdParameter = est_Id.HasValue ?
                new ObjectParameter("est_Id", est_Id) :
                new ObjectParameter("est_Id", typeof(int));
    
            var tps_IdParameter = tps_Id.HasValue ?
                new ObjectParameter("tps_Id", tps_Id) :
                new ObjectParameter("tps_Id", typeof(int));
    
            var pto_IdParameter = pto_Id.HasValue ?
                new ObjectParameter("pto_Id", pto_Id) :
                new ObjectParameter("pto_Id", typeof(int));
    
            var emp_FechaIngresoParameter = emp_FechaIngreso != null ?
                new ObjectParameter("emp_FechaIngreso", emp_FechaIngreso) :
                new ObjectParameter("emp_FechaIngreso", typeof(string));
    
            var emp_DireccionParameter = emp_Direccion != null ?
                new ObjectParameter("emp_Direccion", emp_Direccion) :
                new ObjectParameter("emp_Direccion", typeof(string));
    
            var emp_PathImageParameter = emp_PathImage != null ?
                new ObjectParameter("emp_PathImage", emp_PathImage) :
                new ObjectParameter("emp_PathImage", typeof(string));
    
            var mun_IdParameter = mun_Id != null ?
                new ObjectParameter("mun_Id", mun_Id) :
                new ObjectParameter("mun_Id", typeof(string));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleado_Update_Result>("UDP_Gral_tbEmpleado_Update", emp_IdParameter, emp_NombresParameter, emp_ApellidosParameter, emp_SexoParameter, emp_FechaNacimientoParameter, emp_IdentificacionParameter, emp_TelefonoParameter, emp_CorreoElectronicoParameter, emp_RazonInactivacionParameter, est_IdParameter, tps_IdParameter, pto_IdParameter, emp_FechaIngresoParameter, emp_DireccionParameter, emp_PathImageParameter, mun_IdParameter, emp_UsuarioCreaParameter);
        }
    }
}
