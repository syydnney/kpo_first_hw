
namespace VyshKat.Domain.Models;

public class Bicycle : Transport
{
    public Bicycle(
        int inventoryNumber,
        int easeOfUse,
        bool isTechnicallySound = true)
        : base(inventoryNumber, "Велосипед", easeOfUse,
            isTechnicallySound)
    {
    }
}
