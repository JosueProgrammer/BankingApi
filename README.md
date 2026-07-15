# BankingApi

API bancaria desarrollada con ASP.NET Core 10, siguiendo los principios de **Clean Architecture**, patrones de diseño robustos y buenas prácticas de desarrollo de software.

## 🚀 Tecnologías utilizadas

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **Swagger / OpenAPI**
- **xUnit**
- **Moq**
- **Clean Architecture**
- **Middleware global de excepciones**
- **Unit Of Work**
- **Repository Pattern**

## 🏗️ Arquitectura del proyecto

La solución está estructurada en 4 capas principales para garantizar una alta cohesión y bajo acoplamiento:

```text
BankingApi
│
├── Banking.Api
│   ├── Controllers
│   ├── Middleware
│   └── Configuration
│
├── Banking.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Mappings
│
├── Banking.Domain
│   ├── Entities
│   ├── Enums
│   ├── Exceptions
│   └── Interfaces
│
└── Banking.Infrastructure
    ├── Persistence
    ├── Repositories
    └── Generators
```

## ✨ Funcionalidades implementadas

### Clientes
- **Crear clientes**: Registro de un nuevo cliente en el sistema.
- **Consultar clientes**: Obtención de los datos de un cliente.

### Cuentas bancarias
- **Crear cuenta asociada a un cliente**: Apertura de una nueva cuenta bancaria vinculada a un cliente.
- **Generación automática del número de cuenta**:
  - El número de cuenta es generado dinámicamente.
  - Sigue el formato estricto: `ACC-YYYYMMDD-XXXX`.
  - Cuenta con validación de unicidad básica.

### Consultar saldo
- Obtener el saldo actual en tiempo real proporcionando el número de cuenta.

### Depósitos y retiros
- **Depósitos**: Operación que aumenta el saldo de la cuenta destino.
- **Retiros**: Operación que disminuye el saldo de la cuenta, previa validación de fondos suficientes.
- **Manejo transaccional**: Implementado a través del patrón `UnitOfWork` para garantizar integridad (ej. al retirar sin fondos, se ejecuta un Rollback de la transacción).
- **Idempotencia**: Se requiere un header `Idempotency-Key` para evitar transacciones duplicadas por reintentos accidentales.

### Historial de transacciones
- Consulta cronológica de las operaciones realizadas en una cuenta bancaria.
- Datos incluidos:
  - Id de la transacción.
  - Tipo (Deposit / Withdrawal).
  - Monto de la operación.
  - Fecha.
  - Saldo histórico posterior a la transacción.
- La respuesta incluye **paginación** ejecutada a nivel de base de datos (`page`, `pageSize`, `totalItems`, `totalPages`).

## 🛡️ Manejo de errores

- Se utiliza un **Middleware global de excepciones** que intercepta todos los errores en la capa API.
- Se implementaron **excepciones personalizadas** de dominio (`BusinessException`, `NotFoundException`, `InsufficientFundsException`).
- Las respuestas de error utilizan un formato JSON estándar.
- **No existen try/catch repetitivos** en los controladores; se mantienen limpios y delgados delegando todo al middleware y a la capa de Aplicación.

## 🧩 Principios SOLID aplicados

El proyecto implementa los principios SOLID mediante el uso de Clean Architecture, separación de responsabilidades y dependencia de abstracciones.

### Single Responsibility Principle (SRP)
Cada componente tiene una única responsabilidad:

- Controllers: Manejan únicamente solicitudes HTTP y respuestas.
- Application Services: Contienen la lógica de negocio.
- Repositories: Gestionan únicamente el acceso a datos.
- Domain Entities: Mantienen las reglas propias del dominio.

### Open/Closed Principle (OCP)
El sistema está diseñado para permitir extensiones sin modificar lógica existente:

- Uso de interfaces como `ITransactionRepository`, `IBankAccountRepository` e `IUnitOfWork`.
- Las implementaciones pueden cambiar sin afectar la capa de aplicación.

### Liskov Substitution Principle (LSP)
Las dependencias trabajan mediante contratos definidos por interfaces:

- Cualquier implementación de un repositorio puede sustituirse sin modificar los servicios que lo consumen.

### Interface Segregation Principle (ISP)
Las interfaces están separadas por responsabilidad:

- `ITransactionService`
- `IBankAccountRepository`
- `ITransactionRepository`
- `IUnitOfWork`

Evita interfaces grandes con métodos no relacionados.

### Dependency Inversion Principle (DIP)
La capa Application no depende de implementaciones concretas:

Ejemplo:

```csharp
TransactionService(
    IBankAccountRepository accountRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork)
    


## 🧪 Pruebas unitarias

El proyecto `Banking.Tests` contiene pruebas desarrolladas en xUnit y Moq enfocadas en la lógica central:
- **Generador de números de cuenta**: Validación del formato de salida con expresiones regulares y pruebas de unicidad.
- **Depósitos**: Verificación del incremento del saldo y correcto commit de `UnitOfWork`.
- **Retiros exitosos**: Verificación del descuento de fondos y generación de la entidad transaccional.
- **Retiros sin fondos**: Validación del disparo de la excepción `InsufficientFundsException` y ejecución del Rollback.

```md
## 🔍 Prueba de la API mediante Swagger

Swagger disponible en:

```

[https://localhost](https://localhost):<PUERTO>/swagger/index.html

```

## Flujo de prueba

### 1. Crear cliente

```

POST /api/customers

````

```json
{
  "fullName": "John Doe",
  "birthDate": "1995-05-20",
  "gender": 1,
  "monthlyIncome": 5000
}
````

Guardar el `id` generado.

---

### 2. Crear cuenta bancaria

```
POST /api/bankaccounts
```

```json
{
  "customerId": "cliente-id"
}
```

Guardar el `accountNumber` generado.

---

### 3. Consultar saldo inicial

```
GET /api/bankaccounts/{accountNumber}/balance
```

Resultado esperado:

```json
{
  "balance": 0
}
```

---

### 4. Realizar depósito

```
POST /api/transactions/{accountNumber}/deposit
```

Header:

```
Idempotency-Key: deposit-operation-001
```

Body:

```json
{
  "amount": 2000
}
```

Validar saldo:

```
GET /api/bankaccounts/{accountNumber}/balance
```

Resultado esperado:

```json
{
  "balance": 2000
}
```

---

### 5. Realizar retiro

```
POST /api/transactions/{accountNumber}/withdraw
```

Header:

```
Idempotency-Key: withdraw-operation-001
```

Body:

```json
{
  "amount": 500
}
```

Validar saldo:

```json
{
  "balance": 1500
}
```

---

### 6. Validar retiro sin fondos

```
POST /api/transactions/{accountNumber}/withdraw
```

Body:

```json
{
  "amount": 5000
}
```

Resultado esperado:

* Error por fondos insuficientes.
* El saldo no cambia.

---

### 7. Validar idempotencia

Repetir una operación usando el mismo:

```
Idempotency-Key
```

Resultado esperado:

* No duplica la operación.
* Mantiene el saldo correcto.

---

### 8. Consultar historial

```
GET /api/transactions/{accountNumber}/history?page=1&pageSize=20
```

Validar:

* Id de transacción.
* Tipo de operación.
* Monto.
* Fecha.
* Saldo posterior.
* Paginación.

```
```


## ⚙️ Instalación y ejecución

Sigue estos pasos para ejecutar la API localmente:

1. **Clonar repositorio**
   ```bash
   git clone https://github.com/JosueProgrammer/BankingApi.git
   cd BankingApi
   ```

2. **Restaurar paquetes**
   ```bash
   dotnet restore
   ```

3. **Aplicar migraciones**
   (Se asume que las herramientas de EF Core están instaladas localmente)
   ```bash
   dotnet ef database update --project Banking.Infrastructure --startup-project Banking.Api
   ```

4. **Ejecutar la API**
   ```bash
   dotnet run --project Banking.Api
   ```

5. **Navegar a Swagger**
   Abre tu navegador y dirígete a `https://localhost:<PUERTO>/swagger/index.html` para explorar e interactuar con la API.


## 🧪 Ejecución de pruebas

Para correr las pruebas unitarias y verificar el estado de los componentes clave:

```bash
dotnet test
```

## 🌐 API desplegada

La API se encuentra desplegada en Render:

🔗 Swagger UI:

https://bankingapi-jakd.onrender.com/swagger/index.html

> Nota: El servicio utiliza el plan gratuito de Render, por lo que la primera carga puede tardar unos segundos mientras la instancia se activa después de un periodo de inactividad.  
> Esto corresponde al comportamiento del plan gratuito y no representa un problema de rendimiento de la API.

Una vez iniciada la instancia, los endpoints responden normalmente.
