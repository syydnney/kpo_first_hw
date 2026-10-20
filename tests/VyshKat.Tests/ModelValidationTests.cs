
using VyshKat.Domain.Models;

namespace VyshKat.Tests;

public class ModelValidationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void InventoryItem_RejectsInvalidInventoryNumber(int number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Helmet(number));
    }

    [Fact]
    public void InventoryItem_RejectsEmptyName()
    {
        Assert.Throws<ArgumentException>(
            () => new TestItem(1, ""));
    }

    [Fact]
    public void InventoryItem_RejectsWhitespaceName()
    {
        Assert.Throws<ArgumentException>(
            () => new TestItem(1, "   "));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void Transport_RejectsInvalidEaseOfUse(int ease)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Bicycle(1, ease));
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Scooter_RejectsInvalidEnergy(double energy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Scooter(1, 8, energy));
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void ElectricBicycle_RejectsInvalidEnergy(double energy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ElectricBicycle(1, 8, energy));
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void ChargingStation_RejectsInvalidEnergy(double energy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ChargingStation(1, energy));
    }

    [Theory]
    [InlineData(5, false)]
    [InlineData(6, true)]
    [InlineData(10, true)]
    public void Transport_ChecksBeginnerSuitability(
        int ease,
        bool expected)
    {
        var bicycle = new Bicycle(1, ease);

        Assert.Equal(expected, bicycle.IsSuitableForBeginners);
    }

    private sealed class TestItem : InventoryItem
    {
        public TestItem(int number, string name)
            : base(number, name)
        {
        }
    }
}
