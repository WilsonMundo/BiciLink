# 🚲 BiciLink  
Sistema de reserva de bicicletas – Proyecto para Análisis de Sistemas I

---

## 🔗 Despliegue en línea  
- Aplicación: [https://bicilink.mundoalonzo.com/main](https://bicilink.mundoalonzo.com/main)  
- Documentación Swagger: [https://bicilink.mundoalonzo.com/swagger](https://bicilink.mundoalonzo.com/swagger)

---

## 📚 Descripción  
**BiciLink** es una plataforma para la reserva de bicicletas, desarrollada como un proyecto universitario.  
Está construido con **Blazor Server** que hospeda una aplicación **Blazor WebAssembly** e incluye una **API REST** documentada con Swagger.

Permite a los usuarios registrarse, iniciar sesión, explorar bicicletas disponibles y reservar horarios de uso.

---

## 🧩 Estructura del repositorio  
- `BiciReserva.Client/` – Aplicación Blazor WebAssembly (UI del usuario)  
- `BiciReserva.Server/` – Proyecto Blazor Server (host + API)  
- `BusinessLogic/` – Reglas de negocio y lógica central  
- `DataAccess/` – Acceso a base de datos  
- `Domain/` – Modelos y entidades  
- `Shared/` – Clases compartidas entre cliente y servidor  
- `BiciReserva.sln` – Solución general del proyecto

---

## ⚙️ Requisitos e instalación local

### Prerrequisitos  
- [.NET SDK 8+](https://dotnet.microsoft.com/)  
- SQL Server o LocalDB  

### Clonado y configuración  
```bash
git clone https://github.com/WilsonMundo/BiciLink.git
cd BiciLink
```
### 🔐 Variables de entorno requeridas

Antes de ejecutar la aplicación, asegúrate de definir las siguientes variables de entorno:

| Variable        | Descripción                                  | Ejemplo                                                                 |
|----------------|----------------------------------------------|-------------------------------------------------------------------------|
| `SQLConnection` | Cadena de conexión a la base de datos        | `Server=(localdb)\\mssqllocaldb;Database=BiciLinkDb;Trusted_Connection=True;` |
| `llavejwt`      | Clave secreta para generación de JWT         | `SuperClaveSecreta1234567890`                                           |
