## Context

Dit is een greenfield project binnen het bestaande spellenzolder monorepo. De applicatie gebruikt de bestaande .NET Aspire orchestratie en volgt de gevestigde bounded context conventie. Er is momenteel geen videogame-gerelateerde functionaliteit — alles wordt nieuw gebouwd.

De gebruiker wil een persoonlijke collectie-app met een unieke retro 16-bit arcade esthetiek, die zowel op mobiel als desktop goed bruikbaar is.

## Goals / Non-Goals

**Goals:**
- Volledig werkende CRUD voor videogame-registratie (titel, staat per onderdeel in 5 sterren, platform, registratiedatum)
- Responsive UI met 16-bit arcade look (pixelfonts, retro kleuren, scanline-effecten)
- Server-side rendering met Next.js App Router voor snelle initiële load
- RESTful API in ASP.NET Core met proper DDD-lagen (Domain, Application, Infrastructure)
- SQL Server persistentie georchestreerd via .NET Aspire

**Non-Goals:**
- Multiplayer of social features (delen, vergelijken met anderen)
- Barcode scanning of automatische game-detectie
- Prijsinformatie of marktwaarde-tracking
- Gebruikersauthenticatie (single-user app voor nu)
- Import/export functionaliteit

## Decisions

### 1. Bounded Context: `GameCollection`

**Keuze**: Eigen bounded context `GameCollection` met Domain, Application en Infrastructure.Sql lagen.

**Rationale**: Volgt de projectconventie voor gescheiden contexten. Videogame-collectie is een duidelijk afgebakend domein met eigen ubiquitous language (game, platform, condition rating).

**Alternatieven overwogen**:
- Alles in een shared module → schendt bounded context principe, moeilijker te onderhouden

### 2. Condition Rating als Value Object met drie dimensies

**Keuze**: `ConditionRating` value object met drie losse 1-5 sterren ratings: `ManualRating`, `BoxRating`, `MediaRating` (cart/cd).

**Rationale**: Elk onderdeel (boekje, doos, cart/cd) heeft een onafhankelijke staat. Een enkel getal zou nuance verliezen. Value object garandeert invarianten (1-5 range).

**Alternatieven overwogen**:
- Eén gemiddelde score → verliest detail over individuele onderdelen
- Percentage-schaal → minder intuïtief dan sterren

### 3. Platform als enum-achtig Value Object

**Keuze**: `Platform` als value object met voorgedefinieerde waarden (NES, SNES, N64, GameBoy, PlayStation, PS2, Sega Genesis, etc.) plus mogelijkheid voor custom platforms.

**Rationale**: Voorkomt typo's en inconsistentie, maar staat uitbreiding toe. Geen harde enum zodat de gebruiker niet gelimiteerd is.

### 4. Frontend: Next.js met retro theme via Tailwind CSS v4

**Keuze**: Next.js App Router met een custom retro-arcade Tailwind theme. Pixelfont via Google Fonts (`Press Start 2P`), retro kleurenpalet (donkere achtergrond, neonkleuren), subtiele scanline overlay.

**Rationale**: shadcn/ui biedt solide basis-componenten die via Tailwind volledig gestyled kunnen worden. Pixelfonts en retro kleuren geven de arcade-feel zonder complexe custom CSS. Server Components voor de collectielijst zorgen voor snelle loads.

**Alternatieven overwogen**:
- Volledig custom CSS met pixel-art sprites → te veel werk, moeilijk onderhoudbaar
- Canvas-gebaseerde UI → niet toegankelijk, niet responsive

### 5. Star Rating Component

**Keuze**: Custom client component met interactieve 5-sterren input per onderdeel. Drie rijen (Boekje, Doos, Media) elk met klikbare sterren.

**Rationale**: shadcn/ui heeft geen native star-rating. Een custom component met `'use client'` directive is simpel en herbruikbaar.

### 6. API Design: Minimale REST endpoints

**Keuze**: 
- `POST /api/games` — registreer nieuwe game
- `GET /api/games` — lijst met paging, filtering, sortering
- `GET /api/games/{id}` — detail
- `PUT /api/games/{id}` — update
- `DELETE /api/games/{id}` — verwijder

**Rationale**: Standaard CRUD-operaties. MediatR handlers per operatie conform vertical slice architectuur.

## Risks / Trade-offs

- **Pixelfont leesbaarheid** → Gebruik pixelfont alleen voor headers en accenten; body text in een leesbaar sans-serif font. Zorg voor voldoende font-size op mobiel.
- **Performance bij grote collecties** → Server-side paging vanaf het begin implementeren. DataTable met virtualisatie indien nodig.
- **Retro look vs. UX** → Balans bewaken: retro esthetiek mag usability niet schaden. Standaard UX-patronen (formulieren, knoppen, feedback) blijven intact onder de styling.
- **Pixelfont loading** → Font via `next/font` laden voor optimale performance en geen layout shift.
