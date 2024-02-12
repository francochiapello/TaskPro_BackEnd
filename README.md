## TaskPro con asp.net core

# Requisitos previos

1. mongoDb version 7.0.5
2. sql
3. SDK de .NET Core 3.1 o superior

# Configuracion del proyecto

1. Clona el repositorio
   > git clone https://github.com/francochiapello/TaskPro_BackEnd.git

# Estructura del Proyecto

El proyecto est� organizado en las siguientes carpetas:

- **`controllers`:** Contiene los endpoint de la app.
- **`data`:** Contiene la configuraci�n y las clases de contexto de Entity Framework Core para la base de datos.
- **`helpers`:** Aqu� se encuentran componentes React reutilizables que no est�n vinculados a p�ginas espec�ficas.
- **`models`:** Aqu� se encuentran las clases que representan los modelos de datos seccionados en .MAP y .DTO.
- **`persistence`:** Almacena archivos .DAO, la cual proporciona una capa de abstracci�n entre el c�digo de la aplicaci�n y la base de datos subyacente.
- **`security`:** Contiene archivos relacionados con la configuraci�n de seguridad.
- **`services`:** Almacena la l�gica de negocio o la l�gica de aplicaci�n que no est� directamente relacionada con la manipulaci�n de datos.