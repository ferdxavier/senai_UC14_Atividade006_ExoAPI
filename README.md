# Exo.WebApi

API desenvolvida em ASP.NET Core para fins educacionais, demonstrando a construção de uma Web API com Entity Framework Core, autenticação JWT e documentação via Swagger.

## Visão Geral

Este projeto implementa uma API RESTful utilizando o padrão de repositório e injeção de dependência nativa do ASP.NET Core. A aplicação expõe endpoints para gerenciamento de projetos, utilizando SQL Server como banco de dados e Entity Framework Core 7 para acesso a dados.

## Pré-requisitos

- [.NET SDK 7.x](https://dotnet.microsoft.com/download/dotnet/7.0)
- SQL Server (LocalDB, Express ou instância completa)
- IDE recomendada: Visual Studio 2022, VS Code com extensão C# Dev Kit, ou JetBrains Rider

## Estrutura do Projeto

```
Exo.WebApi/
├── Controllers/        # Controladores da API (endpoints HTTP)
├── Contexts/           # ExoContext (DbContext do Entity Framework)
├── Models/             # Entidades de domínio
├── Repositories/       # Camada de acesso a dados (ProjetoRepository)
├── Program.cs          # Ponto de entrada e configuração de serviços
├── Exo.WebApi.csproj   # Arquivo de projeto (target framework net7.0)
├── appsettings.json           # Configurações gerais
└── appsettings.Development.json # Configurações de desenvolvimento
```

### Componentes Principais

- **Program.cs**: Configura o container de injeção de dependência, registra `ExoContext` e `ProjetoRepository`, mapeia controladores e inicia a aplicação.
- **Exo.WebApi.csproj**: Define o target framework `net7.0` e referências de pacotes (Swashbuckle, EF Core 7, SQL Server, JWT Bearer).
- **Contexts/ExoContext**: Contexto do Entity Framework Core para comunicação com o banco de dados.
- **Repositories/ProjetoRepository**: Implementa operações de acesso a dados para a entidade Projeto.
- **Controllers/**: Controladores que expõem os endpoints da API REST.

## Como Executar

### 1. Restaurar dependências

```bash
dotnet restore
```

### 2. Compilar o projeto

```bash
dotnet build
```

### 3. Executar a aplicação

```bash
dotnet run
```

A API estará disponível em `https://localhost:7xxx` (porta definida no `launchSettings.json` ou atribuída dinamicamente). A documentação Swagger UI pode ser acessada em `/swagger` quando em ambiente de desenvolvimento.

## Configuração

As configurações de conexão e autenticação estão em `appsettings.json` e `appsettings.Development.json`. Ajuste a `ConnectionStrings:DefaultConnection` para apontar para sua instância do SQL Server.

## Tecnologias Utilizadas

- ASP.NET Core 7 (Minimal APIs / Controllers)
- Entity Framework Core 7
- Microsoft SQL Server Provider
- Swashbuckle.AspNetCore (Swagger/OpenAPI)
- JWT Bearer Authentication
- Microsoft.VisualStudio.Web.CodeGeneration.Design

---

*Projeto criado para fins de estudo no curso Full-stack SENAI - UC14 Desenvolvimento de APIs.*
