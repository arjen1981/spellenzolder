# Responsive Layout

## Purpose

Ensures the application provides an optimized experience across all device sizes, from mobile to desktop.

## Requirements

### Requirement: Application is fully responsive
The system SHALL provide an optimized layout for both mobile (< 768px) and desktop (≥ 768px) viewports.

#### Scenario: Desktop layout
- **WHEN** user views the application on a screen ≥ 768px wide
- **THEN** the collection table shows all columns (title, platform, ratings, date) and forms use a comfortable multi-column layout

#### Scenario: Mobile layout
- **WHEN** user views the application on a screen < 768px wide
- **THEN** the collection switches to a card-based layout, forms stack vertically, and all interactive elements are touch-friendly (minimum 44px tap targets)

### Requirement: Touch-friendly interactions on mobile
The system SHALL ensure all interactive elements are usable via touch on mobile devices.

#### Scenario: Star rating on touch
- **WHEN** user taps a star in the rating component on mobile
- **THEN** the rating is set accurately without requiring precision tapping (adequate spacing between stars)

#### Scenario: Navigation on mobile
- **WHEN** user accesses the app on mobile
- **THEN** navigation SHALL be accessible via a hamburger menu or bottom navigation bar

### Requirement: No horizontal scroll on any viewport
The system SHALL never produce horizontal scrollbars on viewports ≥ 320px wide.

#### Scenario: Narrow viewport
- **WHEN** user views the app on a 320px wide screen
- **THEN** all content fits within the viewport width without horizontal overflow

### Requirement: Fast initial load on mobile
The system SHALL leverage Next.js Server Components and streaming to ensure fast initial page load on mobile networks.

#### Scenario: Collection page load
- **WHEN** user navigates to the collection page on a slow connection
- **THEN** a skeleton loader is shown immediately while data streams in from the server
