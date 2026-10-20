
using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models;

public class Scooter : Transport, IEnergyConsumer
{
    public double DailyEnergyConsumption { get; }

    public Scooter(
        int inventoryNumber,
        int easeOfUse,
        double dailyEnergyConsumption,
        bool isTechnicallySound = true)
        : base(inventoryNumber, "Электросамокат",
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
