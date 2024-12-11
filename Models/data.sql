CREATE TABLE Roles(
  id_rol int,
  
  nombre_rol varchar(30) NOT NULL,
  
  estado_rol bit DEFAULT 1 NOT NULL,

  CONSTRAINT chk_nombre_rol CHECK ( nombre_rol LIKE '[A-Za-z][A-Za-z]%' ),

  CONSTRAINT un_nombre_rol UNIQUE (nombre_rol),

  CONSTRAINT pk_rol PRIMARY KEY (id_rol)
);

CREATE TABLE Permisos(
  id_permiso int,
  
  nombre_permiso varchar(30) NOT NULL,

  estado_permiso bit DEFAULT 1 NOT NULL,

  CONSTRAINT chk_nombre_permiso CHECK (  nombre_permiso LIKE '[A-Za-z][A-Za-z]%' ),

  CONSTRAINT un_nombre_permiso UNIQUE (nombre_permiso),

  CONSTRAINT pk_permiso PRIMARY KEY (id_permiso)
);

CREATE TABLE Privilegios(
  id_privilegio int,
  
  nombre_privilegio varchar(30) NOT NULL,

  CONSTRAINT chk_nombre_privilegio CHECK (  nombre_privilegio LIKE '[A-Za-z][A-Za-z]%' ),

  CONSTRAINT pk_privilegio PRIMARY KEY (id_privilegio)
);

CREATE TABLE PerXRolXPriv(
  id_perxrol int,
  
  id_per int,
  id_rol int,
  id_priv int,

  CONSTRAINT pk_perxrol PRIMARY KEY (id_perxrol),

  CONSTRAINT fk_permiso FOREIGN KEY (id_per) 
  REFERENCES Permisos(id_permiso),

  CONSTRAINT fk_rol FOREIGN KEY (id_rol) 
  REFERENCES Roles(id_rol),

  CONSTRAINT fk_priv FOREIGN KEY (id_priv) 
  REFERENCES Privilegios(id_privilegio),

  CONSTRAINT un_perxrolxpriv UNIQUE (id_per,id_rol,id_priv)
);

CREATE TABLE Usuarios (
  id_usuario INT,

  tarjeta_identidad VARCHAR(50) NOT NULL UNIQUE,
  correo_usuario VARCHAR(100) NOT NULL UNIQUE,
  
  nombre_usuario VARCHAR(100) NOT NULL,
  contrasena_usuario VARCHAR(100) NOT NULL,
  cumpleano_usuario DATE NOT NULL,
  fecha_creacion_usuario DATE NOT NULL,

  -- The gener is necessary because this information give us more information about customers
  genero_usuario VARCHAR(5) NOT NULL,

  estado_usuario bit NOT NULL DEFAULT 1,
  
  fk_rol INT NOT NULL,

  CONSTRAINT pk_user PRIMARY KEY (id_usuario),

  CONSTRAINT fk_rol_user FOREIGN KEY (fk_rol) REFERENCES Roles(id_rol),

  CONSTRAINT un_tarjeta_identidad UNIQUE (tarjeta_identidad),
  CONSTRAINT un_correo_usuario UNIQUE (correo_usuario),

  CONSTRAINT chk_tarjeta_identidad CHECK (  tarjeta_identidad LIKE  '[0-9][0-9]%' ),
  CONSTRAINT chk_correo_usuario CHECK ( 
    correo_usuario 
    LIKE 
    '[A-Za-z0-9._%+-][A-Za-z0-9._%+-]%@[A-Za-z0-9._%+-]%.[A-Za-z0-9._%+-][A-Za-z0-9._%+-][A-Za-z0-9._%+-]%'
    ),

  CONSTRAINT chk_nombre_usuario CHECK (  nombre_usuario LIKE '[A-Za-z][A-Za-z]%' ),
  CONSTRAINT chk_pw_usuario CHECK ( LEN(contrasena_usuario) >= 8 AND LEN(contrasena_usuario) <= 20 ),
  
  CONSTRAINT chk_cumpleano_usuario CHECK ( cumpleano_usuario <= CAST( GETDATE() AS DATE )
  AND cumpleano_usuario != fecha_creacion_usuario),

  -- can be Male, female or not binary
  CONSTRAINT chk_gender_user CHECK ( genero_usuario IN ('F','M') OR genero_usuario LIKE 'NBIN' ),
);
