# Levantar el proyecto
## Los pasos para levantar el proyecto ASP.NET Core
* Cambiar la cadena de conexión del Core.Api por la de su base de datos.
* Hacer un REBUILD al proyecto para restaurar los paquetes del NuGet.
* Ejecutar la migración seleccionado el proyecto Core.Api como principal y en la consola del NuGet el proyecto persistence como principal.
* Ejecutar el comando es update-database
* Como la aplicacion de fronted SPA no esta en el repo (hecha en vue 3) debe ir al repo frontend y copiar los archivos y pegarlo en KODOTI-SPA-Solution\src\SPA_Cliente\kodoti-spa
* Cambiar el puerto donde se ejecuta el proyecto SPA, por defecto configurar que apunte al 7000
