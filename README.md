# Sincronía - Backend API Server

## 1. Descripción General del Sistema

Sincronía es una plataforma orientada a la optimización de la
productividad y la gestión del tiempo.\
El núcleo del sistema (Backend) opera como una API RESTful desarrollada
en **.NET 8 (C#)**.

Su función principal es actuar como un orquestador lógico que procesa
las preferencias de los usuarios (restricciones dietéticas, tiempos
máximos de preparación y carga de tareas) y delega el razonamiento
complejo a un modelo de Inteligencia Artificial externo.

El resultado es la generación de un "Plan Maestro de Trabajo" que
intercala eficientemente periodos de cocción con bloques de enfoque
profundo (Deep Work), garantizando la seguridad del contexto operativo.

------------------------------------------------------------------------

## 2. Arquitectura del Proyecto

El sistema está diseñado bajo el patrón **Controlador-Servicio (N-Tier
Architecture)**, priorizando el principio de responsabilidad única (SRP)
y facilitando el bajo acoplamiento.

### Componentes:

-   **Capa de Presentación (Controllers):**\
    Expone los endpoints HTTP. Valida datos mediante Data Annotations y
    retorna respuestas HTTP estándar.

-   **Capa de Lógica de Negocio (Services):**\
    Contiene reglas de negocio e integración con la API de Inteligencia
    Artificial.

-   **Inyección de Dependencias (DI):**\
    Uso del contenedor IoC nativo de .NET. Servicios como
    `IPlannerService` y `HttpClient` son inyectados vía constructor.

-   **Flujo Interno:**\
    Frontend → Middleware (CORS) → PlannerController → Validación DTO →
    PlannerService → Groq API → Respuesta JSON.

------------------------------------------------------------------------

## 3. Estructura de Carpetas

    /Sincronia.Api
    │
    ├── /Controllers
    │   └── PlannerController.cs
    │
    ├── /Services
    │   ├── IPlannerService.cs
    │   └── PlannerService.cs
    │
    ├── /Models
    │   ├── PlannerRequestDto.cs
    │   └── PlannerResponseDto.cs
    │
    ├── appsettings.json
    ├── launchSettings.json
    └── Program.cs

------------------------------------------------------------------------

## 4. Configuración y Ejecución

### Requisitos Previos

-   SDK de .NET 8.0 o superior
-   Clave válida para la API de Groq

### Configuración (appsettings.json)

``` json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Groq": {
    "ApiKey": "INSERTE_SU_API_KEY_AQUI",
    "Model": "llama-3.1-8b-instant"
  }
}
```

### Puerto de Ejecución

Definido en `Properties/launchSettings.json`.

Por defecto:

    http://localhost:5222

Para modificarlo, editar `applicationUrl`.

### Ejecutar desde CLI

``` bash
dotnet run
```

------------------------------------------------------------------------

## 5. Referencia de la API

### Endpoint: Generar Plan de Sincronía

-   **Ruta:** `/api/planner/organize-day`
-   **Método:** POST
-   **Content-Type:** application/json

### Request Body

``` json
{
  "dietPreference": "Balanceada",
  "maxCookingTimeMinutes": 45,
  "tasks": [
    "Revisar correos electrónicos",
    "Redactar informe financiero",
    "Reunión de avance de proyecto"
  ]
}
```

### Response Body

``` json
{
  "success": true,
  "data": {
    "routine": [
      {
        "time": "08:00",
        "activity": "Cocina: Preparación de Desayuno Balanceado",
        "category": "Food"
      },
      {
        "time": "08:45",
        "activity": "Bloque Deep Work: Redactar informe financiero",
        "category": "Work"
      }
    ],
    "summary": "Plan optimizado. El tiempo de cocción inactiva se aprovechará para revisión de correos."
  },
  "message": "Plan generado exitosamente."
}
```

### Códigos de Estado

-   200 OK → Plan generado correctamente
-   400 Bad Request → Error de validación
-   500 Internal Server Error → Fallo en IA o excepción no controlada

------------------------------------------------------------------------

## 6. Integración Externa y Motor de IA

El backend utiliza la API de Groq con el modelo `llama-3.1-8b-instant`.

### Justificación Técnica

Groq emplea arquitectura LPU (Language Processing Units), ofreciendo
menor latencia de inferencia comparado con GPU tradicionales.

Esto permite respuesta síncrona en tiempo real.

### Prompt Engineering

Se utilizan System Prompts restrictivos para:

-   Forzar formato estructurado
-   Mantener consistencia JSON
-   Aplicar reglas de seguridad (evitar riesgos físicos durante cocción)

------------------------------------------------------------------------

## 7. Seguridad y Buenas Prácticas

### Gestión de Credenciales

-   No subir claves reales al repositorio
-   Uso de `.gitignore`

### Variables de Entorno

Recomendado:

    dotnet user-secrets

O variables de entorno:

    GROQ__APIKEY

### Políticas CORS

Configuradas en `Program.cs` permitiendo únicamente orígenes
autorizados.

------------------------------------------------------------------------

## 8. Escalabilidad y Roadmap Técnico

-   Autenticación con OAuth 2.0 / JWT (ASP.NET Core Identity)
-   Persistencia con Entity Framework Core + PostgreSQL o SQL Server
-   Logging estructurado con Serilog + OpenTelemetry
-   Contenerización con Docker y despliegue en Kubernetes o Azure App
    Services

------------------------------------------------------------------------

## 9. Trabajo Colaborativo y Gestión del Proyecto

El desarrollo de Sincronía fue realizado bajo un enfoque colaborativo por un equipo de tres integrantes, aplicando principios de organización ágil y gestión visual del flujo de trabajo.

### Equipo de Desarrollo
- Steven Maldonado
- Maria Perez
- Luis Muñoz

### Metodología de Trabajo
Se utilizó Trello como herramienta de gestión de tareas para:

- Planificación de sprint académico
- Asignación de responsabilidades
- Seguimiento de avances
- Control de entregables
- Priorización de funcionalidades (MVP → mejoras)

### Tablero Oficial del Proyecto
https://trello.com/b/4Mw4xVMq/sincronia-ia

El tablero documenta:
- Backlog inicial
- División Frontend / Backend
- Integración con IA
- Fases de pruebas
- Preparación de entrega final

Este enfoque permitió mantener trazabilidad, transparencia en responsabilidades y control del alcance del proyecto.

------------------------------------------------------------------------

## 10. Autores 
* `Steven Maldonado`
* `Maria Perez`
* `Luis Muñoz`

------------------------------------------------------------------------

## 11. Licencia

Proyecto académico con fines educativos.
