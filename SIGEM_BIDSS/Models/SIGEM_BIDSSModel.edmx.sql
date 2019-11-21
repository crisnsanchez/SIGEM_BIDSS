
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/21/2019 10:09:11
-- Generated from EDMX file: C:\Users\crist\Documents\GitHub\SIGEM_BIDSS\SIGEM_BIDSS\Models\SIGEM_BIDSSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SIGEM_BIDSS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Gral].[FK_are_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbPuesto] DROP CONSTRAINT [FK_are_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_dep_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbMunicipio] DROP CONSTRAINT [FK_dep_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_est_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbEmpleado] DROP CONSTRAINT [FK_est_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbEmpleado] DROP CONSTRAINT [FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbArea_are_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbArea_are_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbEmpleado_emp_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbEmpleado_emp_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbMoneda_tmo_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbMoneda_tmo_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbPuesto_pto_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbPuesto_pto_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbTipoMovimiento_tipmo_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbTipoMovimiento_tipmo_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbTipoSalario_tpsal_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbTipoSalario_tpsal_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbTipoSolicitud_tipsol_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbTipoSolicitud_tipsol_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_Gral_tbSolicitud_tbTipoViatico_tpv_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbSolicitud] DROP CONSTRAINT [FK_Gral_tbSolicitud_tbTipoViatico_tpv_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_pto_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbEmpleado] DROP CONSTRAINT [FK_pto_Id];
GO
IF OBJECT_ID(N'[Gral].[FK_tps_Id]', 'F') IS NOT NULL
    ALTER TABLE [Gral].[tbEmpleado] DROP CONSTRAINT [FK_tps_Id];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Gral].[tbArea]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbArea];
GO
IF OBJECT_ID(N'[Gral].[tbDepartamento]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbDepartamento];
GO
IF OBJECT_ID(N'[Gral].[tbEmpleado]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbEmpleado];
GO
IF OBJECT_ID(N'[Gral].[tbEstado]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbEstado];
GO
IF OBJECT_ID(N'[Gral].[tbMoneda]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbMoneda];
GO
IF OBJECT_ID(N'[Gral].[tbMunicipio]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbMunicipio];
GO
IF OBJECT_ID(N'[Gral].[tbPuesto]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbPuesto];
GO
IF OBJECT_ID(N'[Gral].[tbSolicitud]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbSolicitud];
GO
IF OBJECT_ID(N'[Gral].[tbTipoMovimiento]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoMovimiento];
GO
IF OBJECT_ID(N'[Gral].[tbTipoPermiso]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoPermiso];
GO
IF OBJECT_ID(N'[Gral].[tbTipoSalario]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoSalario];
GO
IF OBJECT_ID(N'[Gral].[tbTipoSangre]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoSangre];
GO
IF OBJECT_ID(N'[Gral].[tbTipoSolicitud]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoSolicitud];
GO
IF OBJECT_ID(N'[Gral].[tbTipoViatico]', 'U') IS NOT NULL
    DROP TABLE [Gral].[tbTipoViatico];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbArea'
CREATE TABLE [dbo].[tbArea] (
    [are_Id] int IDENTITY(1,1) NOT NULL,
    [are_Descripcion] nvarchar(50)  NOT NULL,
    [are_UsuarioCrea] int  NOT NULL,
    [are_FechaCrea] datetime  NOT NULL,
    [are_UsuarioModifica] int  NULL,
    [are_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbDepartamento'
CREATE TABLE [dbo].[tbDepartamento] (
    [dep_Codigo] char(2)  NOT NULL,
    [dep_Nombre] nvarchar(100)  NOT NULL,
    [dep_UsuarioCrea] int  NOT NULL,
    [dep_FechaCrea] datetime  NOT NULL,
    [dep_UsuarioModifica] int  NULL,
    [dep_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbEmpleado'
CREATE TABLE [dbo].[tbEmpleado] (
    [emp_Id] smallint IDENTITY(1,1) NOT NULL,
    [emp_Nombres] nvarchar(100)  NOT NULL,
    [emp_Apellidos] nvarchar(100)  NOT NULL,
    [emp_Sexo] char(1)  NOT NULL,
    [emp_FechaNacimiento] nvarchar(50)  NOT NULL,
    [emp_Identificacion] nvarchar(max)  NOT NULL,
    [emp_Telefono] nvarchar(25)  NOT NULL,
    [emp_CorreoElectronico] nvarchar(50)  NULL,
    [tps_Id] int  NOT NULL,
    [pto_Id] int  NOT NULL,
    [emp_FechaIngreso] varchar(50)  NOT NULL,
    [emp_Direccion] nvarchar(250)  NOT NULL,
    [emp_RazonInactivacion] nvarchar(500)  NULL,
    [est_Id] int  NOT NULL,
    [emp_PathImage] varchar(max)  NULL,
    [mun_Id] char(4)  NOT NULL,
    [emp_UsuarioCrea] int  NULL,
    [emp_FechaCrea] datetime  NULL,
    [emp_UsuarioModifica] int  NULL,
    [emp_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbEstado'
CREATE TABLE [dbo].[tbEstado] (
    [est_Id] int IDENTITY(1,1) NOT NULL,
    [est_Descripcion] nvarchar(100)  NOT NULL,
    [est_UsuarioCrea] int  NOT NULL,
    [est_FechaCrea] datetime  NOT NULL,
    [est_UsuarioModifica] int  NULL,
    [est_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbMunicipio'
CREATE TABLE [dbo].[tbMunicipio] (
    [mun_codigo] char(4)  NOT NULL,
    [dep_codigo] char(2)  NOT NULL,
    [mun_nombre] nvarchar(100)  NOT NULL,
    [mun_UsuarioCrea] int  NOT NULL,
    [mun_FechaCrea] datetime  NOT NULL,
    [mun_UsuarioModifica] int  NULL,
    [mun_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbPuesto'
CREATE TABLE [dbo].[tbPuesto] (
    [pto_Id] int IDENTITY(1,1) NOT NULL,
    [are_Id] int  NOT NULL,
    [pto_Descripcion] nvarchar(150)  NOT NULL,
    [pto_UsuarioCrea] int  NOT NULL,
    [pto_FechaCrea] datetime  NOT NULL,
    [pto_UsuarioModifica] int  NULL,
    [pto_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoMoneda'
CREATE TABLE [dbo].[tbTipoMoneda] (
    [tmo_Id] smallint IDENTITY(1,1) NOT NULL,
    [tmo_Abreviatura] varchar(4)  NOT NULL,
    [tmo_Nombre] nvarchar(100)  NOT NULL,
    [tmo_UsuarioCrea] int  NOT NULL,
    [tmo_FechaCrea] datetime  NOT NULL,
    [tmo_UsuarioModifica] int  NULL,
    [tmo_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoMovimiento'
CREATE TABLE [dbo].[tbTipoMovimiento] (
    [tipmo_id] int  NOT NULL,
    [tipmo_Movimiento] varchar(25)  NOT NULL,
    [tipmo_UsuarioCrea] int  NOT NULL,
    [tipmo_FechaCrea] datetime  NOT NULL,
    [tipmo_UsuarioModifica] int  NULL,
    [tipmo_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoSalario'
CREATE TABLE [dbo].[tbTipoSalario] (
    [tpsal_id] int  NOT NULL,
    [tpsal_Descripcion] nvarchar(50)  NOT NULL,
    [tpsal_UsuarioCrea] int  NOT NULL,
    [tpsal_FechaCrea] datetime  NOT NULL,
    [tpsal_UsuarioModifica] int  NULL,
    [tpsal_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoSangre'
CREATE TABLE [dbo].[tbTipoSangre] (
    [tps_Id] int IDENTITY(1,1) NOT NULL,
    [tps_nombre] nvarchar(3)  NOT NULL,
    [tps_UsuarioCrea] int  NOT NULL,
    [tps_FechaCrea] datetime  NOT NULL,
    [tps_UsuarioModifica] int  NULL,
    [tps_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoSolicitud'
CREATE TABLE [dbo].[tbTipoSolicitud] (
    [tipsol_Id] int IDENTITY(1,1) NOT NULL,
    [tipsol_Descripcion] varchar(50)  NOT NULL,
    [tipsol_UsuarioCrea] int  NOT NULL,
    [tipsol_FechaCrea] datetime  NOT NULL,
    [tipsol_UsuarioModifica] int  NULL,
    [tipsol_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbTipoViatico'
CREATE TABLE [dbo].[tbTipoViatico] (
    [tpv_Id] int IDENTITY(1,1) NOT NULL,
    [tpv_Descripcion] nvarchar(100)  NOT NULL,
    [tpv_UsuarioCrea] int  NOT NULL,
    [tpv_FechaCrea] datetime  NOT NULL,
    [tpv_UsuarioModifica] int  NULL,
    [tpv_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbMoneda'
CREATE TABLE [dbo].[tbMoneda] (
    [tmo_Id] smallint IDENTITY(1,1) NOT NULL,
    [tmo_Abreviatura] varchar(4)  NOT NULL,
    [tmo_Nombre] nvarchar(100)  NOT NULL,
    [tmo_UsuarioCrea] int  NOT NULL,
    [tmo_FechaCrea] datetime  NOT NULL,
    [tmo_UsuarioModifica] int  NULL,
    [tmo_FechaModifica] datetime  NULL
);
GO

-- Creating table 'tbSolicitud'
CREATE TABLE [dbo].[tbSolicitud] (
    [sol_Id] int IDENTITY(1,1) NOT NULL,
    [emp_Id] smallint  NOT NULL,
    [tipsol_Id] int  NOT NULL,
    [pto_Id] int  NOT NULL,
    [tpsal_id] int  NOT NULL,
    [tmo_Id] smallint  NOT NULL,
    [are_Id] int  NOT NULL,
    [tipmo_id] int  NOT NULL,
    [tpv_Id] int  NOT NULL,
    [sol_GralDescripcion] nvarchar(50)  NOT NULL,
    [sol_GralJefeInmediato] nvarchar(100)  NULL,
    [sol_GralCorreoJefeInmediato] nvarchar(50)  NULL,
    [sol_GralComentario] nvarchar(50)  NULL,
    [sol_GralJustificacion] nvarchar(50)  NULL,
    [sol_GralFechaSolicitud] datetime  NOT NULL,
    [sol_AnviFechaViaje] datetime  NOT NULL,
    [sol_Anvi_Cliente] nvarchar(100)  NOT NULL,
    [sol_Anvi_LugarDestino] nvarchar(50)  NOT NULL,
    [sol_Acper_Anterior] nvarchar(50)  NOT NULL,
    [sol_Anvi_PropositoVisita] nvarchar(50)  NOT NULL,
    [sol_Anvi_DiasVisita] int  NOT NULL,
    [sol_AnviHospedaje] nvarchar(50)  NOT NULL,
    [sol_AnviTrasladoHacia] nvarchar(50)  NOT NULL,
    [sol_AnsolMonto] float  NOT NULL,
    [sol_perFechaRegreso] datetime  NOT NULL,
    [sol_perMedioDia] bit  NOT NULL,
    [sol_perCantidadDias] int  NOT NULL,
    [sol_ReemMonto] float  NOT NULL,
    [sol_ReemFechaMonto] datetime  NOT NULL,
    [sol_ReemProveedor] nchar(10)  NOT NULL,
    [sol_ReemCargoA] nvarchar(50)  NOT NULL,
    [sol_ReemFechaGastos] datetime  NOT NULL,
    [sol_ReemNoFactura] nvarchar(50)  NOT NULL,
    [sol_ReemMontoTotal] float  NOT NULL,
    [sol_AprRtn] nvarchar(50)  NOT NULL,
    [sol_AprNombreEmpresa] nvarchar(50)  NOT NULL,
    [sol_AprCiudad] nvarchar(50)  NOT NULL,
    [sol_AprDireccion] nvarchar(50)  NOT NULL,
    [sol_ApreTelefono] nvarchar(50)  NOT NULL,
    [sol_ApreContactoAdm] nvarchar(50)  NOT NULL,
    [sol_ApreCorreoAdm] nvarchar(50)  NOT NULL,
    [sol_ApreNombreTecn] nvarchar(50)  NOT NULL,
    [sol_ApreTelefonoTecn] nvarchar(50)  NOT NULL,
    [sol_ApreCorreoTecn] nvarchar(50)  NOT NULL,
    [sol_ApreCargoTecn] nvarchar(50)  NOT NULL,
    [sol_ApreLink] nvarchar(50)  NOT NULL,
    [sol_Acper_Nuevo] nvarchar(50)  NOT NULL,
    [sol_RequeCantidad] float  NULL,
    [sol_UsuarioCrea] int  NOT NULL,
    [sol_FechaCrea] datetime  NOT NULL,
    [sol_UsuarioModifica] int  NOT NULL,
    [sol_FechaModifica] datetime  NOT NULL
);
GO

-- Creating table 'tbTipoPermiso'
CREATE TABLE [dbo].[tbTipoPermiso] (
    [tperm_Id] int IDENTITY(1,1) NOT NULL,
    [tperm_Descripcion] varchar(50)  NOT NULL,
    [tperm_UsuarioCrea] int  NOT NULL,
    [tperm_FechaCrea] datetime  NOT NULL,
    [tperm_UsuarioModifica] int  NULL,
    [tperm_FechaModifica] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [are_Id] in table 'tbArea'
ALTER TABLE [dbo].[tbArea]
ADD CONSTRAINT [PK_tbArea]
    PRIMARY KEY CLUSTERED ([are_Id] ASC);
GO

-- Creating primary key on [dep_Codigo] in table 'tbDepartamento'
ALTER TABLE [dbo].[tbDepartamento]
ADD CONSTRAINT [PK_tbDepartamento]
    PRIMARY KEY CLUSTERED ([dep_Codigo] ASC);
GO

-- Creating primary key on [emp_Id] in table 'tbEmpleado'
ALTER TABLE [dbo].[tbEmpleado]
ADD CONSTRAINT [PK_tbEmpleado]
    PRIMARY KEY CLUSTERED ([emp_Id] ASC);
GO

-- Creating primary key on [est_Id] in table 'tbEstado'
ALTER TABLE [dbo].[tbEstado]
ADD CONSTRAINT [PK_tbEstado]
    PRIMARY KEY CLUSTERED ([est_Id] ASC);
GO

-- Creating primary key on [mun_codigo] in table 'tbMunicipio'
ALTER TABLE [dbo].[tbMunicipio]
ADD CONSTRAINT [PK_tbMunicipio]
    PRIMARY KEY CLUSTERED ([mun_codigo] ASC);
GO

-- Creating primary key on [pto_Id] in table 'tbPuesto'
ALTER TABLE [dbo].[tbPuesto]
ADD CONSTRAINT [PK_tbPuesto]
    PRIMARY KEY CLUSTERED ([pto_Id] ASC);
GO

-- Creating primary key on [tmo_Id] in table 'tbTipoMoneda'
ALTER TABLE [dbo].[tbTipoMoneda]
ADD CONSTRAINT [PK_tbTipoMoneda]
    PRIMARY KEY CLUSTERED ([tmo_Id] ASC);
GO

-- Creating primary key on [tipmo_id] in table 'tbTipoMovimiento'
ALTER TABLE [dbo].[tbTipoMovimiento]
ADD CONSTRAINT [PK_tbTipoMovimiento]
    PRIMARY KEY CLUSTERED ([tipmo_id] ASC);
GO

-- Creating primary key on [tpsal_id] in table 'tbTipoSalario'
ALTER TABLE [dbo].[tbTipoSalario]
ADD CONSTRAINT [PK_tbTipoSalario]
    PRIMARY KEY CLUSTERED ([tpsal_id] ASC);
GO

-- Creating primary key on [tps_Id] in table 'tbTipoSangre'
ALTER TABLE [dbo].[tbTipoSangre]
ADD CONSTRAINT [PK_tbTipoSangre]
    PRIMARY KEY CLUSTERED ([tps_Id] ASC);
GO

-- Creating primary key on [tipsol_Id] in table 'tbTipoSolicitud'
ALTER TABLE [dbo].[tbTipoSolicitud]
ADD CONSTRAINT [PK_tbTipoSolicitud]
    PRIMARY KEY CLUSTERED ([tipsol_Id] ASC);
GO

-- Creating primary key on [tpv_Id] in table 'tbTipoViatico'
ALTER TABLE [dbo].[tbTipoViatico]
ADD CONSTRAINT [PK_tbTipoViatico]
    PRIMARY KEY CLUSTERED ([tpv_Id] ASC);
GO

-- Creating primary key on [tmo_Id] in table 'tbMoneda'
ALTER TABLE [dbo].[tbMoneda]
ADD CONSTRAINT [PK_tbMoneda]
    PRIMARY KEY CLUSTERED ([tmo_Id] ASC);
GO

-- Creating primary key on [sol_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [PK_tbSolicitud]
    PRIMARY KEY CLUSTERED ([sol_Id] ASC);
GO

-- Creating primary key on [tperm_Id] in table 'tbTipoPermiso'
ALTER TABLE [dbo].[tbTipoPermiso]
ADD CONSTRAINT [PK_tbTipoPermiso]
    PRIMARY KEY CLUSTERED ([tperm_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [are_Id] in table 'tbPuesto'
ALTER TABLE [dbo].[tbPuesto]
ADD CONSTRAINT [FK_are_Id]
    FOREIGN KEY ([are_Id])
    REFERENCES [dbo].[tbArea]
        ([are_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_are_Id'
CREATE INDEX [IX_FK_are_Id]
ON [dbo].[tbPuesto]
    ([are_Id]);
GO

-- Creating foreign key on [dep_codigo] in table 'tbMunicipio'
ALTER TABLE [dbo].[tbMunicipio]
ADD CONSTRAINT [FK_dep_Id]
    FOREIGN KEY ([dep_codigo])
    REFERENCES [dbo].[tbDepartamento]
        ([dep_Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dep_Id'
CREATE INDEX [IX_FK_dep_Id]
ON [dbo].[tbMunicipio]
    ([dep_codigo]);
GO

-- Creating foreign key on [est_Id] in table 'tbEmpleado'
ALTER TABLE [dbo].[tbEmpleado]
ADD CONSTRAINT [FK_est_Id]
    FOREIGN KEY ([est_Id])
    REFERENCES [dbo].[tbEstado]
        ([est_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_est_Id'
CREATE INDEX [IX_FK_est_Id]
ON [dbo].[tbEmpleado]
    ([est_Id]);
GO

-- Creating foreign key on [mun_Id] in table 'tbEmpleado'
ALTER TABLE [dbo].[tbEmpleado]
ADD CONSTRAINT [FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id]
    FOREIGN KEY ([mun_Id])
    REFERENCES [dbo].[tbMunicipio]
        ([mun_codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id'
CREATE INDEX [IX_FK_Gral_tbMunicipio_Gral_tbEmpleado_emp_Id]
ON [dbo].[tbEmpleado]
    ([mun_Id]);
GO

-- Creating foreign key on [pto_Id] in table 'tbEmpleado'
ALTER TABLE [dbo].[tbEmpleado]
ADD CONSTRAINT [FK_pto_Id]
    FOREIGN KEY ([pto_Id])
    REFERENCES [dbo].[tbPuesto]
        ([pto_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_pto_Id'
CREATE INDEX [IX_FK_pto_Id]
ON [dbo].[tbEmpleado]
    ([pto_Id]);
GO

-- Creating foreign key on [tps_Id] in table 'tbEmpleado'
ALTER TABLE [dbo].[tbEmpleado]
ADD CONSTRAINT [FK_tps_Id]
    FOREIGN KEY ([tps_Id])
    REFERENCES [dbo].[tbTipoSangre]
        ([tps_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tps_Id'
CREATE INDEX [IX_FK_tps_Id]
ON [dbo].[tbEmpleado]
    ([tps_Id]);
GO

-- Creating foreign key on [are_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbArea_are_Id]
    FOREIGN KEY ([are_Id])
    REFERENCES [dbo].[tbArea]
        ([are_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbArea_are_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbArea_are_Id]
ON [dbo].[tbSolicitud]
    ([are_Id]);
GO

-- Creating foreign key on [emp_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbEmpleado_emp_Id]
    FOREIGN KEY ([emp_Id])
    REFERENCES [dbo].[tbEmpleado]
        ([emp_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbEmpleado_emp_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbEmpleado_emp_Id]
ON [dbo].[tbSolicitud]
    ([emp_Id]);
GO

-- Creating foreign key on [tmo_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbMoneda_tmo_Id]
    FOREIGN KEY ([tmo_Id])
    REFERENCES [dbo].[tbMoneda]
        ([tmo_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbMoneda_tmo_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbMoneda_tmo_Id]
ON [dbo].[tbSolicitud]
    ([tmo_Id]);
GO

-- Creating foreign key on [pto_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbPuesto_pto_Id]
    FOREIGN KEY ([pto_Id])
    REFERENCES [dbo].[tbPuesto]
        ([pto_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbPuesto_pto_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbPuesto_pto_Id]
ON [dbo].[tbSolicitud]
    ([pto_Id]);
GO

-- Creating foreign key on [tipmo_id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbTipoMovimiento_tipmo_Id]
    FOREIGN KEY ([tipmo_id])
    REFERENCES [dbo].[tbTipoMovimiento]
        ([tipmo_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbTipoMovimiento_tipmo_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbTipoMovimiento_tipmo_Id]
ON [dbo].[tbSolicitud]
    ([tipmo_id]);
GO

-- Creating foreign key on [tpsal_id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbTipoSalario_tpsal_Id]
    FOREIGN KEY ([tpsal_id])
    REFERENCES [dbo].[tbTipoSalario]
        ([tpsal_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbTipoSalario_tpsal_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbTipoSalario_tpsal_Id]
ON [dbo].[tbSolicitud]
    ([tpsal_id]);
GO

-- Creating foreign key on [tipsol_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbTipoSolicitud_tipsol_Id]
    FOREIGN KEY ([tipsol_Id])
    REFERENCES [dbo].[tbTipoSolicitud]
        ([tipsol_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbTipoSolicitud_tipsol_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbTipoSolicitud_tipsol_Id]
ON [dbo].[tbSolicitud]
    ([tipsol_Id]);
GO

-- Creating foreign key on [tpv_Id] in table 'tbSolicitud'
ALTER TABLE [dbo].[tbSolicitud]
ADD CONSTRAINT [FK_Gral_tbSolicitud_tbTipoViatico_tpv_Id]
    FOREIGN KEY ([tpv_Id])
    REFERENCES [dbo].[tbTipoViatico]
        ([tpv_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gral_tbSolicitud_tbTipoViatico_tpv_Id'
CREATE INDEX [IX_FK_Gral_tbSolicitud_tbTipoViatico_tpv_Id]
ON [dbo].[tbSolicitud]
    ([tpv_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------