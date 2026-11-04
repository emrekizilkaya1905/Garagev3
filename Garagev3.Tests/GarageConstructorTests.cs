using Garagev3.Models;
using Garagev3.Services;

namespace Garagev3.Tests;

public class GarageConstructorTests
{
    [Fact]
    public void Constructor_PositiveCapacity_CreatesEmptyGarage()
    {
        // Arrange
        int capacity = 2;
        // Act
        var garage = new Garage<Vehicle>(capacity);
        // Assert
        Assert.Equal(2, garage.Capacity);
        Assert.Equal(0, garage.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ZeroOrNegativeCapacity_ThrowsArgumentOutOfRangeException(int capacity)
    {
        // Arrange / Act / Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Garage<Vehicle>(capacity));
    }
}

