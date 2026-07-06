using GameCatalog.Domain;

namespace GameCatalog.Domain.Tests;

public class GameTests
{
    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var condition = new ConditionRating(4, 3, 5);

        var game = new Game("Super Mario World", "SNES", condition);

        Assert.Equal("Super Mario World", game.Name);
        Assert.Equal("SNES", game.Platform);
        Assert.Equal(condition, game.Condition);
        Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow), game.RegistrationDate);
        Assert.NotEqual(Guid.Empty, game.Id);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidName_ShouldThrow(string? name)
    {
        var condition = new ConditionRating(3, 3, 3);

        Assert.Throws<ArgumentException>(() => new Game(name!, "SNES", condition));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidPlatform_ShouldThrow(string? platform)
    {
        var condition = new ConditionRating(3, 3, 3);

        Assert.Throws<ArgumentException>(() => new Game("Test Game", platform!, condition));
    }

    [Fact]
    public void Create_WithNullCondition_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() => new Game("Test Game", "SNES", null!));
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateFields()
    {
        var game = new Game("Old Name", "NES", new ConditionRating(1, 1, 1));
        var newCondition = new ConditionRating(5, 5, 5);

        game.Update("New Name", "SNES", newCondition);

        Assert.Equal("New Name", game.Name);
        Assert.Equal("SNES", game.Platform);
        Assert.Equal(newCondition, game.Condition);
    }

    [Fact]
    public void Update_ShouldNotChangeRegistrationDate()
    {
        var game = new Game("Test", "SNES", new ConditionRating(3, 3, 3));
        var originalDate = game.RegistrationDate;

        game.Update("Updated", "NES", new ConditionRating(5, 5, 5));

        Assert.Equal(originalDate, game.RegistrationDate);
    }

    [Fact]
    public void Update_WithInvalidName_ShouldThrow()
    {
        var game = new Game("Test", "SNES", new ConditionRating(3, 3, 3));

        Assert.Throws<ArgumentException>(() => game.Update("", "SNES", new ConditionRating(3, 3, 3)));
    }

    [Fact]
    public void Update_WithInvalidPlatform_ShouldThrow()
    {
        var game = new Game("Test", "SNES", new ConditionRating(3, 3, 3));

        Assert.Throws<ArgumentException>(() => game.Update("Test", "", new ConditionRating(3, 3, 3)));
    }

    [Fact]
    public void Update_WithNullCondition_ShouldThrow()
    {
        var game = new Game("Test", "SNES", new ConditionRating(3, 3, 3));

        Assert.Throws<ArgumentNullException>(() => game.Update("Test", "SNES", null!));
    }
}
