using Garagev3.Models;
using Garagev3.Services;
using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageListingTests
{
    [Fact]
    public void GetAllVehicles_EmptyGarage_ReturnsEmptyCollection()
    {
        var garage = new Garage<Vehicle>(1);
        var result = garage.GetAllVehicles();
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllVehicles_ParkedVehicles_ReturnsAllVehicles()
    {
        var garage = MixedGarage();
        var result = garage.GetAllVehicles();
        Assert.Equal(new[] { "ABC123", "DEF456", "MCA001", "BOA001" }, result.Select(v => v.RegistrationNumber));
    }

    [Fact]
    public void GetAllVehicles_ModifiedReturnedCollection_DoesNotChangeGarage()
    {
        var garage = MixedGarage();
        var result = garage.GetAllVehicles();
        result.Clear();
        Assert.Equal(4, garage.Count);
    }

    [Fact]
    public void GetVehicleTypeCounts_MixedVehicles_ReturnsCorrectCounts()
    {
        var garage = MixedGarage();
        var result = garage.GetVehicleTypeCounts();
        Assert.Equal(2, result[nameof(Car)]);
        Assert.Equal(1, result[nameof(Motorcycle)]);
        Assert.Equal(1, result[nameof(Boat)]);
    }

    [Fact]
    public void GetVehicleTypeCounts_AfterRemoval_UpdatesCounts()
    {
        var garage = MixedGarage();
        garage.Remove("MCA001");
        var result = garage.GetVehicleTypeCounts();
        Assert.False(result.ContainsKey(nameof(Motorcycle)));
        Assert.Equal(3, result.Values.Sum());
    }
}

