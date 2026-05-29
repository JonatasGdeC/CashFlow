# CashFlow

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-ORM-6DB33F?style=for-the-badge)
![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Tests-5E2B97?style=for-the-badge)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

CashFlow e uma API REST para controle de despesas pessoais. O projeto permite cadastrar usuários, autenticar com token JWT, registrar despesas/rendas, consultar despesas/rendas cadastradas, atualizar ou remover registros e gerar relatórios em PDF e Excel.

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

## Pré-requisitos

Antes de executar o projeto, instale:

- [.NET SDK 10](https://dotnet.microsoft.com/)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Uma IDE ou editor de sua preferência, como Rider, Visual Studio ou VS Code

## Como executar localmente

Clone o repositório e acesse a pasta do projeto:

```bash
git clone https://github.com/JonatasGdeC/CashFlow.git
cd CashFlow
```

Restaure as dependências:

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

Também e possível configurar por variáveis de ambiente:

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

## Observações para avaliação

- O projeto executável da API é `src/CashFlow.Api/CashFlow.Api.csproj`.
- O banco utilizado em desenvolvimento é MySQL.
- As migrations estão em `src/CashFlow.Infrastructure/DataAccess/Migrations`.
- A documentação Swagger fica disponível apenas em ambiente de desenvolvimento.
- A API está hospedada no plano gratuito da Render.
- A primeira requisição pode demorar alguns segundos devido ao cold start.
