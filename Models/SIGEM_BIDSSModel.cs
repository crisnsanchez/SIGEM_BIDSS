using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SIGEM_BIDSS.Models
{
    public partial class SIGEM_BIDSSModel : DbContext
    {
        public SIGEM_BIDSSModel()
        {
        }

        public SIGEM_BIDSSModel(DbContextOptions<SIGEM_BIDSSModel> options)
            : base(options)
        {
        }

        public virtual DbSet<TbArea> TbArea { get; set; }
        public virtual DbSet<TbDepartamento> TbDepartamento { get; set; }
        public virtual DbSet<TbEmpleado> TbEmpleado { get; set; }
        public virtual DbSet<Estado> TbEstado { get; set; }
        public virtual DbSet<TbMunicipio> TbMunicipio { get; set; }
        public virtual DbSet<Puesto> TbPuesto { get; set; }
        public virtual DbSet<TipoMoneda> TbTipoMoneda { get; set; }
        public virtual DbSet<TbTipoSangre> TbTipoSangre { get; set; }
        public virtual DbSet<TipoSolicitud> TbTipoSolicitud { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ahm.canbbevbcgld.us-west-2.rds.amazonaws.com;Initial Catalog=SIGEM_BIDSS;Persist Security Info=True;User ID=bidss;Password=$$BIdss2019$$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TbArea>(entity =>
            {
                entity.HasKey(e => e.AreId)
                    .HasName("PK__tbArea__025610C5D7C00B74");

                entity.ToTable("tbArea", "Gral");

                entity.Property(e => e.AreId)
                    .HasColumnName("are_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AreDescripcion)
                    .IsRequired()
                    .HasColumnName("are_Descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.AreFechaCrea)
                    .HasColumnName("are_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.AreFechaModifica)
                    .HasColumnName("are_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.AreUsuarioCrea).HasColumnName("are_UsuarioCrea");

                entity.Property(e => e.AreUsuarioModifica).HasColumnName("are_UsuarioModifica");
            });

            modelBuilder.Entity<TbDepartamento>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK__tbDepart__BB4CBBE0A95C743F");

                entity.ToTable("tbDepartamento", "Gral");

                entity.Property(e => e.DepId)
                    .HasColumnName("dep_Id")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepDescripcion)
                    .IsRequired()
                    .HasColumnName("dep_Descripcion")
                    .HasMaxLength(100);

                entity.Property(e => e.DepFechaCrea)
                    .HasColumnName("dep_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepFechaModifica)
                    .HasColumnName("dep_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepUsuarioCrea).HasColumnName("dep_UsuarioCrea");

                entity.Property(e => e.DepUsuarioModifica).HasColumnName("dep_UsuarioModifica");
            });

            modelBuilder.Entity<TbEmpleado>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_Gral_tbEmpleados_emp_Id");

                entity.ToTable("tbEmpleado", "Gral");

                entity.Property(e => e.EmpId).HasColumnName("emp_Id");

                entity.Property(e => e.EmpApellidos)
                    .IsRequired()
                    .HasColumnName("emp_Apellidos")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpCorreoElectronico)
                    .HasColumnName("emp_CorreoElectronico")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpDireccion)
                    .IsRequired()
                    .HasColumnName("emp_Direccion")
                    .HasMaxLength(250);

                entity.Property(e => e.EmpEstado)
                    .IsRequired()
                    .HasColumnName("emp_Estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFechaCrea)
                    .HasColumnName("emp_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpFechaIngreso)
                    .IsRequired()
                    .HasColumnName("emp_FechaIngreso")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpFechaModifica)
                    .HasColumnName("emp_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpFechaNacimiento)
                    .IsRequired()
                    .HasColumnName("emp_FechaNacimiento")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpIdentificacion)
                    .IsRequired()
                    .HasColumnName("emp_Identificacion");

                entity.Property(e => e.EmpNombres)
                    .IsRequired()
                    .HasColumnName("emp_Nombres")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpPathImage)
                    .HasColumnName("emp_PathImage")
                    .IsUnicode(false);

                entity.Property(e => e.EmpRazonInactivacion)
                    .HasColumnName("emp_RazonInactivacion")
                    .HasMaxLength(500);

                entity.Property(e => e.EmpSexo)
                    .IsRequired()
                    .HasColumnName("emp_Sexo")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmpTelefono)
                    .IsRequired()
                    .HasColumnName("emp_Telefono")
                    .HasMaxLength(25);

                entity.Property(e => e.EmpUsuarioCrea).HasColumnName("emp_UsuarioCrea");

                entity.Property(e => e.EmpUsuarioModifica).HasColumnName("emp_UsuarioModifica");

                entity.Property(e => e.MunId)
                    .IsRequired()
                    .HasColumnName("mun_Id")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PtoId).HasColumnName("pto_Id");

                entity.Property(e => e.TpsId).HasColumnName("tps_Id");

                entity.HasOne(d => d.Mun)
                    .WithMany(p => p.TbEmpleado)
                    .HasForeignKey(d => d.MunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id");

                entity.HasOne(d => d.Pto)
                    .WithMany(p => p.TbEmpleado)
                    .HasForeignKey(d => d.PtoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pto_Id");

                entity.HasOne(d => d.Tps)
                    .WithMany(p => p.TbEmpleado)
                    .HasForeignKey(d => d.TpsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tps_Id");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.EstId)
                    .HasName("PK__tbEstado__40ADECB0E46D2B4B");

                entity.ToTable("tbEstado", "Gral");

                entity.Property(e => e.EstId)
                    .HasColumnName("est_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstDescripcion)
                    .IsRequired()
                    .HasColumnName("est_Descripcion")
                    .HasMaxLength(100);

                entity.Property(e => e.EstFechaCrea)
                    .HasColumnName("est_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.EstFechaModifica)
                    .HasColumnName("est_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.EstUsuarioCrea).HasColumnName("est_UsuarioCrea");

                entity.Property(e => e.EstUsuarioModifica).HasColumnName("est_UsuarioModifica");
            });

            modelBuilder.Entity<TbMunicipio>(entity =>
            {
                entity.HasKey(e => e.MunId)
                    .HasName("PK__tbMunici__130DA6EF1E42CAE1");

                entity.ToTable("tbMunicipio", "Gral");

                entity.Property(e => e.MunId)
                    .HasColumnName("mun_Id")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepId)
                    .IsRequired()
                    .HasColumnName("dep_Id")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.MunFechaCrea)
                    .HasColumnName("mun_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.MunFechaModifica)
                    .HasColumnName("mun_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.MunNombre)
                    .IsRequired()
                    .HasColumnName("mun_nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.MunUsuarioCrea).HasColumnName("mun_UsuarioCrea");

                entity.Property(e => e.MunUsuarioModifica).HasColumnName("mun_UsuarioModifica");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.TbMunicipio)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dep_Id");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.PtoId)
                    .HasName("PK__tbPuesto__6856AD9DF71DB5B8");

                entity.ToTable("tbPuesto", "Gral");

                entity.Property(e => e.PtoId)
                    .HasColumnName("pto_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AreId).HasColumnName("are_Id");

                entity.Property(e => e.PtoDescripcion)
                    .IsRequired()
                    .HasColumnName("pto_Descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.PtoFechaCrea)
                    .HasColumnName("pto_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.PtoFechaModifica)
                    .HasColumnName("pto_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.PtoUsuarioCrea).HasColumnName("pto_UsuarioCrea");

                entity.Property(e => e.PtoUsuarioModifica).HasColumnName("pto_UsuarioModifica");

                entity.HasOne(d => d.Are)
                    .WithMany(p => p.TbPuesto)
                    .HasForeignKey(d => d.AreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_are_Id");
            });

            modelBuilder.Entity<TipoMoneda>(entity =>
            {
                entity.HasKey(e => e.TmoId)
                    .HasName("PK__tbTipoMo__8AEAA3A3D1C9BBB4");

                entity.ToTable("tbTipoMoneda", "Gral");

                entity.Property(e => e.TmoId)
                    .HasColumnName("tmo_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TmoAbreviatura)
                    .IsRequired()
                    .HasColumnName("tmo_Abreviatura")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TmoFechaCrea)
                    .HasColumnName("tmo_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.TmoFechaModifica)
                    .HasColumnName("tmo_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.TmoNombre)
                    .IsRequired()
                    .HasColumnName("tmo_Nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.TmoUsuarioCrea).HasColumnName("tmo_UsuarioCrea");

                entity.Property(e => e.TmoUsuarioModifica).HasColumnName("tmo_UsuarioModifica");
            });

            modelBuilder.Entity<TbTipoSangre>(entity =>
            {
                entity.HasKey(e => e.TpsId);

                entity.ToTable("tbTipoSangre", "Gral");

                entity.Property(e => e.TpsId)
                    .HasColumnName("tps_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TpsFechaCrea)
                    .HasColumnName("tps_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.TpsFechaModifica)
                    .HasColumnName("tps_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.TpsNombre)
                    .IsRequired()
                    .HasColumnName("tps_nombre")
                    .HasMaxLength(3);

                entity.Property(e => e.TpsUsuarioCrea).HasColumnName("tps_UsuarioCrea");

                entity.Property(e => e.TpsUsuarioModifica).HasColumnName("tps_UsuarioModifica");
            });

            modelBuilder.Entity<TipoSolicitud>(entity =>
            {
                entity.HasKey(e => e.TipsolId)
                    .HasName("PK__tbTipoSo__1A57EB0701D0435A");

                entity.ToTable("tbTipoSolicitud", "Gral");

                entity.Property(e => e.TipsolId).HasColumnName("tipsol_Id");

                entity.Property(e => e.TipsolDescripcion)
                    .IsRequired()
                    .HasColumnName("tipsol_Descripcion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipsolFechaCrea)
                    .HasColumnName("tipsol_FechaCrea")
                    .HasColumnType("datetime");

                entity.Property(e => e.TipsolFechaModifica)
                    .HasColumnName("tipsol_FechaModifica")
                    .HasColumnType("datetime");

                entity.Property(e => e.TipsolUsuarioCrea).HasColumnName("tipsol_UsuarioCrea");

                entity.Property(e => e.TipsolUsuarioModifica).HasColumnName("tipsol_UsuarioModifica");
            });
        }
    }
}
