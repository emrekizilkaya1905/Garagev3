using Garagev3.Models;
using Garagev3.Services;
using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageSearchTests
{
    public static IEnumerable<object[]> FilterCombinations => Enumerable.Range(0, 16).Select(mask => new object[] { mask });

    [Theory]
    [InlineData(0)] [InlineData(1)] [InlineData(2)] [InlineData(3)]
    public void Search_SingleCriterion_ReturnsOnlyMatchingVehicles(int filter)
    {
        var garage = MixedGarage();
        var criteria = filter switch
        {
            0 => new VehicleSearchCriteria { RegistrationNumber = "abc123" },
            1 => new VehicleSearchCriteria { Color = "RED" },
            2 => new VehicleSearchCriteria { NumberOfWheels = 0 },
            _ => new VehicleSearchCriteria { VehicleType = typeof(Motorcycle) }
        };
        var result = garage.Search(criteria);
        Assert.Equal(new[] { "ABC123", "DEF456", "BOA001", "MCA001" }[filter], Assert.Single(result).RegistrationNumber);
    }

    [Theory]
    [MemberData(nameof(FilterCombinations))]
    public void Search_RegistrationColorWheelsCombinations_ReturnsExpectedMatches(int mask)
    {
        // Arrange: all 16 subsets of registration, color, wheels and type.
        var garage = MixedGarage();
        var criteria = new VehicleSearchCriteria
        {
            RegistrationNumber = (mask & 1) != 0 ? "abc123" : "",
            Color = (mask & 2) != 0 ? "BLACK" : " ",
            NumberOfWheels = (mask & 4) != 0 ? 4 : null,
            VehicleType = (mask & 8) != 0 ? typeof(Car) : null
        };
        var expected = new List<string> { "ABC123" };
        if ((mask & 3) == 0) expected.Add("DEF456");
        if (mask == 0) expected.Add("MCA001");
        if ((mask & 13) == 0) expected.Add("BOA001");
        // Act
        var result = garage.Search(criteria);
        // Assert
        Assert.Equal(expected, result.Select(v => v.RegistrationNumber));
    }

    [Fact]
    public void Search_MultipleCriteria_ReturnsOnlyVehiclesMatchingEveryCriterion()
    {
        var garage = MixedGarage();
        var result = garage.Search(new VehicleSearchCriteria { RegistrationNumber = "ABC123", Color = "Red" });
        Assert.Empty(result);
    }

    [Fact]
    public void Search_NoCriteria_ReturnsAllVehicles()
    {
        var garage = MixedGarage();
        var result = garage.Search(new VehicleSearchCriteria());
        Assert.Equal(garage.GetAllVehicles(), result);
    }

    [Fact]
    public void Search_NoMatches_ReturnsEmptyCollection()
    {
        var garage = MixedGarage();
        var result = garage.Search(new VehicleSearchCriteria { Color = "Green" });
        Assert.Empty(result);
    }

    [Fact]
    public void Search_NullCriteria_ThrowsArgumentNullException()
    {
        var garage = MixedGarage();
        Assert.Throws<ArgumentNullException>(() => garage.Search(null!));
    }

    [Fact]
    public void Search_NegativeWheels_ThrowsArgumentOutOfRangeException()
    {
        var garage = MixedGarage();
        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Search(new VehicleSearchCriteria { NumberOfWheels = -1 }));
    }

    [Fact]
    public void Search_InvalidVehicleType_ThrowsArgumentException()
    {
        var garage = MixedGarage();
        Assert.Throws<ArgumentException>(() => garage.Search(new VehicleSearchCriteria { VehicleType = typeof(string) }));
    }
}

