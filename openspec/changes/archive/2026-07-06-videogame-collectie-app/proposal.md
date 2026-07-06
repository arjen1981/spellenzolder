## Why

Er is geen centrale plek om mijn videogame-collectie bij te houden. Ik wil snel kunnen zien welke games ik bezit, op welk platform, en in welke staat ze verkeren — zowel vanaf mijn telefoon als desktop. Een dedicated app met een retro 16-bit arcade look maakt het bijhouden leuk en visueel aantrekkelijk.

## What Changes

- Nieuwe responsive webapplicatie voor het registreren en beheren van een videogame-collectie
- Formulier om games toe te voegen met: titel, staat (5-sterren per onderdeel: boekje, doos, cart/cd), platform, registratiedatum
- Overzichtslijst van de collectie met filter- en sorteermogelijkheden
- 16-bit arcade-stijl visueel thema (pixelfonts, retro kleuren, arcadeachtige UI-elementen)
- Responsive layout geoptimaliseerd voor zowel mobiel als desktop

## Capabilities

### New Capabilities
- `game-registration`: CRUD-functionaliteit voor het registreren van games met titel, staat (5-sterren per onderdeel), platform en registratiedatum
- `collection-overview`: Overzichtslijst van de volledige collectie met filtering en sortering
- `retro-arcade-theme`: 16-bit arcade visueel thema met pixelfonts, retro kleurenpalet en arcadeachtige UI-componenten
- `responsive-layout`: Responsive design dat optimaal werkt op mobiel en desktop

### Modified Capabilities

_(geen — dit is een nieuw project)_

## Impact

- Nieuw Next.js frontend project met App Router en Server Components
- Nieuwe ASP.NET Core Web API voor game-registratie (bounded context: `GameCollection`)
- Nieuwe SQL Server database met tabel voor games
- Integratie in bestaande .NET Aspire AppHost voor orchestratie
- Dependencies: shadcn/ui, Tailwind CSS v4, lucide-react, pixelfont packages
