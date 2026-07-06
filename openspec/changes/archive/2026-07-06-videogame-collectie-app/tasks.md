## 1. Project Setup & Infrastructure

- [x] 1.1 Create `GameCollection.Domain` class library project under `src/GameCollection/`
- [x] 1.2 Create `GameCollection.Application` class library project with MediatR dependency
- [x] 1.3 Create `GameCollection.Infrastructure.Sql` class library project with EF Core
- [x] 1.4 Create `GameCollection.Web` ASP.NET Core Web API project with minimal API endpoints
- [x] 1.5 Register GameCollection services in AppHost with SQL Server resource
- [x] 1.6 Add projects to solution file and configure `Directory.Packages.props`

## 2. Domain Layer

- [x] 2.1 Create `ConditionRating` value object (ManualRating, BoxRating, MediaRating; range 0-5)
- [x] 2.2 Create `Platform` value object with predefined platforms and custom support
- [x] 2.3 Create `Game` aggregate root with Title, ConditionRating, Platform, RegistrationDate
- [x] 2.4 Write unit tests for `ConditionRating` (boundary validation, equality)
- [x] 2.5 Write unit tests for `Platform` (predefined list, custom value)
- [x] 2.6 Write unit tests for `Game` aggregate (creation, update, invariants)

## 3. Application Layer

- [x] 3.1 Define `IGameRepository` interface (Add, GetById, GetPaged, Update, Delete)
- [x] 3.2 Create `RegisterGameHandler` with nested Command record
- [x] 3.3 Create `UpdateGameHandler` with nested Command record
- [x] 3.4 Create `DeleteGameHandler` with nested Command record
- [x] 3.5 Create `GetGameByIdHandler` with nested Query record
- [x] 3.6 Create `GetGamesPagedHandler` with nested Query record (filtering, sorting, paging)
- [x] 3.7 Register MediatR handlers via `ServiceCollectionExtensions`
- [x] 3.8 Write unit tests for all handlers (success and failure paths)

## 4. Infrastructure Layer

- [x] 4.1 Create `GameCollectionDbContext` with `Game` entity configuration
- [x] 4.2 Create `GameRepository` implementing `IGameRepository` with EF Core
- [x] 4.3 Add initial EF Core migration for Games table
- [x] 4.4 Register DbContext and repository in `ServiceCollectionExtensions`
- [x] 4.5 Write integration tests with Testcontainers for `GameRepository` CRUD operations

## 5. Web API

- [x] 5.1 Create minimal API endpoints: POST, GET (list), GET (by id), PUT, DELETE for `/api/games`
- [x] 5.2 Add request/response DTOs with validation
- [x] 5.3 Configure OpenAPI metadata on endpoints
- [x] 5.4 Wire up service registration and middleware pipeline
- [x] 5.5 Create `.http` file for manual endpoint testing
- [x] 5.6 Write web integration tests with `WebApplicationFactory`

## 6. Next.js Frontend Setup

- [x] 6.1 Initialize Next.js project with App Router, TypeScript strict mode, Tailwind CSS v4
- [x] 6.2 Install and configure shadcn/ui with custom retro theme
- [x] 6.3 Configure `Press Start 2P` pixel font via `next/font/google`
- [x] 6.4 Define retro arcade color palette in `globals.css` (dark bg, neon cyan/magenta/green/yellow)
- [x] 6.5 Add scanline overlay CSS effect as optional decorative layer
- [x] 6.6 Wire up Next.js frontend in AppHost with API proxy

## 7. Frontend Components & Pages

- [x] 7.1 Create `StarRating` client component (interactive 5-star input with pixel stars and neon glow)
- [x] 7.2 Create `ConditionRatingInput` component (three rows: Manual, Box, Media)
- [x] 7.3 Create `PlatformSelect` component (combobox with predefined platforms + custom input)
- [x] 7.4 Create game registration form page with `react-hook-form` + `zod` validation
- [x] 7.5 Create collection overview page (Server Component) with DataTable
- [x] 7.6 Implement server-side pagination, filtering by platform, sorting, and text search
- [x] 7.7 Create game detail/edit page
- [x] 7.8 Create delete confirmation dialog
- [x] 7.9 Add empty state component for zero-games scenario
- [x] 7.10 Add skeleton loaders for async data fetching

## 8. Responsive Layout & Polish

- [x] 8.1 Implement responsive collection view (table on desktop, cards on mobile)
- [x] 8.2 Add mobile navigation (hamburger menu or bottom nav)
- [x] 8.3 Ensure touch-friendly tap targets (≥ 44px) for star ratings and buttons on mobile
- [x] 8.4 Verify no horizontal scroll on viewports ≥ 320px
- [x] 8.5 Add retro-styled toast notifications (Sonner) for action feedback
- [x] 8.6 Add button hover/active states with neon glow effects
- [x] 8.7 Final visual QA pass on mobile and desktop viewports
