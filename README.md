# Spellenzolder – Retro Game Collection

Een videogame-collectie app gebouwd tijdens een live demo van AI-assisted software development, opgenomen samen met Ivo.

## Wat is dit?

Dit project demonstreert hoe je met AI-tooling (GitHub Copilot) een volledige applicatie kunt bouwen — van ontwerp tot werkende code. De app beheert een persoonlijke retro-gamecollectie met een arcade-geïnspireerde UI.

## Tech Stack

- **Frontend** – Next.js / React met retro arcade thema
- **Backend** – ASP.NET Core Web API
- **Database** – SQL Server via Entity Framework Core
- **Orchestratie** – .NET Aspire
- **Workflow** – OpenSpec voor planning en specificaties

## Projectstructuur

```
src/
  AppHost/          → .NET Aspire orchestrator
  frontend/         → Next.js frontend
  Web/              → ASP.NET Core API
  GameCatalog/      → Domain, Application & Infrastructure
  ServiceDefaults/  → Gedeelde Aspire configuratie
tests/              → Unit tests
openspec/           → Specificaties en planning
```

## Starten

```bash
# Vanuit src/AppHost
dotnet run
```

Dit start de volledige stack via Aspire (API + frontend + database).
