# Fuel Manager API

API REST para cadastro de veículos e controle de despesas com combustíveis. O projeto foi desenvolvido em **ASP.NET Core 9**, com **Entity Framework Core**, **SQL Server** e documentação interativa pelo **Swagger**.

## Funcionalidades

- Cadastro, consulta, atualização e exclusão de veículos;
- Registro dos consumos associados a cada veículo;
- Classificação por tipo de combustível: diesel, etanol ou gasolina;
- Persistência dos dados no SQL Server;
- Documentação e testes dos endpoints pelo Swagger.

## Tecnologias

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9
- SQL Server
- Swagger / OpenAPI

## Como executar

### Pré-requisitos

- SDK do .NET 9;
- SQL Server disponível localmente;
- ferramenta `dotnet-ef` para aplicar as migrações.

### Configuração

1. Clone o repositório:

```bash
git clone https://github.com/AntonioRubens77/mf-apis-web-services-fuel-manager.git
cd mf-apis-web-services-fuel-manager
```

2. Confira a conexão `DefaultConnection` no arquivo `appsettings.json`.

3. Restaure os pacotes e aplique as migrações:

```bash
dotnet restore
dotnet ef database update
```

4. Execute a API:

```bash
dotnet run
```

5. Abra no navegador o endereço exibido no terminal. A rota raiz redireciona para o Swagger.

## Endpoints de veículos

| Método | Rota | Finalidade |
|---|---|---|
| `GET` | `/api/veiculos` | Listar veículos e seus consumos |
| `GET` | `/api/veiculos/{id}` | Consultar um veículo |
| `POST` | `/api/veiculos` | Cadastrar um veículo |
| `PUT` | `/api/veiculos/{id}` | Atualizar um veículo |
| `DELETE` | `/api/veiculos/{id}` | Excluir um veículo |

## Estrutura principal

- `Controllers/`: endpoints da API;
- `Models/`: entidades e contexto do banco;
- `Migrations/`: migrações do Entity Framework Core;
- `Program.cs`: configuração e inicialização da aplicação.
