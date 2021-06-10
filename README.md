# Context
Módulo exemplo de integração de loja.

# Functional Overview
# Quality Attributes
## Escalabilidade
## Performance
## Segurança
## Disponibilidade
## Failover
## Extensibilidade
## Flexibilidade
## Auditoria
## Monitoramento
## Interoperabilidade

# Constraints

- Tecnologia
  - .Net Core
  - SQLite
- Sistema Operacional
  - Desktop
    - Windows 7 SP1, ou superior
- Padrões tecnológicos
  - Web API
- Protocolos padrões
  - HTTP
- Padrões de mensagens
  - JSON

# Principles

- Programar orientado a Interfaces
- ORM: EF
- Dependency injection
- Repository Pattern: uma camada para cada banco de dados, atualmente compreende SQLite;
- Alta coesão/Baixo acoplamento
- SOLID
- DRY (don't repeat yourself).
- Stateless
- Não usar *stored procedures* ou *triggers*


# Software Architecture
## Container
### Web API
# Code

## Entity Framework Migrations

Sempre que alterado o modelo de dados (*entities* do EF) é necessário que o *migrations* do SQLite seja incrementado. Pelo *prompt* de comando (como administrador), no diretório `Api`, execute o comando:

```powershell
dotnet ef migrations add [migrations-nome] --context Totvs.Sample.Shop.Infra.SqLite.Context.General.Totvs.Sample.ShopContextSqlite  --project ..\Infra.SqLite
```

> Altere `[migrations-nome]` pela nome desejado para o *migrations*. Exemplo: `Initial`.

## Publicação

Pelo *prompt* de comando (como administrador), no diretório `Api`, execute o comando conforme a seguir:

Para *self-contained* (*runtime* do .Net Core embarcado):

```
dotnet publish -c release --self-contained
```

Para *Azure Web App* ou *Windows Server 2012+*:

```
dotnet publish -c release
```

Em caso de Windows 7 ou Windows Server 2008, utilize o comando junto a opção de runtime para `win7`:

Para *self-contained* (runtime do .Net Core embarcado):

```
dotnet publish -c release --self-contained -r win7-x64
```

Para Azure Web App ou Windows Server 2012+:

```
dotnet publish -c release -r win7-x64
```

# Data
# Infrastructure Architecture
# Deployment
## On-Premise 

# Development Environment

Na preparação do ambiente de desenvolvimento são necessários:

- Visual Studio 2017 (*update* 15.8 ou superior) ou Visual Studio Code
- DB Browser for SQLite
- .Net Core 2.2 SDK

## Docker

1 - Acessar via linha de comando: totvsSampleShopIntegration\src\Totvs.Sample.Shop

2 - Executar: docker build --no-cache -t hub_totvsSampleShop -f Dockerfile .

3 - Executar: docker tag hub_totvsSampleShop:latest docker.totvs.io/hub_totvsSampleShop/hub_totvsSampleShop:1.0.0.0

4 - Executar: docker push docker.totvs.io/hub_totvsSampleShop/hub_totvsSampleShop:1.0.0.0 

# Operation and Support

# Decision Log
