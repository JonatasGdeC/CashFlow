# CashFlow

Aplicação Full Stack para gerenciamento de finanças pessoais, desenvolvida com **ASP.NET Core** e **Blazor WebAssembly**.

O sistema permite acompanhar receitas, despesas, categorias, metas financeiras e indicadores consolidados através de dashboards e relatórios.

## Demonstração

**Aplicação:** [https://cash-flow-jgc.vercel.app/](https://cash-flow-jgc.vercel.app/)

> A API está hospedada no plano gratuito da Render. A primeira requisição pode levar alguns segundos devido ao cold start.

---

## Funcionalidades

### Autenticação

* Cadastro de usuários
* Login com JWT
* Alteração de perfil
* Alteração de senha

### Gestão Financeira

* Cadastro de receitas
* Cadastro de despesas
* Categorias personalizadas
* Metas financeiras por categoria
* Dashboard financeiro
* Filtros por período
* Consolidação anual de dados

### Relatórios

* Exportação para Excel
* Exportação para PDF

---

## Arquitetura

O projeto segue os princípios de:

* Clean Architecture
* SOLID
* Domain Driven Design (DDD)
* Repository Pattern
* Unit of Work

Estrutura atual:

```text
src
├── Backend
│   ├── CashFlow.Api
│   ├── CashFlow.Application
│   ├── CashFlow.Domain
│   └── CashFlow.Infrastructure
│
├── Shared
│   ├── CashFlow.Communication
│   └── CashFlow.Exception
│
└── Web
    ├── CashFlow.Adapter
    └── CashFlow.App

tests
└── Backend
    ├── CommomTestsUtilies
    ├── UseCases.Tests
    ├── Validators.Tests
    └── WebApi.Tests
```

---

## Tecnologias Utilizadas

### Backend

* C#
* .NET 10
* ASP.NET Core
* Entity Framework Core
* JWT Authentication
* MySQL
* Swagger
* xUnit

### Frontend

* Blazor WebAssembly
* HTML
* CSS
* JavaScript

### Infraestrutura

* GitHub Actions
* Docker
* Vercel
* Render
* TiDB Cloud

---

## Executando Localmente

### Clonar o repositório

```bash
git clone https://github.com/JonatasGdeC/CashFlow.git
```

```bash
cd CashFlow
```

### Restaurar dependências

```bash
dotnet restore CashFlow.sln
```

### Executar API

```bash
dotnet run --project src/Backend/CashFlow.Api/CashFlow.Api.csproj
```

### Executar Frontend

```bash
dotnet run --project src/Web/CashFlow.App/CashFlow.App.csproj
```

---

## Testes

Executar todos os testes:

```bash
dotnet test CashFlow.sln
```

Executar projetos específicos:

```bash
dotnet test tests/Backend/UseCases.Tests/UseCases.Tests.csproj

dotnet test tests/Backend/Validators.Tests/Validators.Tests.csproj

dotnet test tests/Backend/WebApi.Tests/WebApi.Tests.csproj
```

---


## Screenshots

Adicione aqui imagens do Dashboard, Receitas, Despesas, Categorias e Relatórios.

---

## Autor

**Jônatas Gonçalves de Carvalho**

* GitHub: [JonatasGdeC](https://github.com/JonatasGdeC)
* LinkedIn: [Jônatas Carvalho](https://www.linkedin.com/in/jonatasgdec)
* Portfólio: [Portfólio Pessoal](https://jonatasgdec-portfolio.vercel.app/)