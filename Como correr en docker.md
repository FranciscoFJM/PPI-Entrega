# Manual para correr la API en docker con docker compose

Se crearan contenedores para la API y la base de datos, por lo cual no es necesario instalar ningun complemento ni sdk para que esto funcione. La API se crea en un contenedor Linux. La data generada se guardará en el contenedor de base de datos, asi que no es necesario tener una base de datos local.
El archivo DB.sql no debe ser movido de la carpeta del proyecto, si no sera necesario actualizar el comando del docker compose para buscarlo en la ruta nueva.

La base de datos es relacional (SQL Server) y la API esta hecha en .NET 8
Requisitos:

* Instalar **Docker desktop**
* Iniciar **docker desktop**
* Instalar WSL (Subsistema de windows para Linux, se instala con Docker)
* Revisar que el `appsettings.json` del projecto `API` tenga la connection string y la sección jwt con sus propiedades vacias, ya que ellas se van a setear en el contenedor de docker como variables de entorno en el archivo .env. Deberian quedar así:

  ```json
  "ConnectionString": {
    "Server": "",
    "DataBase": "",
    "User": "",
    "Password": ""
  },
  "Jwt": {
    "Key": "",
    "ExpiryInHours": ""
  }

* Abrir terminal en la carpeta del proyecto, donde se encuentra el archivo `docker-compose.yml`

* Ejecutar este comando: `docker compose up --build`
Esto generará:
  * 2 contenedores:
    * Uno para la base de datos (`ppi_sqlserver`).
    * Otro para la API (`ppi_api_container`).
  * Un volumen para guardar la base de datos y la data que se genere (`ppi_sqlserver_data`).
  * Una red para que la API y la base de datos se puedan conectar entre ellas (`ppi_network`).

La primera vez que se corra el comando se descargaran las imagenes de docker hub, esto puede tardar un poco. La base de datos solo se creará la primera vez que se levanten los contenedores, o si se destruye el volumen de datos y se vuelven a levantar los contenedores en otro momento.
Cuando la terminal termine de desplegar todo debe salir un mensaje que diga: `Servidor de SQL listo para ser usado por la api.`

* Dirigirse a esta url: <http://localhost:3000/swagger/index.html>

* Para dar de baja los contenedores, correr este comando: `docker compose down`
* Para dar de baja los contenedores y borrar el volumen con la data almacenada correr este comando: `docker compose down -v`
