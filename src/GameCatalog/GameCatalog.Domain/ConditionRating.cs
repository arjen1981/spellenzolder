namespace GameCatalog.Domain;

public sealed record ConditionRating
{
    public int Booklet { get; }
    public int Box { get; }
    public int Media { get; }

    public ConditionRating(int booklet, int box, int media)
    {
        if (booklet is < 1 or > 5)
            throw new ArgumentOutOfRangeException(nameof(booklet), "Rating must be between 1 and 5.");
        if (box is < 1 or > 5)
            throw new ArgumentOutOfRangeException(nameof(box), "Rating must be between 1 and 5.");
        if (media is < 1 or > 5)
            throw new ArgumentOutOfRangeException(nameof(media), "Rating must be between 1 and 5.");

        Booklet = booklet;
        Box = box;
        Media = media;
    }
}
