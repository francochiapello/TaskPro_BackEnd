## TaskPro con asp.net core

# Requisitos previos

1. mongoDb version 7.0.5
2. sql
3. SDK de .NET Core 3.1 o superior

# Configuracion del proyecto

1. Clona el repositorio
   > git clone https://github.com/francochiapello/TaskPro_BackEnd.git

# Estructura del Proyecto

El proyecto está organizado en las siguientes carpetas:

- **`controllers`:** Contiene los endpoint de la app.
- **`data`:** Contiene la configuración y las clases de contexto de Entity Framework Core para la base de datos.
- **`helpers`:** Aquí se encuentran componentes React reutilizables que no están vinculados a páginas específicas.
- **`models`:** Aquí se encuentran las clases que representan los modelos de datos seccionados en .MAP y .DTO.
- **`persistence`:** Almacena archivos .DAO, la cual proporciona una capa de abstracción entre el código de la aplicación y la base de datos subyacente.
- **`security`:** Contiene archivos relacionados con la configuración de seguridad.
- **`services`:** Almacena la lógica de negocio o la lógica de aplicación que no está directamente relacionada con la manipulación de datos.