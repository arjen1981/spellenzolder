"use client";

import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { StarRating } from "./star-rating";
import { addGame, updateGame, type Game } from "@/lib/api";
import { toast } from "sonner";
import { Plus, Pencil } from "lucide-react";

interface GameFormProps {
  game?: Game;
  platforms?: string[];
  onSuccess: () => void;
}

export function GameForm({ game, platforms = [], onSuccess }: GameFormProps) {
  const [open, setOpen] = useState(false);
  const [name, setName] = useState(game?.name ?? "");
  const [platform, setPlatform] = useState(game?.platform ?? "");
  const [bookletRating, setBookletRating] = useState(game?.bookletRating ?? 3);
  const [boxRating, setBoxRating] = useState(game?.boxRating ?? 3);
  const [mediaRating, setMediaRating] = useState(game?.mediaRating ?? 3);
  const [loading, setLoading] = useState(false);
  const [showSuggestions, setShowSuggestions] = useState(false);

  const isEdit = !!game;

  const filteredPlatforms = platforms.filter((p) =>
    p.toLowerCase().includes(platform.toLowerCase())
  );

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!name.trim() || !platform.trim()) {
      toast.error("Name and platform are required!");
      return;
    }

    setLoading(true);
    try {
      const data = { name, platform, bookletRating, boxRating, mediaRating };
      if (isEdit) {
        await updateGame(game.id, data);
        toast.success("GAME UPDATED!");
      } else {
        await addGame(data);
        toast.success("GAME ADDED!");
      }
      setOpen(false);
      resetForm();
      onSuccess();
    } catch {
      toast.error("Something went wrong...");
    } finally {
      setLoading(false);
    }
  }

  function resetForm() {
    if (!isEdit) {
      setName("");
      setPlatform("");
      setBookletRating(3);
      setBoxRating(3);
      setMediaRating(3);
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger
        className={isEdit
          ? "inline-flex items-center justify-center h-7 w-7 rounded-lg text-neon-cyan hover:text-neon-green hover:bg-muted transition-all"
          : "inline-flex items-center justify-center gap-2 h-8 px-3 rounded-lg bg-neon-green text-background hover:bg-neon-green/80 font-bold text-xs"
        }
      >
        {isEdit ? <Pencil size={14} /> : <><Plus size={16} /> ADD GAME</>}
      </DialogTrigger>
      <DialogContent className="bg-card border-border">
        <DialogHeader>
          <DialogTitle className="arcade-heading text-neon-green text-sm">
            {isEdit ? "EDIT GAME" : "ADD NEW GAME"}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="text-xs text-muted-foreground mb-1 block">NAME</label>
            <Input
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Super Mario World"
              className="bg-secondary border-border focus:border-neon-green"
            />
          </div>
          <div className="relative">
            <label className="text-xs text-muted-foreground mb-1 block">PLATFORM</label>
            <Input
              value={platform}
              onChange={(e) => {
                setPlatform(e.target.value);
                setShowSuggestions(true);
              }}
              onFocus={() => setShowSuggestions(true)}
              onBlur={() => setTimeout(() => setShowSuggestions(false), 200)}
              placeholder="SNES"
              className="bg-secondary border-border focus:border-neon-green"
            />
            {showSuggestions && filteredPlatforms.length > 0 && platform && (
              <div className="absolute z-10 w-full mt-1 bg-card border border-border rounded-md shadow-lg max-h-32 overflow-y-auto">
                {filteredPlatforms.map((p) => (
                  <button
                    key={p}
                    type="button"
                    className="w-full px-3 py-2 text-left text-sm hover:bg-secondary"
                    onClick={() => {
                      setPlatform(p);
                      setShowSuggestions(false);
                    }}
                  >
                    {p}
                  </button>
                ))}
              </div>
            )}
          </div>
          <div className="space-y-3">
            <div className="flex items-center justify-between">
              <label className="text-xs text-muted-foreground">BOOKLET</label>
              <StarRating value={bookletRating} onChange={setBookletRating} />
            </div>
            <div className="flex items-center justify-between">
              <label className="text-xs text-muted-foreground">BOX</label>
              <StarRating value={boxRating} onChange={setBoxRating} />
            </div>
            <div className="flex items-center justify-between">
              <label className="text-xs text-muted-foreground">MEDIA</label>
              <StarRating value={mediaRating} onChange={setMediaRating} />
            </div>
          </div>
          <Button
            type="submit"
            disabled={loading}
            className="w-full bg-neon-green text-background hover:bg-neon-green/80 font-bold"
          >
            {loading ? "SAVING..." : isEdit ? "UPDATE" : "ADD TO COLLECTION"}
          </Button>
        </form>
      </DialogContent>
    </Dialog>
  );
}
