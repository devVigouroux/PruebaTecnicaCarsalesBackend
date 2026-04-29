# API de Contactos — Prueba Técnica Carsales

Esta prueba consiste en una **API REST en ASP.NET Core (.NET 10)** que permite agregar, buscar, eliminar y actualizar contactos en memoria, aplicando arquitectura desacoplada, validaciones, manejo global de errores y pruebas automatizadas.

---

# Arquitectura

La solución implementa **separación de responsabilidades** por capas:

- Controller → Manejo de endpoints HTTP  
- Service → Lógica de negocio  
- Domain → Modelo de datos  
- DTO → Validaciones de entrada  
- Middleware → Manejo global de excepciones  
- Interfaces → Código desacoplado y testeable  

Esta arquitectura permite mantener el código organizado, reutilizable y fácil de testear.

---

# Tecnologías utilizadas

- ASP.NET Core (.NET 10)
- Swagger
- xUnit
- FluentAssertions
- Angular (Frontend)
- C#
- Dependency Injection
- Middleware personalizado

---

# Ejecutar Backend

Abrir terminal y ejecutar:

cd Backend/PruebaTecnicaCarsales.BFF  
dotnet run  

Luego abrir en navegador:

http://localhost:5021/swagger  

Esto abrirá Swagger para probar la API.

---

# Ejecutar Tests

Desde la raíz del proyecto ejecutar:

dotnet test PruebaTecnicaCarsales.Test/PruebaTecnicaCarsales.Test.csproj  

Tests implementados:

- Test unitario (crear contacto correctamente)  
- Test unitario (validar largo del teléfono)  
- Test de integración (POST contacto)  

---

# Endpoints Disponibles

Crear contacto:

POST /api/Contacts  

Ejemplo:

{
  "nombre": "Simon",
  "telefono": "987654321"
}

---

Obtener contacto por Id:

GET /api/Contacts/{id}

---

Obtener lista de contactos:

GET /api/Contacts

---

Eliminar contacto:

DELETE /api/Contacts/{id}

---

# Funcionalidades implementadas

- Id autoincrementable  
- Validación de teléfono (9 dígitos)  
- Evita duplicados por nombre y teléfono  
- Manejo global de errores  
- Thread-safe  
- Uso de interfaces  
- Logging estructurado  
- Tests unitarios  
- Test de integración  

---

# Manejo Global de Errores

Se implementa un middleware personalizado llamado:

ErrorMiddleware  

Este middleware captura:

- InvalidOperationException → 400 (Bad Request)  
- Exception → 500 (Internal Server Error)  

Ejemplo de respuesta:

{
  "message": "Ya existe el contacto en la lista con el mismo nombre y teléfono"
}

Esto permite centralizar el manejo de errores y evitar duplicar código en los controladores.

---

# Concurrencia

La solución es **thread-safe**, evitando problemas cuando múltiples solicitudes intentan agregar contactos al mismo tiempo.

Se utiliza:

lock(lockObject)

Esto asegura que la lista de contactos se modifique de forma segura.

---

# Swagger

Swagger está habilitado y disponible en:

http://localhost:5021/swagger  

Permite probar todos los endpoints de la API.

---

# Funcionalidades adicionales implementadas

- Middleware personalizado  
- Logging estructurado  
- Arquitectura desacoplada  
- Uso de interfaces  
- Thread-safe  
- Tests unitarios  
- Test de integración  

---

# Autor

Simón Pereira Vigouroux