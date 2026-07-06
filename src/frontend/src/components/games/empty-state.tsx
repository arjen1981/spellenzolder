"use client";

import { Gamepad2 } from "lucide-react";

export function EmptyState() {
  return (
    <div className="flex flex-col items-center justify-center py-16 text-center">
      <div className="relative mb-6">
        <Gamepad2 size={80} className="text-neon-magenta opacity-60" />
        <div className="absolute -top-2 -right-2 w-4 h-4 bg-neon-green rounded-full animate-pulse" />
      </div>
      <h2 className="arcade-heading text-neon-cyan text-sm md:text-base mb-3">
        NO GAMES YET
      </h2>
      <p className="text-muted-foreground text-sm max-w-md">
        Your collection is empty! Add your first retro game to start tracking your collection.
      </p>
      <p className="text-muted-foreground text-xs mt-2 opacity-60">
        INSERT COIN TO CONTINUE...
      </p>
    </div>
  );
}
