export function GameListSkeleton() {
  return (
    <div className="space-y-3 animate-pulse">
      {[...Array(5)].map((_, i) => (
        <div key={i} className="arcade-card rounded-lg p-4 bg-card">
          <div className="flex justify-between items-start mb-2">
            <div className="space-y-2">
              <div className="h-4 w-40 bg-secondary rounded" />
              <div className="h-5 w-16 bg-secondary rounded" />
            </div>
            <div className="flex gap-1">
              <div className="h-8 w-8 bg-secondary rounded" />
              <div className="h-8 w-8 bg-secondary rounded" />
            </div>
          </div>
          <div className="grid grid-cols-3 gap-2 mt-3">
            <div className="h-4 bg-secondary rounded" />
            <div className="h-4 bg-secondary rounded" />
            <div className="h-4 bg-secondary rounded" />
          </div>
        </div>
      ))}
    </div>
  );
}
