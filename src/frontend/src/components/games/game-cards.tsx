"use client";

import type { Game } from "@/lib/api";
import { StarRating } from "./star-rating";
import { GameForm } from "./game-form";
import { DeleteGameDialog } from "./delete-game-dialog";

interface GameCardsProps {
  games: Game[];
  platforms: string[];
  onRefresh: () => void;
}

export function GameCards({ games, platforms, onRefresh }: GameCardsProps) {
  return (
    <div className="md:hidden space-y-3">
      {games.map((game) => (
        <div key={game.id} className="arcade-card rounded-lg p-4 bg-card">
          <div className="flex justify-between items-start mb-2">
            <div>
              <h3 className="font-medium text-foreground">{game.name}</h3>
              <span className="text-xs px-2 py-0.5 rounded bg-secondary text-neon-cyan border border-border">
                {game.platform}
              </span>
            </div>
            <div className="flex gap-1">
              <GameForm game={game} platforms={platforms} onSuccess={onRefresh} />
              <DeleteGameDialog gameId={game.id} gameName={game.name} onSuccess={onRefresh} />
            </div>
          </div>
          <div className="grid grid-cols-3 gap-2 mt-3">
            <div>
              <span className="text-[10px] text-muted-foreground block">BOOKLET</span>
              <StarRating value={game.bookletRating} readonly size={12} />
            </div>
            <div>
              <span className="text-[10px] text-muted-foreground block">BOX</span>
              <StarRating value={game.boxRating} readonly size={12} />
            </div>
            <div>
              <span className="text-[10px] text-muted-foreground block">MEDIA</span>
              <StarRating value={game.mediaRating} readonly size={12} />
            </div>
          </div>
          <p className="text-xs text-muted-foreground mt-2">Added: {game.registrationDate}</p>
        </div>
      ))}
    </div>
  );
}
