"use client";

import { useState, useEffect, useCallback } from "react";
import { fetchGames, fetchPlatforms, type GameFilters, type PagedResponse } from "@/lib/api";
import { GameFiltersBar } from "@/components/games/game-filters";
import { GameTable } from "@/components/games/game-table";
import { GameCards } from "@/components/games/game-cards";
import { GameForm } from "@/components/games/game-form";
import { EmptyState } from "@/components/games/empty-state";
import { GameListSkeleton } from "@/components/games/game-list-skeleton";
import { Button } from "@/components/ui/button";
import { ChevronLeft, ChevronRight } from "lucide-react";

export default function HomePage() {
  const [data, setData] = useState<PagedResponse | null>(null);
  const [platforms, setPlatforms] = useState<string[]>([]);
  const [filters, setFilters] = useState<GameFilters>({ page: 1, pageSize: 20, sortBy: "name" });
  const [loading, setLoading] = useState(true);

  const loadData = useCallback(async () => {
    setLoading(true);
    try {
      const [gamesResult, platformsResult] = await Promise.all([
        fetchGames(filters),
        fetchPlatforms(),
      ]);
      setData(gamesResult);
      setPlatforms(platformsResult);
    } catch {
      setData({ items: [], totalCount: 0, page: 1, pageSize: 20, totalPages: 0 });
    } finally {
      setLoading(false);
    }
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const hasGames = data && data.totalCount > 0;
  const isEmpty = data && data.totalCount === 0 && !filters.search && !filters.platforms?.length;

  return (
    <div className="max-w-7xl mx-auto space-y-6">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <div>
          <p className="text-sm text-muted-foreground">
            {data ? `${data.totalCount} games in collection` : "Loading..."}
          </p>
        </div>
        <GameForm platforms={platforms} onSuccess={loadData} />
      </div>

      {!isEmpty && (
        <GameFiltersBar
          platforms={platforms}
          filters={filters}
          onFiltersChange={setFilters}
        />
      )}

      {loading ? (
        <GameListSkeleton />
      ) : isEmpty ? (
        <EmptyState />
      ) : hasGames ? (
        <>
          <GameTable
            games={data.items}
            platforms={platforms}
            filters={filters}
            onFiltersChange={setFilters}
            onRefresh={loadData}
          />
          <GameCards
            games={data.items}
            platforms={platforms}
            onRefresh={loadData}
          />
          {data.totalPages > 1 && (
            <div className="flex justify-center items-center gap-4 pt-4">
              <Button
                variant="ghost"
                size="sm"
                disabled={!data || data.page <= 1}
                onClick={() => setFilters({ ...filters, page: (filters.page ?? 1) - 1 })}
                className="text-neon-cyan"
              >
                <ChevronLeft size={16} />
                PREV
              </Button>
              <span className="text-sm text-muted-foreground">
                Page {data.page} of {data.totalPages}
              </span>
              <Button
                variant="ghost"
                size="sm"
                disabled={!data || data.page >= data.totalPages}
                onClick={() => setFilters({ ...filters, page: (filters.page ?? 1) + 1 })}
                className="text-neon-cyan"
              >
                NEXT
                <ChevronRight size={16} />
              </Button>
            </div>
          )}
        </>
      ) : (
        <div className="text-center py-8">
          <p className="text-muted-foreground">No games match your filters.</p>
        </div>
      )}
    </div>
  );
}
