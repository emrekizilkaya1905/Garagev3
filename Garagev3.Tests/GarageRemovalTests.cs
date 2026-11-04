using Garagev3.Models;
using Garagev3.Services;
using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageRemovalTests
{
    [Theory]
    [InlineData("ABC123")]
    [InlineData("aBc123")]
    public void Remove_ExistingRegistrationWithAnyCase_RemovesVehicleAndDecreasesCount(string registration)
    {
        // Arrange
        var garage = new Garage<Car>(1);
        garage.Park(Car());
        // Act
        var result = garage.Remove(registration);
        // Assert
        Assert.True(result.Success);
        Assert.Equal(0, garage.Count);
        Assert.Empty(garage);
    }

    [Theory]
    [InlineData("XYZ999")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Remove_UnknownOrBlankRegistration_ReturnsFailureWithoutChangingCount(string? registration)
    {
        var garage = MixedGarage();
        var result = garage.Remove(registration!);
        Assert.False(result.Success);
        Assert.Equal(4, garage.Count);
    }

    [Fact]
    public void Remove_EmptyGarage_ReturnsFailure()
    {
        var garage = new Garage<Car>(1);
        var result = garage.Remove("ABC123");
        Assert.False(result.Success);
        Assert.Empty(garage);
    }
}

