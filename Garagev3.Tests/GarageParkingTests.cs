using Garagev3.Models;
using Garagev3.Services;
using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageParkingTests
{
    [Fact]
    public void Park_AvailableSpace_AddsVehicleAndIncreasesCount()
    {
        // Arrange
        var garage = new Garage<Car>(1);
        var car = Car();
        // Act
        var result = garage.Park(car);
        // Assert
        Assert.True(result.Success);
        Assert.Same(car, Assert.Single(garage));
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Park_FullGarage_ReturnsFailureWithoutChangingVehicles()
    {
        // Arrange
        var garage = new Garage<Car>(1);
        var car = Car();
        garage.Park(car);
        // Act
        var result = garage.Park(Car("DEF456"));
        // Assert
        Assert.False(result.Success);
        Assert.Same(car, Assert.Single(garage));
        Assert.Equal(1, garage.Capacity);
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    public void Park_DuplicateRegistrationWithAnyCase_ReturnsFailure(string registration)
    {
        // Arrange
        var garage = new Garage<Car>(2);
        garage.Park(Car());
        // Act
        var result = garage.Park(Car(registration));
        // Assert
        Assert.False(result.Success);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Park_NullVehicle_ThrowsArgumentNullException()
    {
        var garage = new Garage<Car>(1);
        Assert.Throws<ArgumentNullException>(() => garage.Park(null!));
    }

    [Fact]
    public void Park_AfterRemoval_ReusesAvailableCapacity()
    {
        var garage = new Garage<Car>(1);
        garage.Park(Car());
        garage.Remove("ABC123");
        var replacement = Car("DEF456");
        var result = garage.Park(replacement);
        Assert.True(result.Success);
        Assert.Same(replacement, Assert.Single(garage));
    }
}

