
namespace VyshKat.Domain.Models;

public abstract class Transport : InventoryItem
{
    public int EaseOfUse { get; }

    public bool IsSuitableForBeginners => EaseOfUse >= 6;

    public bool IsTechnicallySound { get; }

    protected Transport(
        int inventoryNumber,
        string name,
        int easeOfUse,
        bool isTechnicallySound = true)
        : base(inventoryNumber, name)
    {
        if (easeOfUse < 1 || easeOfUse > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(easeOfUse),
                "Простота управления должна быть от 1 до 10.");
        }

        EaseOfUse = easeOfUse;
        IsTechnicallySound = isTechnicallySound;
    }
}
