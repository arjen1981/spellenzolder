## ADDED Requirements

### Requirement: Application uses 16-bit arcade visual theme
The system SHALL use a retro 16-bit arcade aesthetic with pixel fonts, neon accent colors on a dark background, and arcade-inspired UI elements.

#### Scenario: Pixel font for headings
- **WHEN** the application renders headings and titles
- **THEN** a pixel-style font (Press Start 2P or equivalent) SHALL be used

#### Scenario: Dark background with neon accents
- **WHEN** the application renders any page
- **THEN** the background SHALL be dark (near-black or deep purple) with neon-colored accents (cyan, magenta, green, yellow) for interactive elements

#### Scenario: Body text readability
- **WHEN** the application renders body text and form labels
- **THEN** a readable sans-serif font SHALL be used at sufficient size for legibility, while maintaining the retro color scheme

### Requirement: Star ratings use pixel-style icons
The condition star ratings SHALL use pixelated/retro-styled star icons consistent with the 16-bit theme.

#### Scenario: Filled vs empty stars
- **WHEN** a game has a manual rating of 3 out of 5
- **THEN** 3 filled pixel stars and 2 empty pixel stars are displayed

#### Scenario: Interactive star input
- **WHEN** user hovers over stars in the rating input
- **THEN** stars light up with a neon glow effect indicating the potential rating

### Requirement: Arcade-style UI feedback
The system SHALL provide arcade-inspired feedback for user actions.

#### Scenario: Success toast
- **WHEN** user successfully adds a game
- **THEN** a toast notification appears with retro styling (pixel border, neon glow, arcade sound optional)

#### Scenario: Button interactions
- **WHEN** user hovers or clicks a button
- **THEN** the button SHALL show a retro-styled hover/active state (glow, color shift, or pixel animation)

### Requirement: Consistent retro color palette
The application SHALL use a defined retro color palette applied consistently across all components via CSS custom properties.

#### Scenario: Theme colors defined
- **WHEN** the application is loaded
- **THEN** the following theme colors SHALL be available: background (dark), foreground (light), primary (neon cyan), secondary (neon magenta), accent (neon green), warning (neon yellow), destructive (neon red)

#### Scenario: Components use theme colors
- **WHEN** any UI component renders
- **THEN** it SHALL use only colors from the defined retro palette, not default shadcn/ui colors
