"use client";

import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { deleteGame } from "@/lib/api";
import { toast } from "sonner";
import { Trash2 } from "lucide-react";

interface DeleteGameDialogProps {
  gameId: string;
  gameName: string;
  onSuccess: () => void;
}

export function DeleteGameDialog({ gameId, gameName, onSuccess }: DeleteGameDialogProps) {
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);

  async function handleDelete() {
    setLoading(true);
    try {
      await deleteGame(gameId);
      toast.success("GAME DELETED!");
      setOpen(false);
      onSuccess();
    } catch {
      toast.error("Failed to delete game");
    } finally {
      setLoading(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger
        className="inline-flex items-center justify-center h-7 w-7 rounded-lg text-destructive hover:text-destructive/80 hover:bg-muted transition-all"
      >
        <Trash2 size={14} />
      </DialogTrigger>
      <DialogContent className="bg-card border-border">
        <DialogHeader>
          <DialogTitle className="arcade-heading text-destructive text-sm">
            DELETE GAME?
          </DialogTitle>
        </DialogHeader>
        <p className="text-sm text-muted-foreground">
          Are you sure you want to delete <span className="text-foreground font-bold">{gameName}</span>? This action cannot be undone.
        </p>
        <div className="flex gap-3 justify-end mt-4">
          <Button variant="secondary" onClick={() => setOpen(false)}>
            CANCEL
          </Button>
          <Button
            variant="destructive"
            onClick={handleDelete}
            disabled={loading}
          >
            {loading ? "DELETING..." : "DELETE"}
          </Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}
