"use client";

import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { StarRating } from "./star-rating";
import { Search, X, RotateCcw } from "lucide-react";
import { useState, useEffect } from "react";
import type { GameFilters } from "@/lib/api";

interface GameFiltersBarProps {
  platforms: string[];
  filters: GameFilters;
  onFiltersChange: (filters: GameFilters) => void;
}

export function GameFiltersBar({ platforms, filters, onFiltersChange }: GameFiltersBarProps) {
  const [search, setSearch] = useState(filters.search ?? "");

  useEffect(() => {
    const timeout = setTimeout(() => {
      onFiltersChange({ ...filters, search: search || undefined, page: 1 });
    }, 300);
    return () => clearTimeout(timeout);
  }, [search]);

  function togglePlatform(platform: string) {
    const current = filters.platforms ?? [];
    const updated = current.includes(platform)
      ? current.filter((p) => p !== platform)
      : [...current, platform];
    onFiltersChange({ ...filters, platforms: updated.length ? updated : undefined, page: 1 });
  }

  function resetFilters() {
    setSearch("");
    onFiltersChange({ page: 1, pageSize: filters.pageSize });
  }

  const hasActiveFilters = !!(filters.search || filters.platforms?.length || filters.minBooklet || filters.minBox || filters.minMedia);

  return (
    <div className="space-y-3">
      {/* Search */}
      <div className="relative">
        <Search size={16} className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground" />
        <Input
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          placeholder="Search games..."
          className="pl-9 bg-secondary border-border focus:border-neon-green"
        />
        {search && (
          <button
            onClick={() => setSearch("")}
            className="absolute right-3 top-1/2 -translate-y-1/2 text-muted-foreground hover:text-foreground"
          >
            <X size={14} />
          </button>
        )}
      </div>

      {/* Platform filters */}
      {platforms.length > 0 && (
        <div className="flex flex-wrap gap-2">
          {platforms.map((platform) => (
            <button
              key={platform}
              onClick={() => togglePlatform(platform)}
              className={`px-3 py-1 text-xs rounded border transition-all ${
                filters.platforms?.includes(platform)
                  ? "border-neon-green bg-neon-green/10 text-neon-green"
                  : "border-border text-muted-foreground hover:border-neon-cyan hover:text-neon-cyan"
              }`}
            >
              {platform}
            </button>
          ))}
        </div>
      )}

      {/* Condition filters */}
      <div className="flex flex-wrap gap-4 items-center">
        <div className="flex items-center gap-2">
          <span className="text-xs text-muted-foreground">Min Booklet:</span>
          <StarRating
            value={filters.minBooklet ?? 0}
            onChange={(v) => onFiltersChange({ ...filters, minBooklet: v > 0 ? v : undefined, page: 1 })}
            size={14}
          />
        </div>
        <div className="flex items-center gap-2">
          <span className="text-xs text-muted-foreground">Min Box:</span>
          <StarRating
            value={filters.minBox ?? 0}
            onChange={(v) => onFiltersChange({ ...filters, minBox: v > 0 ? v : undefined, page: 1 })}
            size={14}
          />
        </div>
        <div className="flex items-center gap-2">
          <span className="text-xs text-muted-foreground">Min Media:</span>
          <StarRating
            value={filters.minMedia ?? 0}
            onChange={(v) => onFiltersChange({ ...filters, minMedia: v > 0 ? v : undefined, page: 1 })}
            size={14}
          />
        </div>
        {hasActiveFilters && (
          <Button variant="ghost" size="sm" onClick={resetFilters} className="text-neon-magenta hover:text-neon-magenta/80">
            <RotateCcw size={14} className="mr-1" />
            Reset
          </Button>
        )}
      </div>
    </div>
  );
}
