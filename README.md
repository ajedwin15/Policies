# Polices.Api

Polices.Api es un proyecto de aplicación .NET 6.0 construido con C# y siguiendo una arquitectura limpia. Esta API RESTful está diseñada para manejar todas las operaciones relacionadas con las pólizas de seguros.

## Requisitos

- .NET 6.0 SDK
- MongoDB

## Tecnologías usadas

- .NET 6
- C#
- MongoDB
- JWT

## Arquitectura

Esta aplicación utiliza la arquitectura limpia para separar la lógica de la aplicación de la infraestructura.

## Configuración de la aplicación

Para configurar la aplicación, necesitas definir algunas variables de entorno para la autenticación por token:

- `TOKEN_KEY`: Esta es la clave secreta utilizada para firmar el token. Debe ser una cadena larga y segura.

- `TOKEN_ISSUER`: Este es el emisor del token. Por lo general, es la URL de tu aplicación.

- `TOKEN_AUDIENCE`: Esta es la audiencia del token. Por lo general, es la URL de tu aplicación.

- `JWT_EXPIRES`: Este es el tiempo en minutos para que expire el token.

## Cómo iniciar el proyecto con visual estudio code

1. Clona este repositorio a tu máquina local con `git clone https://github.com/[tu_usuario]/Polices.Api.git`.

2. Navega hasta la carpeta del proyecto con `cd InsurancePolicies.API`.

3. Restaura las dependencias del proyecto con `dotnet restore`.

4. Compila el proyecto con `dotnet build`.

5. Inicia el proyecto con `dotnet run`.

## Cómo iniciar el proyecto con visual estudio

1. Clona este repositorio a tu máquina local con `git clone https://github.com/[tu_usuario]/Polices.Api.git`.

2. Navega hasta la carpeta del proyecto e inicia la solucion  `InsurancePolicies.sln`.

3. Restaura las dependencias del proyecto con `dotnet restore`.

4. Compila el proyecto 

5. Estabelce como proyecto de unicio la capa `InsurancePolicies.sln`.


## Configuración de la base de datos

configura tu appsettings crealo en el mimo lugar donde esta el archivo `appsettings.Development.json`

Para configurar MongoDB, actualiza el archivo `appsettings.json` con tu cadena de conexión a la base de datos. 

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",  
  "MONGO_STRING_CONNECTION": "mongodb://localhost:27017",
  "MONGO_DB_NAME_USER": "PolicesDB",
  "JWT_KEY": "",
  "JWT_ISSUER": "",
  "JWT_AUDIENCE": "",
  "JWT_EXPIRES": "1"
}
