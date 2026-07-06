export interface Game {
  id: string;
  name: string;
  platform: string;
  bookletRating: number;
  boxRating: number;
  mediaRating: number;
  registrationDate: string;
}

export interface PagedResponse {
  items: Game[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface GameFilters {
  search?: string;
  platforms?: string[];
  minBooklet?: number;
  minBox?: number;
  minMedia?: number;
  sortBy?: string;
  sortDescending?: boolean;
  page?: number;
  pageSize?: number;
}

const API_BASE = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5039";

export async function fetchGames(filters: GameFilters = {}): Promise<PagedResponse> {
  const params = new URLSearchParams();
  if (filters.search) params.set("search", filters.search);
  if (filters.platforms?.length) params.set("platforms", filters.platforms.join(","));
  if (filters.minBooklet) params.set("minBooklet", String(filters.minBooklet));
  if (filters.minBox) params.set("minBox", String(filters.minBox));
  if (filters.minMedia) params.set("minMedia", String(filters.minMedia));
  if (filters.sortBy) params.set("sortBy", filters.sortBy);
  if (filters.sortDescending) params.set("sortDescending", "true");
  if (filters.page) params.set("page", String(filters.page));
  if (filters.pageSize) params.set("pageSize", String(filters.pageSize));

  const res = await fetch(`${API_BASE}/api/games?${params}`, { cache: "no-store" });
  if (!res.ok) throw new Error("Failed to fetch games");
  return res.json();
}

export async function fetchPlatforms(): Promise<string[]> {
  const res = await fetch(`${API_BASE}/api/platforms`, { cache: "no-store" });
  if (!res.ok) throw new Error("Failed to fetch platforms");
  return res.json();
}

export async function addGame(data: Omit<Game, "id" | "registrationDate">): Promise<Game> {
  const res = await fetch(`${API_BASE}/api/games`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error("Failed to add game");
  return res.json();
}

export async function updateGame(id: string, data: Omit<Game, "id" | "registrationDate">): Promise<Game> {
  const res = await fetch(`${API_BASE}/api/games/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error("Failed to update game");
  return res.json();
}

export async function deleteGame(id: string): Promise<void> {
  const res = await fetch(`${API_BASE}/api/games/${id}`, { method: "DELETE" });
  if (!res.ok) throw new Error("Failed to delete game");
}
