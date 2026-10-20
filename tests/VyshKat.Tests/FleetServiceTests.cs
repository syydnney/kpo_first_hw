
using VyshKat.Domain.Models;
using VyshKat.Domain.Services;
using VyshKat.Tests.Fakes;

namespace VyshKat.Tests;

public class FleetServiceTests
{
    [Fact]
    public void AddTransport_AcceptsTransport_WhenInspectionPasses()
    {
        var fake = new FakeServiceCenter();
        var fleet = new FleetService(fake);
        var scooter = new Scooter(1, 8, 2.5);

        bool result = fleet.AddTransport(scooter);

        Assert.True(result);
        Assert.Equal(1, fleet.GetTransportCount());
        Assert.Equal(1, fake.InspectionCount);
    }

    [Fact]
    public void AddTransport_RejectsTransport_WhenInspectionFails()
    {
        var fake = new FakeServiceCenter
        {
            InspectionResult = false
        };

        var fleet = new FleetService(fake);
        var scooter = new Scooter(1, 8, 2.5);

        bool result = fleet.AddTransport(scooter);

        Assert.False(result);
        Assert.Equal(0, fleet.GetTransportCount());
        Assert.Empty(fleet.GetInventory());
        Assert.Equal(1, fake.InspectionCount);
    }

    [Fact]
    public void AddTransport_RejectsDuplicateInventoryNumber()
    {
        var fake = new FakeServiceCenter();
        var fleet = new FleetService(fake);

        fleet.AddTransport(new Scooter(1, 8, 2.5));

        bool result = fleet.AddTransport(new Bicycle(1, 7));

        Assert.False(result);
        Assert.Equal(1, fleet.GetTransportCount());
        Assert.Equal(1, fake.InspectionCount);
    }

    [Fact]
    public void AddInventoryItem_AddsHelmet()
    {
        var fleet = new FleetService(new FakeServiceCenter());

        bool result = fleet.AddInventoryItem(new Helmet(10));

        Assert.True(result);
        Assert.Equal(1, fleet.GetInventoryCount());
        Assert.Equal(0, fleet.GetTransportCount());
    }

    [Fact]
    public void AddInventoryItem_RejectsDuplicateNumber()
    {
        var fleet = new FleetService(new FakeServiceCenter());

        fleet.AddInventoryItem(new Helmet(10));

        bool result = fleet.AddInventoryItem(new ChargingStation(10, 3.0));

        Assert.False(result);
        Assert.Equal(1, fleet.GetInventoryCount());
    }

    [Fact]
    public void GetTotalDailyEnergyConsumption_SumsAllConsumers()
    {
        var fleet = new FleetService(new FakeServiceCenter());

        fleet.AddTransport(new Scooter(1, 8, 2.5));
        fleet.AddTransport(new ElectricBicycle(2, 7, 1.5));
        fleet.AddTransport(new Bicycle(3, 9));

        fleet.AddInventoryItem(new ChargingStation(4, 3.0));
        fleet.AddInventoryItem(new Helmet(5));

        double total = fleet.GetTotalDailyEnergyConsumption();

        Assert.Equal(7.0, total, 6);
    }

    [Fact]
    public void GetBeginnerTransport_ReturnsOnlySuitableTransport()
    {
        var fleet = new FleetService(new FakeServiceCenter());

        fleet.AddTransport(new Scooter(1, 5, 2.5));
        fleet.AddTransport(new Bicycle(2, 6));
        fleet.AddTransport(new ElectricBicycle(3, 9, 1.5));

        var result = fleet.GetBeginnerTransport();

        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, item => item.InventoryNumber == 1);
        Assert.Contains(result, item => item.InventoryNumber == 2);
        Assert.Contains(result, item => item.InventoryNumber == 3);
    }

    [Fact]
    public void AddInventoryItem_RejectsTransportWithoutInspection()
    {
        var fake = new FakeServiceCenter();
        var fleet = new FleetService(fake);

        bool result = fleet.AddInventoryItem(new Scooter(1, 8, 2.5));

        Assert.False(result);
        Assert.Equal(0, fake.InspectionCount);
        Assert.Empty(fleet.GetInventory());
    }
    
    [Fact]
    public void RealServiceCenter_RejectsBrokenTransport()
    {
        var fleet = new FleetService(new ServiceCenter());
        var scooter = new Scooter(1, 8, 2.5, false);

        bool result = fleet.AddTransport(scooter);

        Assert.False(result);
        Assert.Equal(0, fleet.GetTransportCount());
    }

    [Fact]
    public void RealServiceCenter_AcceptsWorkingTransport()
    {
        var fleet = new FleetService(new ServiceCenter());
        var scooter = new Scooter(1, 8, 2.5, true);

        bool result = fleet.AddTransport(scooter);

        Assert.True(result);
        Assert.Equal(1, fleet.GetTransportCount());
    }

}
