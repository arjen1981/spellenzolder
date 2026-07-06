namespace GameCatalog.Domain;

public sealed class Game
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Platform { get; private set; }
    public ConditionRating Condition { get; private set; }
    public DateOnly RegistrationDate { get; private set; }

    private Game()
    {
        Name = null!;
        Platform = null!;
        Condition = null!;
    }

    public Game(string name, string platform, ConditionRating condition)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(platform))
            throw new ArgumentException("Platform is required.", nameof(platform));

        Id = Guid.NewGuid();
        Name = name;
        Platform = platform;
        Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public void Update(string name, string platform, ConditionRating condition)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(platform))
            throw new ArgumentException("Platform is required.", nameof(platform));

        Name = name;
        Platform = platform;
        Condition = condition ?? throw new ArgumentNullException(nameof(condition));
    }
}
