# Prueba Técnica para candidatos

# Comentarios de Néstor Campillo sobre la prueba
En primer lugar queria coentar que he tenido que sntalarme en mi máquina tanto VS, SQL, Git o Docker. No tenia mi PC configurado el desarrollo, nada mas que como simple PC doméstico. 
Esto me llevo varias horas para poder tenerlo listo. La aplicación no funcionará por el acceso a la BD, en mi máquina local no me ha dado tiempo a configrar el servidor DOcker para que funcione sin certificado
Tampoco he tenido las horas para confirurar mi firewall o la red, asique al final he dejado la funcionalidad básica de Docker. Tampoco creo que para una entrevista tenga que hacerlo todo real, con el odelo bastaría. 
Finalmente he creado una solución con un proyecto  principal para la API, con un servicio que consume de la API externa, "guarda" en base de datos la información de los bancos en este caso  y los devuelve. 
Le añadí las funciones basicas CRUD dado que para una app de prueba no hace falta irse a algo más complejo. Al igual que con el cifrado de contraseñas. 
La estructura de carpetas es básica, seprando las distintas capas que tendriamos. Logicamente en una versión real el acceso a base de datos quedaria separado en lectura y escritura, la capa de apliación o de logica tendrían que pasar por una capa de framewor para llegar a la base de datos etc..
A su vez le añadí un proyecto de test muy simple (generado por IA) porque no tenía mucho mas tiempo
El reposotorio lo he subido al gitlab: https://github.com/NestorCR/PTANetBackMVC si tienen algún problema conctactenme e intentamos solucionarlo
Muchas gracias, Néstor


## Descripción

Este repositorio contiene una prueba técnica para candidatos que deseen unirse a nuestro equipo de desarrollo backend y frontend. El objetivo de la prueba es evaluar las habilidades de los candidatos en el desarrollo de aplicaciones utilizando tecnologías como .NET, C#, SQL Server, MVC...

## Instrucciones

1. Realizar un programa en .NET - C# que cumpla con los siguientes requisitos:
    - Haz un fork de este proyecto
    - Consumir la siguiente API: [https://api.opendata.esett.com/](https://api.opendata.esett.com/). Escoge sólo 1 servicio cualquiera de los proporcionados por la API.
    - Almacenar la información obtenida en la base de datos. (usa SQL Server en contenedor de docker para esto)
    - Implementar un controlador que permita filtrar por Primary Key en la base de datos.
    - Construir una API REST con Swagger que permita visualizar los datos almacenados en la base de datos.
    - Usar contenedores Docker para DDBB y la propia App
    - Usa arquitectura MVC (sólo API imagina que existe un segundo proyecto con el frontend, por tanto las vistas serán DTOs)
    - Haz un pull request con tu nombre completo y comenta lo que creas necesario al evaluador técnico.
    - Elige entre implementar CRUD o CQRS

### Criterios de evaluación:

Se valorará positivamente (pero no es obligatorio cumplir con todos estos puntos):

1. El uso de código limpio y buenas prácticas de programación tanto en el frontend como en el backend.
2. Utilizar código generado a mano en lugar de depender excesivamente de herramientas de generación automática.
3. Hacer commits frecuentes y bien explicados durante el desarrollo.
4. Demostrar conocimientos en patrones de diseño, tanto en el frontend como en el backend.
5. Gestion correcta de los secretos como cadenas de conexión, usuarios, passwords...
6. Uso del inglés en código y comentarios
7. Uso de elementos de monitoreo y observabilidad como ILogger
8. Uso de Eventos
9. Manejo de excepciones con patron monad
10. Pruebas de test

## Tecnologías utilizadas

- .NET - C#
- SQL Server
- MVC

## Estructura del repositorio

No hay restricciones específicas sobre la estructura del repositorio. Los candidatos son libres de organizar su código de la manera que consideren más apropiada. Sin embargo, se recomienda seguir las convenciones de nomenclatura y estructura de proyecto estándar.

¡Buena suerte!
