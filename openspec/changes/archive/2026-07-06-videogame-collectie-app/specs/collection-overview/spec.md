## ADDED Requirements

### Requirement: User can view collection as a list
The system SHALL display all registered games in a tabular overview showing title, platform, condition ratings, and registration date.

#### Scenario: View collection with games
- **WHEN** user navigates to the collection overview and has registered games
- **THEN** the system displays a table/list with all games showing title, platform, star ratings, and registration date

#### Scenario: View empty collection
- **WHEN** user navigates to the collection overview and has no registered games
- **THEN** the system displays an empty state with a prompt to add the first game

### Requirement: Collection supports server-side pagination
The system SHALL paginate the collection overview with server-side paging to handle large collections efficiently.

#### Scenario: Default page size
- **WHEN** user views the collection without specifying page size
- **THEN** the system displays 20 games per page

#### Scenario: Navigate between pages
- **WHEN** user clicks next/previous page
- **THEN** the system loads the corresponding page from the server

### Requirement: Collection supports filtering by platform
The system SHALL allow the user to filter the collection overview by platform.

#### Scenario: Filter by single platform
- **WHEN** user selects "SNES" from the platform filter
- **THEN** the system displays only games registered for the SNES platform

#### Scenario: Clear filter
- **WHEN** user clears the platform filter
- **THEN** the system displays all games regardless of platform

### Requirement: Collection supports sorting
The system SHALL allow the user to sort the collection by title, platform, registration date, or overall condition.

#### Scenario: Sort by title ascending
- **WHEN** user clicks the title column header
- **THEN** the system sorts games alphabetically A-Z by title

#### Scenario: Toggle sort direction
- **WHEN** user clicks the same column header again
- **THEN** the system reverses the sort direction

#### Scenario: Sort by registration date
- **WHEN** user sorts by registration date descending
- **THEN** the most recently added games appear first

### Requirement: Collection supports text search
The system SHALL allow the user to search games by title.

#### Scenario: Search by partial title
- **WHEN** user types "Mario" in the search box
- **THEN** the system filters to show only games whose title contains "Mario" (case-insensitive)

#### Scenario: Search with no results
- **WHEN** user searches for a title that matches no games
- **THEN** the system displays a "no results" message
