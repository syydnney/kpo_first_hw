using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models;

public abstract class InventoryItem : IInventoryItem
{
    public int InventoryNumber { get; }

    public string Name { get; }

    protected InventoryItem(int inventoryNumber, string name)
    {
        if (inventoryNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(inventoryNumber),
                "Инвентарный номер должен быть положительным.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Название не может быть пустым.",
                nameof(name));
        }

        InventoryNumber = inventoryNumber;
        Name = name;
    }
}
