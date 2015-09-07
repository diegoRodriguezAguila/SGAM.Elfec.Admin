/*
 * ER/Studio 8.0 SQL Code Generation
 * Company :      Elfec
 * Project :      SGAM DB.dm1
 * Author :       Diego
 *
 * Date Created : Friday, November 07, 2014 18:58:02
 * Target DBMS : Microsoft SQL Server 2008
 */

/* 
 * TABLE: AppDeGrupo 
 */

CREATE TABLE AppDeGrupo(
    Id                  int            IDENTITY(1,1),
    AplicacionId        int            NOT NULL,
    GrupoId             int            NOT NULL,
    Estado              int            NOT NULL,
    FechaInsert         date           NOT NULL,
    FechaUpdate         date           NULL,
    UsuarioAuditoria    varchar(30)    NULL,
    CONSTRAINT PK4_1 PRIMARY KEY NONCLUSTERED (Id, AplicacionId, GrupoId)
)
go



IF OBJECT_ID('AppDeGrupo') IS NOT NULL
    PRINT '<<< CREATED TABLE AppDeGrupo >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AppDeGrupo >>>'
go

/* 
 * TABLE: Applicaciones 
 */

CREATE TABLE Applicaciones(
    Id                  int             IDENTITY(1,1),
    Nombre              varchar(30)     NOT NULL,
    Version             varchar(20)     NOT NULL,
    NombrePaquete       varchar(25)     NOT NULL,
    URLAlmacenada       varchar(100)    NULL,
    Autor               varchar(30)     NULL,
    Estado              int             NOT NULL,
    FechaInsert         datetime        NOT NULL,
    FechaUpdate         datetime        NULL,
    UsuarioAuditoria    varchar(30)     NOT NULL,
    CONSTRAINT PK1 PRIMARY KEY NONCLUSTERED (Id)
)
go



IF OBJECT_ID('Applicaciones') IS NOT NULL
    PRINT '<<< CREATED TABLE Applicaciones >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Applicaciones >>>'
go

/* 
 * TABLE: AsignacionAppMovil 
 */

CREATE TABLE AsignacionAppMovil(
    Id                  int            IDENTITY(1,1),
    DispositivoId       int            NOT NULL,
    AplicacionId        int            NOT NULL,
    GrupoId             int            NULL,
    Estado              int            NOT NULL,
    FechaInsert         date           NOT NULL,
    FechaUpdate         date           NULL,
    UsuarioAuditoria    varchar(30)    NULL,
    CONSTRAINT PK4 PRIMARY KEY NONCLUSTERED (Id, DispositivoId, AplicacionId)
)
go



IF OBJECT_ID('AsignacionAppMovil') IS NOT NULL
    PRINT '<<< CREATED TABLE AsignacionAppMovil >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AsignacionAppMovil >>>'
go

/* 
 * TABLE: DispositivosMoviles 
 */

CREATE TABLE DispositivosMoviles(
    Id                  int            IDENTITY(1,1),
    Marca               varchar(25)    NOT NULL,
    Modelo              varchar(50)    NOT NULL,
    Imei                varchar(20)    NOT NULL,
    Tipo                varchar(20)    NOT NULL,
    ResistenteAgua      int            NOT NULL,
    ResistentePolvo     int            NOT NULL,
    Camara              int            NOT NULL,
    Estado              int            NOT NULL,
    FechaInsert         date           NOT NULL,
    FechaUpdate         date           NULL,
    UsuarioAuditoria    varchar(30)    NOT NULL,
    CONSTRAINT PK2 PRIMARY KEY NONCLUSTERED (Id)
)
go



IF OBJECT_ID('DispositivosMoviles') IS NOT NULL
    PRINT '<<< CREATED TABLE DispositivosMoviles >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE DispositivosMoviles >>>'
go

/* 
 * TABLE: GruposDispositivos 
 */

CREATE TABLE GruposDispositivos(
    Id                  int             IDENTITY(1,1),
    Nombre              varchar(35)     NOT NULL,
    Descripcion         varchar(300)    NULL,
    Estado              int             NOT NULL,
    FechaInsert         date            NOT NULL,
    FechaUpdate         date            NULL,
    UsuarioAuditoria    varchar(30)     NOT NULL,
    CONSTRAINT PK3 PRIMARY KEY NONCLUSTERED (Id)
)
go



IF OBJECT_ID('GruposDispositivos') IS NOT NULL
    PRINT '<<< CREATED TABLE GruposDispositivos >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE GruposDispositivos >>>'
go

/* 
 * TABLE: MovilPerteneceGrupo 
 */

CREATE TABLE MovilPerteneceGrupo(
    Id                  int            IDENTITY(1,1),
    DispositivoId       int            NOT NULL,
    GrupoId             int            NOT NULL,
    Estado              int            NOT NULL,
    FechaInsert         date           NOT NULL,
    FechaUpdate         date           NULL,
    UsuarioAuditoria    varchar(30)    NULL,
    CONSTRAINT PK4_2 PRIMARY KEY NONCLUSTERED (Id, DispositivoId, GrupoId)
)
go



IF OBJECT_ID('MovilPerteneceGrupo') IS NOT NULL
    PRINT '<<< CREATED TABLE MovilPerteneceGrupo >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE MovilPerteneceGrupo >>>'
go

/* 
 * TABLE: AppDeGrupo 
 */

ALTER TABLE AppDeGrupo ADD CONSTRAINT RefApplicaciones6 
    FOREIGN KEY (AplicacionId)
    REFERENCES Applicaciones(Id)
go

ALTER TABLE AppDeGrupo ADD CONSTRAINT RefGruposDispositivos7 
    FOREIGN KEY (GrupoId)
    REFERENCES GruposDispositivos(Id)
go


/* 
 * TABLE: AsignacionAppMovil 
 */

ALTER TABLE AsignacionAppMovil ADD CONSTRAINT RefGruposDispositivos1 
    FOREIGN KEY (GrupoId)
    REFERENCES GruposDispositivos(Id)
go

ALTER TABLE AsignacionAppMovil ADD CONSTRAINT RefDispositivosMoviles2 
    FOREIGN KEY (DispositivoId)
    REFERENCES DispositivosMoviles(Id)
go

ALTER TABLE AsignacionAppMovil ADD CONSTRAINT RefApplicaciones3 
    FOREIGN KEY (AplicacionId)
    REFERENCES Applicaciones(Id)
go


/* 
 * TABLE: MovilPerteneceGrupo 
 */

ALTER TABLE MovilPerteneceGrupo ADD CONSTRAINT RefDispositivosMoviles4 
    FOREIGN KEY (DispositivoId)
    REFERENCES DispositivosMoviles(Id)
go

ALTER TABLE MovilPerteneceGrupo ADD CONSTRAINT RefGruposDispositivos5 
    FOREIGN KEY (GrupoId)
    REFERENCES GruposDispositivos(Id)
go


