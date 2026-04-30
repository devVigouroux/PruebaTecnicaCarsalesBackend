# API de Contactos — Prueba Técnica Carsales

API REST para la gestión de contactos desarrollada en ASP.NET Core utilizando **.NET 10.0**, cumpliendo con el requerimiento de la pauta que solicitaba **.NET 6 o superior**.

---

## Prerrequisitos

Antes de ejecutar el proyecto asegúrese de tener instalado:

- **.NET SDK 10.0 o superior**  
  (La pauta indicaba .NET 6 o superior; esta implementación utiliza **.NET 10.0**)  
  https://dotnet.microsoft.com/download  

- Visual Studio Code o Visual Studio (opcional)  
- Git (opcional)

Para verificar la versión instalada ejecutar:

    dotnet --version

---

## Arquitectura

La solución implementa separación de responsabilidades por capas, permitiendo mantener el código organizado y fácil de testear:

- Controller: Manejo de endpoints HTTP  
- Service: Lógica de negocio  
- Domain: Modelo de datos  
- DTO: Validaciones de entrada  
- Middleware: Manejo global de excepciones  
- Exceptions: Manejo de errores de negocio (Conflict y NotFound)  
- Interfaces: Código desacoplado y reutilizable  

Esta estructura facilita el mantenimiento del código y mejora la capacidad de realizar pruebas automatizadas.

---

## Tecnologías utilizadas

- ASP.NET Core Web API  
- .NET 10.0  
- Swagger  
- xUnit  
- FluentAssertions  
- C#  
- Dependency Injection  
- Middleware personalizado  
- Logging estructurado  

---

## Ejecución del proyecto

Ubicarse en la raíz del proyecto y ejecutar:

    dotnet restore
    dotnet build
    dotnet run --project Backend/PruebaTecnicaCarsales.BFF

Luego abrir en navegador:

    http://localhost:5021/swagger

Esto abrirá Swagger y permitirá probar los endpoints disponibles.

---

## Ejecución de pruebas

Desde la raíz del proyecto ejecutar:

    dotnet test

Las pruebas implementadas validan:

- Creación de contactos  
- Actualización de contactos  
- Prevención de duplicados  
- Funcionamiento correcto de endpoints  

---

## Endpoints disponibles

Crear contacto:

POST /api/Contacts  

Ejemplo:

    {
      "nombre": "Simon",
      "telefono": "987654321"
    }

Obtener contacto por Id:

GET /api/Contacts/{id}

Obtener lista de contactos:

GET /api/Contacts

Actualizar contacto:

PUT /api/Contacts/{id}

Eliminar contacto:

DELETE /api/Contacts/{id}

---

## Funcionalidades implementadas

- Id autoincrementable  
- Validación de teléfono (9 dígitos)  
- Prevención de duplicados por teléfono  
- Manejo global de errores  
- Uso de interfaces  
- Logging estructurado  
- Arquitectura desacoplada  
- Ejecución thread-safe  
- Tests unitarios  
- Tests de integración  

---

## Manejo global de errores

Se implementa un middleware llamado **ErrorMiddleware**, el cual captura excepciones y retorna códigos HTTP adecuados:

- 409 → Conflictos (duplicados)  
- 404 → Recurso no encontrado  
- 500 → Error interno  

Ejemplo de respuesta:

    {
      "message": "Ya existe el contacto en la lista con el mismo teléfono"
    }

Este enfoque permite centralizar el manejo de errores y mantener los controladores limpios.

---

## Concurrencia

La solución es thread-safe y evita problemas cuando múltiples solicitudes intentan modificar la lista de contactos simultáneamente utilizando:

    lock(lockObject)

Esto asegura modificaciones seguras en memoria.

---

## Swagger

Swagger está habilitado en:

    http://localhost:5021/swagger

Permite probar todos los endpoints de forma visual.

---

## Autor

Simón Pereira Vigouroux