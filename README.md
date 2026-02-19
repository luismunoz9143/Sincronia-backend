# ♾️ Sincronía | AI Productivity Assistant

Sincronía es una aplicación web impulsada por Inteligencia Artificial diseñada para mentes creativas y profesionales ocupados. El sistema sincroniza la preparación de alimentos con las tareas diarias, optimizando los tiempos muertos (como el tiempo de cocción) para generar bloques de "enfoque profundo" (Deep Work) sin sacrificar la nutrición ni la seguridad.

## 🚀 Arquitectura del Proyecto

El proyecto está dividido en dos capas principales:

* **Frontend (Cliente):** Interfaz de usuario limpia y minimalista, desarrollada con Vanilla JavaScript, HTML5 y CSS3 moderno (CSS Variables, Flexbox/Grid). Utiliza `Chart.js` para la visualización de datos y `Phosphor Icons` para la iconografía.
* **Backend (Servidor):** API RESTful desarrollada en **C# .NET**. Se encarga de la lógica de negocio y la orquestación de servicios externos.
* **Motor de IA:** Integración con la API de **Groq** (modelo `llama-3.1-8b-instant`) para el procesamiento de lenguaje natural y la generación de rutinas de productividad estructuradas y seguras.

## ✨ Características Principales

1.  **Nutrición Adaptativa:** Sugerencias basadas en preferencias alimenticias (Balanceada, Keto, Vegetariana) y el tiempo disponible.
2.  **Motor de Inteligencia:** Generación de un "Plan Maestro de Trabajo" que analiza las tareas pendientes y las distribuye lógicamente a lo largo del día.
3.  **Seguridad de Contexto:** Reglas estrictas de Prompt Engineering que evitan sugerencias peligrosas (ej. salir de casa mientras la estufa está encendida).
4.  **Dashboard Interactivo:** Visualización en tiempo real de la distribución del tiempo (Cocina vs. Enfoque) mediante gráficos.

## 🛠️ Requisitos Previos

Para ejecutar este proyecto de manera local, necesitas:
* [.NET SDK 8.0 o superior](https://dotnet.microsoft.com/download)
* Un navegador web moderno.
* Una API Key de **Groq** (Gratuita).

## ⚙️ Instalación y Configuración

### 1. Configuración del Backend (.NET)
Navega a la carpeta del backend y configura tu clave de IA:

1. Abre el archivo `appsettings.json`.
2. Agrega tu clave de Groq en la sección correspondiente:
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "Groq": {
       "ApiKey": "TU_API_KEY_AQUI"
     }
   }
Ejecuta el proyecto. La API se levantará por defecto en el puerto 5222 (o el configurado en tu launchSettings.json).

Bash
dotnet run
2. Configuración del Frontend
Asegúrate de que la API de .NET esté corriendo.

Abre el archivo Sincronia.Front/script.js y verifica que la constante API_URL apunte al puerto correcto de tu entorno local (ej. http://localhost:5222/api/planner/organize-day).

Abre el archivo index.html en tu navegador (puedes usar la extensión Live Server de VS Code para una mejor experiencia).

💻 Uso de la Aplicación
Inicia sesión para acceder al Dashboard.

En el panel izquierdo, selecciona tu preferencia de dieta y ajusta el tiempo máximo que deseas pasar cocinando usando el slider.

Escribe tus tareas del día, una por línea.

Haz clic en Sincronizar D'IA. El sistema consultará al modelo de lenguaje y estructurará tu rutina diaria en el panel derecho.

👨‍💻 Autores
[Tu Nombre Completo] - Desarrollo Full-Stack y Prompt Engineering - Proyecto Final.