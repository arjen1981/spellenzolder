"use client";

import type { Game } from "@/lib/api";
import { StarRating } from "./star-rating";
import { GameForm } from "./game-form";
import { DeleteGameDialog } from "./delete-game-dialog";
import { ArrowUpDown } from "lucide-react";
import { Button } from "@/components/ui/button";
import type { GameFilters } from "@/lib/api";

interface GameTableProps {
  games: Game[];
  platforms: string[];
  filters: GameFilters;
  onFiltersChange: (filters: GameFilters) => void;
  onRefresh: () => void;
}

export function GameTable({ games, platforms, filters, onFiltersChange, onRefresh }: GameTableProps) {
  function handleSort(column: string) {
    if (filters.sortBy === column) {
      onFiltersChange({ ...filters, sortDescending: !filters.sortDescending });
    } else {
      onFiltersChange({ ...filters, sortBy: column, sortDescending: false });
    }
  }

  function SortButton({ column, label }: { column: string; label: string }) {
    const isActive = filters.sortBy === column;
    return (
      <Button
        variant="ghost"
        size="sm"
        onClick={() => handleSort(column)}
        className={`text-xs font-normal ${isActive ? "text-neon-green" : "text-muted-foreground"}`}
      >
        {label}
        <ArrowUpDown size={12} className="ml-1" />
      </Button>
    );
  }

  return (
    <div className="hidden md:block">
      <table className="w-full">
        <thead>
          <tr className="border-b border-border">
            <th className="text-left p-3"><SortButton column="name" label="NAME" /></th>
            <th className="text-left p-3"><SortButton column="platform" label="PLATFORM" /></th>
            <th className="text-center p-3"><SortButton column="booklet" label="BOOKLET" /></th>
            <th className="text-center p-3"><SortButton column="box" label="BOX" /></th>
            <th className="text-center p-3"><SortButton column="media" label="MEDIA" /></th>
            <th className="text-left p-3"><SortButton column="registrationdate" label="ADDED" /></th>
            <th className="p-3"></th>
          </tr>
        </thead>
        <tbody>
          {games.map((game) => (
            <tr key={game.id} className="border-b border-border/50 hover:bg-secondary/50 transition-colors">
              <td className="p-3 font-medium">{game.name}</td>
              <td className="p-3">
                <span className="px-2 py-0.5 text-xs rounded bg-secondary text-neon-cyan border border-border">
                  {game.platform}
                </span>
              </td>
              <td className="p-3"><StarRating value={game.bookletRating} readonly size={14} /></td>
              <td className="p-3"><StarRating value={game.boxRating} readonly size={14} /></td>
              <td className="p-3"><StarRating value={game.mediaRating} readonly size={14} /></td>
              <td className="p-3 text-sm text-muted-foreground">{game.registrationDate}</td>
              <td className="p-3">
                <div className="flex gap-1">
                  <GameForm game={game} platforms={platforms} onSuccess={onRefresh} />
                  <DeleteGameDialog gameId={game.id} gameName={game.name} onSuccess={onRefresh} />
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
