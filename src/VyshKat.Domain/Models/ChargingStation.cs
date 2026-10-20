
using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models;

public class ChargingStation : InventoryItem, IEnergyConsumer
{
    public double DailyEnergyConsumption { get; }

    public ChargingStation(
        int inventoryNumber,
        double dailyEnergyConsumption)
        : base(inventoryNumber, "Зарядная станция")
    {
        if (double.IsNaN(dailyEnergyConsumption) ||
            double.IsInfinity(dailyEnergyConsumption) ||
            dailyEnergyConsumption < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(dailyEnergyConsumption));
        }

        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}
