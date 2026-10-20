
using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models;

public class ElectricBicycle : Transport, IEnergyConsumer
{
    public double DailyEnergyConsumption { get; }

    public ElectricBicycle(
        int inventoryNumber,
        int easeOfUse,
        double dailyEnergyConsumption,
        bool isTechnicallySound = true)
        : base(inventoryNumber, "Электровелосипед",
            easeOfUse, isTechnicallySound)
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
