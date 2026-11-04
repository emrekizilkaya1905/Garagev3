using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageLookupTests
{
    [Theory]
    [InlineData("ABC123")]
    [InlineData("aBc123")]
    [InlineData(" ABC123 ")]
    public void FindByRegistrationNumber_ExistingRegistrationWithAnyCase_ReturnsCorrectVehicle(string registration)
    {
        var garage = MixedGarage();
        var expected = garage.GetAllVehicles()[0];
        var result = garage.FindByRegistrationNumber(registration);
        Assert.Same(expected, result);
    }

    [Theory]
    [InlineData("XYZ999")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void FindByRegistrationNumber_UnknownOrBlankRegistration_ReturnsNull(string? registration)
    {
        var garage = MixedGarage();
        var result = garage.FindByRegistrationNumber(registration!);
        Assert.Null(result);
    }
}

