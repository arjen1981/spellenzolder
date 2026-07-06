using GameCatalog.Domain;

namespace GameCatalog.Domain.Tests;

public class ConditionRatingTests
{
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(5, 5, 5)]
    [InlineData(3, 4, 2)]
    public void Create_WithValidRatings_ShouldSucceed(int booklet, int box, int media)
    {
        var rating = new ConditionRating(booklet, box, media);

        Assert.Equal(booklet, rating.Booklet);
        Assert.Equal(box, rating.Box);
        Assert.Equal(media, rating.Media);
    }

    [Theory]
    [InlineData(0, 3, 3)]
    [InlineData(6, 3, 3)]
    [InlineData(-1, 3, 3)]
    public void Create_WithInvalidBooklet_ShouldThrow(int booklet, int box, int media)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ConditionRating(booklet, box, media));
        Assert.Equal("booklet", exception.ParamName);
    }

    [Theory]
    [InlineData(3, 0, 3)]
    [InlineData(3, 6, 3)]
    public void Create_WithInvalidBox_ShouldThrow(int booklet, int box, int media)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ConditionRating(booklet, box, media));
        Assert.Equal("box", exception.ParamName);
    }

    [Theory]
    [InlineData(3, 3, 0)]
    [InlineData(3, 3, 6)]
    public void Create_WithInvalidMedia_ShouldThrow(int booklet, int box, int media)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ConditionRating(booklet, box, media));
        Assert.Equal("media", exception.ParamName);
    }

    [Fact]
    public void Equality_SameValues_ShouldBeEqual()
    {
        var rating1 = new ConditionRating(3, 4, 5);
        var rating2 = new ConditionRating(3, 4, 5);

        Assert.Equal(rating1, rating2);
    }

    [Fact]
    public void Equality_DifferentValues_ShouldNotBeEqual()
    {
        var rating1 = new ConditionRating(3, 4, 5);
        var rating2 = new ConditionRating(3, 4, 4);

        Assert.NotEqual(rating1, rating2);
    }
}
