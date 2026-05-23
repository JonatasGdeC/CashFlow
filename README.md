# CashFlow

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-ORM-6DB33F?style=for-the-badge)
![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Tests-5E2B97?style=for-the-badge)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

CashFlow e uma API REST para controle de despesas pessoais. O projeto permite cadastrar usuarios, autenticar com token JWT, registrar despesas, consultar despesas cadastradas, atualizar ou remover registros e gerar relatorios em PDF e Excel.

## Funcionalidades

- Cadastro, consulta, atualizacao e exclusao de usuario.
- Login com geracao de token JWT.
- Cadastro, listagem, consulta por id, atualizacao e exclusao de despesas.
- Geracao de relatorios de despesas em PDF e Excel.
- Validacoes de entrada com FluentValidation.
- Persistencia em banco MySQL usando Entity Framework Core.
- Documentacao interativa via Swagger em ambiente de desenvolvimento.
- Testes automatizados para casos de uso, validadores e endpoints da API.



## Estrutura do projeto

```text
CashFlow/
|-- src/
|   |-- CashFlow.Api             # Controllers, filtros, middleware e configuracao da API
|   |-- CashFlow.Application     # Casos de uso, validacoes, mapeamentos e relatorios
|   |-- CashFlow.Communication   # DTOs de request e response
|   |-- CashFlow.Domain          # Entidades, enums, contratos e regras de dominio
|   |-- CashFlow.Exception       # Excecoes e mensagens de erro
|   `-- CashFlow.Infrastructure  # Banco de dados, repositorios, migrations, token e criptografia
|-- tests/
|   |-- CommomTestsUtilies
|   |-- UseCases.Tests
|   |-- Validators.Tests
|   `-- WebApi.Tests
`-- CashFlow.sln
```

## Pre-requisitos

Antes de executar o projeto, instale:

- [.NET SDK 10](https://dotnet.microsoft.com/)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Uma IDE ou editor de sua preferencia, como Rider, Visual Studio ou VS Code

## Como executar localmente

Clone o repositorio e acesse a pasta do projeto:

```bash
git clone https://github.com/JonatasGdeC/CashFlow.git
cd CashFlow
```

Restaure as dependencias:

```bash
dotnet restore
```

Configure a conexao com o banco em `src/CashFlow.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "connection": "Server=localhost;Database=CashFlowDb;Uid=root;Pwd=sua_senha;"
  },
  "Settings": {
    "Jwt": {
      "SigningKey": "sua-chave-secreta-com-tamanho-seguro",
      "ExpiresMinutes": 1000
    }
  }
}
```

Tambem e possivel configurar por variaveis de ambiente:

```bash
ConnectionStrings__connection="Server=localhost;Database=CashFlowDb;Uid=root;Pwd=sua_senha;"
Settings__Jwt__SigningKey="sua-chave-secreta-com-tamanho-seguro"
Settings__Jwt__ExpiresMinutes=1000
```

Execute a API:

```bash
dotnet run --project src/CashFlow.Api/CashFlow.Api.csproj
```

Ao iniciar fora do ambiente de testes, a aplicacao executa automaticamente as migrations do Entity Framework Core e cria/atualiza o banco configurado.

Com a API em execucao em ambiente de desenvolvimento, acesse o Swagger:

```text
https://localhost:<porta>/swagger
```

A porta sera exibida no terminal apos o comando `dotnet run`.

## Como usar a API

1. Cadastre um usuario em `POST /api/User`.
2. Faca login em `POST /api/Login`.
3. Copie o token JWT retornado.
4. No Swagger, clique em `Authorize` e informe o token no formato:

```text
Bearer seu-token-jwt
```

Depois disso, os endpoints protegidos de usuario, despesas e relatorios poderao ser acessados.

## Principais endpoints

| Metodo | Rota | Descricao | Autenticacao |
| --- | --- | --- | --- |
| `POST` | `/api/User` | Cadastra um usuario | Nao |
| `POST` | `/api/Login` | Realiza login e retorna token JWT | Nao |
| `GET` | `/api/User` | Consulta o perfil do usuario autenticado | Sim |
| `PUT` | `/api/User` | Atualiza o perfil do usuario autenticado | Sim |
| `PUT` | `/api/User/change-password` | Altera a senha do usuario autenticado | Sim |
| `DELETE` | `/api/User` | Remove a conta do usuario autenticado | Sim |
| `POST` | `/api/Expenses` | Cadastra uma despesa | Sim |
| `GET` | `/api/Expenses` | Lista as despesas | Sim |
| `GET` | `/api/Expenses/{id}` | Busca uma despesa por id | Sim |
| `PUT` | `/api/Expenses/{id}` | Atualiza uma despesa | Sim |
| `DELETE` | `/api/Expenses/{id}` | Remove uma despesa | Sim |
| `GET` | `/api/Report/pdf` | Gera relatorio em PDF | Sim |
| `GET` | `/api/Report/excel` | Gera relatorio em Excel | Sim |

## Como executar os testes

Execute todos os testes da solucao:

```bash
dotnet test
```

Ou execute um projeto especifico:

```bash
dotnet test tests/UseCases.Tests/UseCases.Tests.csproj
dotnet test tests/Validators.Tests/Validators.Tests.csproj
dotnet test tests/WebApi.Tests/WebApi.Tests.csproj
```

Os testes de API usam configuracao de ambiente de teste com banco em memoria.

## Observacoes para avaliacao

- A solucao principal e `CashFlow.sln`.
- O projeto executavel da API e `src/CashFlow.Api/CashFlow.Api.csproj`.
- O banco utilizado em desenvolvimento e MySQL.
- As migrations estao em `src/CashFlow.Infrastructure/DataAccess/Migrations`.
- A documentacao Swagger fica disponivel apenas em ambiente de desenvolvimento.
