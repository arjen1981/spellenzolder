## ADDED Requirements

### Requirement: User can register a new game
The system SHALL allow the user to register a new videogame with title, condition ratings (manual/box/media each 1-5 stars), platform, and registration date.

#### Scenario: Successful game registration
- **WHEN** user fills in title "Super Mario World", selects platform "SNES", rates manual 4 stars, box 3 stars, media 5 stars, and submits the form
- **THEN** the system persists the game and shows it in the collection overview with a success confirmation

#### Scenario: Registration with automatic registration date
- **WHEN** user submits a new game without explicitly setting a registration date
- **THEN** the system SHALL set the registration date to the current date

#### Scenario: Title is required
- **WHEN** user attempts to submit a game without a title
- **THEN** the system SHALL reject the submission and display a validation error on the title field

#### Scenario: Platform is required
- **WHEN** user attempts to submit a game without selecting a platform
- **THEN** the system SHALL reject the submission and display a validation error on the platform field

#### Scenario: Condition ratings default to unrated
- **WHEN** user submits a game without setting condition ratings
- **THEN** the system SHALL accept the game with all condition ratings set to 0 (unrated)

### Requirement: User can update an existing game
The system SHALL allow the user to edit all fields of a previously registered game.

#### Scenario: Successful update
- **WHEN** user changes the box rating from 3 to 5 stars and saves
- **THEN** the system persists the updated rating and shows the updated game details

#### Scenario: Update non-existing game
- **WHEN** user attempts to update a game that no longer exists
- **THEN** the system SHALL display an error indicating the game was not found

### Requirement: User can delete a game
The system SHALL allow the user to remove a game from the collection permanently.

#### Scenario: Successful deletion
- **WHEN** user confirms deletion of a game
- **THEN** the system removes the game from the database and the collection overview

#### Scenario: Deletion requires confirmation
- **WHEN** user clicks the delete button
- **THEN** the system SHALL prompt for confirmation before executing the deletion

### Requirement: Condition rating uses three dimensions
The system SHALL track game condition as three independent 1-5 star ratings: manual (boekje), box (doos), and media (cart/cd).

#### Scenario: Independent rating per dimension
- **WHEN** user sets manual to 2 stars, box to 5 stars, and media to 4 stars
- **THEN** each dimension is stored and displayed independently

#### Scenario: Rating value boundaries
- **WHEN** a condition rating value is provided
- **THEN** the system SHALL only accept values between 0 (unrated) and 5 (mint)

### Requirement: Platform selection from predefined list
The system SHALL provide a list of predefined gaming platforms for selection, including but not limited to: NES, SNES, N64, GameBoy, GameBoy Color, GameBoy Advance, Nintendo DS, Nintendo Switch, PlayStation, PS2, PS3, PS4, PS5, Sega Master System, Sega Genesis/Mega Drive, Sega Saturn, Sega Dreamcast, Xbox, Xbox 360, Xbox One, PC.

#### Scenario: Select predefined platform
- **WHEN** user opens the platform selector
- **THEN** the system displays a searchable list of predefined platforms

#### Scenario: Platform list is extensible
- **WHEN** user's desired platform is not in the predefined list
- **THEN** the user SHALL be able to type a custom platform name
