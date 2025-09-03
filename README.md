# PPI API - Sistema de Gestión de Órdenes de Inversión

## Descripción General

Esta API REST está desarrollada en .NET 8 para la gestión de órdenes de inversión, permitiendo operaciones con diferentes tipos de activos financieros como acciones, bonos y fondos comunes de inversión (FCI).
Solo hay CRUD para ordenes.
Para cargar una orden se debe cargar el activo correcto segun el tipo de orden, y el tipo de operación debe existir en la base de datos.
Se pueden listar todas las ordenes, listarlas por activo, tipo de activo, tipo de orden y por estado.
El resto de entidades (Activos, tipos de activos, tipos de operaciones, estados de ordenes) Se pueden listár.

## Archivos incluidos

- Readme general (Este archivo)
- `Como correr en docker.md` > Manual para desplegar el proyecto en docker.
- `openapi.yml` > Especificacion de api con todos los endpoints del proyecto API. Hecho en OpenAPI 3.0.3.
- `docker-compose.yml` > Archivo docker compose para desplegar la API y su base de datos en contenedores docker.
- `DB.sql` > Script para crear y poblar la base de datos con la info del pdf.
- `.env` > Variables de entorno para el despliegue en docker.

## Arquitectura del Proyecto

El proyecto sigue una arquitectura limpia (Clean Architecture) dividida en múltiples capas:

### **API** (`apps/API/`)

Proyecto principal que contiene los controladores REST y la configuración de la aplicación. Es el punto de entrada de la aplicación y maneja:

- Controladores REST
- Configuraciones de autenticación JWT
- Configuraciones de Swagger
- DTOs
- Filtros de validación

### **Application** (`src/core/Application/`)

Capa de aplicación que contiene la lógica de negocio. Implementa:

- Servicios de aplicación que orquestan las operaciones
- Implementación de interfaces de servicios
- Lógica de negocio
- Validaciones con FluentValidation

### **Domain** (`src/core/Domain/`)

Núcleo del dominio que contiene:

- Modelos que se compartiran entre la API y la capa de persistencia.
- Interfaces de repositorios
- Interfaces de servicios

### **Base** (`src/core/Base/`)

Capa base que proporciona:

- Tipos y clases base comunes
- Excepciones personalizadas

### **Persistence** (`src/infrastructure/Persistence/`)

Capa de infraestructura para el acceso a datos que maneja:

- Entity Framework Core como ORM
- Configuración de base de datos SQL Server
- Implementación de repositorios
- Entidades de base de datos
- Contexto de base de datos (PPIContext)

## Controladores y Endpoints

### 🔐 AuthController (`/api/Auth`)

#### `POST /login`

**Descripción**: Autentica al usuario y genera un token JWT. Se puede usar cualquier usuario/contraseña ya que no hay tabla de usuarios en la base de datos, esto es solo a modo de conseguir facilmente un token para poder utilizar las apis.

**Parámetros**:

- `loginRequest` (body): Credenciales de usuario

**Respuesta**: Token JWT para autorización

---

#### `GET /verify` 🔒

**Descripción**: Endpoint de prueba para verificar que la autenticación JWT funciona.

**Respuesta**: Información del usuario autenticado

---

### AssetController (`/api/Asset`) 🔒

#### `GET /`

**Descripción**: Obtiene todos los activos disponibles.

**Respuesta**: Lista de todos los activos

---

#### `GET /{id}`

**Descripción**: Obtiene un activo específico por su Id.

**Parámetros**:

- `id` : Id único del activo

**Respuesta**: Activo encontrado o NotFound si no existe

---

#### `GET /GetAssetsByAssetTypeId/{assetTypeId}`

**Descripción**: Obtiene todos los activos que pertenecen a un tipo específico. 1 = Acción, 2 = Bonos, 3 = FCI.

**Parámetros**:

- `assetTypeId` : Id del tipo de activo

**Respuesta**: Lista de activos del tipo especificado o NotFound si no existen

---

### AssetTypeController (`/api/AssetType`) 🔒

#### `GET /`

**Descripción**: Obtiene todos los tipos de activos disponibles.

**Respuesta**: Lista de todos los tipos de activos

---

#### `GET /{id}`

**Descripción**: Obtiene un tipo de activo específico por su Id. 1 = Acción, 2 = Bonos, 3 = FCI.

**Parámetros**:

- `id`: Id único del tipo de activo

**Respuesta**: Tipo de activo encontrado o NotFound si no existe

---

### OperationTypeController (`/api/OperationType`) 🔒

#### `GET /`

**Descripción**: Obtiene todos los tipos de operaciones disponibles.

**Respuesta**: Lista de todos los tipos de operaciones

---

#### `GET /{id}`

**Descripción**: Obtiene un tipo de operación específico por su Id. C = Compra, V = Venta.

**Parámetros**:

- `id` : Id único del tipo de operación

**Respuesta**: Tipo de operación encontrado o NotFound si no existe

---

### OrderController (`/api/Order`) 🔒

#### `GET /`

**Descripción**: Obtiene todas las órdenes disponibles.

**Respuesta**: Lista de todas las órdenes disponibles

---

#### `GET /{id}`

**Descripción**: Obtiene una orden específica por su Id. Esta orden vendrá con información detallada. Se incluyen las descripciones del activo, tipo de activo, tipo de operación y estado de la orden.

**Parámetros**:

- `id` : Id único de la orden

**Respuesta**: Orden encontrada o NotFound si no existe

---

#### `GET /asset/{assetId}`

**Descripción**: Obtiene todas las órdenes asociadas a un activo específico.

**Parámetros**:

- `assetId` : Id del activo

**Respuesta**: Lista de órdenes del activo especificado

---

#### `GET /account/{accountId}`

**Descripción**: Obtiene todas las órdenes asociadas a una cuenta específica.

**Parámetros**:

- `accountId` : Id de la cuenta

**Respuesta**: Lista de órdenes de la cuenta especificada

---

#### `GET /status/{statusId}`

**Descripción**: Obtiene todas las órdenes que tienen un estado específico. 0 = En proceso, 1= Ejecutada, 3 = Cancelada.

**Parámetros**:

- `statusId` : Id del estado de la orden

**Respuesta**: Lista de órdenes con el estado especificado

---

#### `GET /operation/{operationTypeId}`

**Descripción**: Obtiene todas las órdenes que tienen un tipo de operación específico. C = Compra, V = Venta.

**Parámetros**:

- `operationTypeId` : Id del tipo de operación

**Respuesta**: Lista de órdenes con el tipo de operación especificado

---

#### `GET /assettype/{assetTypeId}`

**Descripción**: Obtiene todas las órdenes asociadas a un tipo de activo específico. 1 = Acción, 2 = Bono, 3 = FCI.

**Parámetros**:

- `assetTypeId` : Id del tipo de activo

**Respuesta**: Lista de órdenes del tipo de activo especificado

---

#### `POST /Stock`

**Descripción**: Crea una nueva orden con Activo tipo Acción. Solo se pueden usar los Activos de tipo Acción (AssetTypeId = 1). No se ingresa el precio, se obtiene del activo. Se deben discriminar comisiones e impuestos:

- **Comisiones**: 0.6% sobre el "Monto Total"
- **Impuestos**: 21% sobre el valor de las comisiones

**Parámetros**:

- `createOrderDto` (body): Datos de la orden

**Respuesta**: Resultado de la operación de creación

---

#### `POST /Bond`

**Descripción**: Crea una nueva orden con Activo tipo Bono. Solo se pueden usar los Activos de tipo Bono (AssetTypeId = 2). Se ingresan el precio y la cantidad. Se deben discriminar comisiones e impuestos:

- **Comisiones**: 0.2% sobre el "Monto Total"
- **Impuestos**: 21% sobre el valor de las comisiones

**Parámetros**:

- `createOrderDto` (body): Datos de la orden

**Respuesta**: Resultado de la operación de creación

---

#### `POST /MutualFund`

**Descripción**: Crea una nueva orden de Activo tipo FCI. Solo se pueden usar los Activos de tipo FCI (AssetTypeId = 3). No se aplican comisiones ni impuestos.

**Parámetros**:

- `createOrderDto` (body): Datos de la orden

**Respuesta**: Resultado de la operación de creación

---

#### `PATCH /{id}/status`

**Descripción**: Actualiza el estado de una orden existente. Se puede cambiar el estado de la orden a:

- **0** = En proceso
- **1** = Ejecutada  
- **3** = Cancelada

**Parámetros**:

- `id` : Id de la orden a actualizar
- `updateOrderDto` (body): Datos para actualizar el estado de la orden. De momento es solo el id de estado pero a futuro se pueden agregar mas propiedades.

**Respuesta**: Resultado de la operación de actualización

---

#### `DELETE /{id}`

**Descripción**: Elimina una orden del sistema.

**Parámetros**:

- `id` : Id de la orden a eliminar

**Respuesta**: Resultado de la operación de eliminación

---

### OrderStateController (`/api/OrderState`) 🔒

#### `GET /`

**Descripción**: Obtiene todos los estados de órdenes disponibles.

**Respuesta**: Lista de todos los estados de órdenes

---

#### `GET /{id}`

**Descripción**: Obtiene un estado de orden específico por su Id. 0 = En proceso, 1= Ejecutada, 3 = Cancelada.

**Parámetros**:

- `id` : Id único del estado de orden

**Respuesta**: Estado de orden encontrado o NotFound si no existe

---

## Tipos de Datos

### Estados de Orden

- **0**: En proceso
- **1**: Ejecutada
- **3**: Cancelada

### Tipos de Activos

- **1**: Acción
- **2**: Bonos
- **3**: FCI

### Tipos de Operación

- **C**: Compra
- **V**: Venta

## Comisiones e Impuestos

### Acciones (Stock)

- **Comisiones**: 0.6% sobre el monto total
- **Impuestos**: 21% sobre el valor de las comisiones
- **Precio**: Se obtiene automáticamente del activo

### Bonos (Bond)

- **Comisiones**: 0.2% sobre el monto total
- **Impuestos**: 21% sobre el valor de las comisiones
- **Precio**: Se debe especificar manualmente

### FCI (Mutual Fund)

- **Comisiones**: 0%
- **Impuestos**: 0%
- **Precio**: Se obtiene automáticamente del activo

## Tecnologías Utilizadas

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM para acceso a datos
- **SQL Server**: Base de datos
- **JWT**: Autenticación y autorización
- **Swagger/OpenAPI**: Documentación de API
- **FluentValidation**: Validación de modelos

## Como correr el proyecto

1. **Con Docker**: Seguir las instrucciones en `Como correr en docker.md`
2. **Local**:
   - Crear la base de datos con el archivo DB.SQL
   - Configurar la connection string y los parametros de JWT en `appsettings.json`
   Ejemplo:

  ```json
      "ConnectionString": {
    "Server": "FRANCISCO\\SQLEXPRESS",
    "DataBase": "PPI",
    "User": "",
    "Password": ""
  },
  "Jwt": {
    "Key": "PPI_JWT_KEY_SHA256_AUTHORIZATION_113355778899",
    "ExpiryInHours": "24"
  }
  ```

   La key del jwt debe tener como minimo 32 caracteres.
   Si no se colocan user y password en la ConnectionString, se usara Windows Authentication, y si se colocan, se usará SQL Authentication. Recomiendo dejarlas en blanco siempre, ya que solo se colocaran credenciales cuando se despliegue la API con Docker

3. Ejecutar el proyecto API

# Notas Importantes

- 🔒 = Controlador/Metodo que requiere autenticación JWT
- Se debe obtener un token primero antes de poder llamar a los endpoints de la API. La autenticación es solo para demostración, acepta cualquier usuario/contraseña, pero el token que genera es necesario. El token se debe refrescar cada vez que se inicia el proyecto o se recarga la pagina con el swagger.
- Todos los endpoints excepto `/api/Auth/login` requieren autenticación
- La API incluye validaciones con FluentValidation para todos los DTOs de entrada

# Autenticación JWT

### Usar la autenticación en Swagger

1. **Obtener el token**:
   - Ir a este endpoint: `/api/Auth/login`
   - Usa cualquier usuario/contraseña (no hay validación real)
   - Copia el token de la respuesta

2. **Autorizar en Swagger**:
   - Hacer clic en el botón "Authorize" en la parte superior derecha
   - En el modal que aparece, ingresar: `Bearer {token}`
   - Ejemplo: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9eyJ1bmlxdWVfbmFtZSI6InN0cmluZyIsIm...`
   - Hacer clic en "Authorize"

3. **Realizar peticiones**:
   - Ahora todos los endpoints protegidos incluirán automáticamente el header de autorización

### Endpoint de verificación

Se puede llamar a este endpoint para verificar que el token funciona: `/api/Auth/verify`
