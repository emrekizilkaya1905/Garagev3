using System.Collections;
using Garagev3.Models;
using Garagev3.Services;
using static Garagev3.Tests.Helpers.TestVehicleFactory;

namespace Garagev3.Tests;

public class GarageEnumerationTests
{
    [Fact]
    public void GetEnumerator_ParkedVehicles_EnumeratesAllVehicles()
    {
        var garage = MixedGarage();
        var actual = new List<Vehicle>();
        foreach (Vehicle vehicle in garage) actual.Add(vehicle);
        Assert.Equal(garage.GetAllVehicles(), actual);
    }

    [Fact]
    public void GetEnumerator_EmptyGarage_EnumeratesNoVehicles()
    {
        var garage = new Garage<Vehicle>(1);
        var actual = new List<Vehicle>();
        foreach (Vehicle vehicle in garage) actual.Add(vehicle);
        Assert.Empty(actual);
    }

    [Fact]
    public void NonGenericGetEnumerator_ParkedVehicles_EnumeratesAllVehicles()
    {
        var garage = MixedGarage();
        var actual = new List<object>();
        foreach (object vehicle in (IEnumerable)garage) actual.Add(vehicle);
        Assert.Equal(garage.GetAllVehicles().Cast<object>(), actual);
    }
}

