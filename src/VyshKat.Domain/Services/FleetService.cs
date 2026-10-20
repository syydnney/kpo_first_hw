
using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Models;

namespace VyshKat.Domain.Services;

public class FleetService
{
    private readonly IServiceCenter _serviceCenter;

    private readonly List<Transport> _transport = new();
    private readonly List<IInventoryItem> _inventory = new();

    public FleetService(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter
            ?? throw new ArgumentNullException(nameof(serviceCenter));
    }

    public bool AddTransport(Transport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);

        if (HasInventoryNumber(transport.InventoryNumber))
        {
            return false;
        }

        if (!_serviceCenter.Inspect(transport))
        {
            return false;
        }

        _transport.Add(transport);
        _inventory.Add(transport);

        return true;
    }

    public bool AddInventoryItem(IInventoryItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (item is Transport)
        {
            return false;
        }

        if (HasInventoryNumber(item.InventoryNumber))
        {
            return false;
        }

        _inventory.Add(item);
        return true;
    }

    public int GetTransportCount()
    {
        return _transport.Count;
    }

    public int GetInventoryCount()
    {
        return _inventory.Count;
    }

    public double GetTotalDailyEnergyConsumption()
    {
        return _inventory
            .OfType<IEnergyConsumer>()
            .Sum(item => item.DailyEnergyConsumption);
    }

    public IReadOnlyList<Transport> GetBeginnerTransport()
    {
        return _transport
            .Where(item => item.IsSuitableForBeginners)
            .ToList();
    }

    public IReadOnlyList<IInventoryItem> GetInventory()
    {
        return _inventory.AsReadOnly();
    }

    private bool HasInventoryNumber(int inventoryNumber)
    {
        return _inventory.Any(
            item => item.InventoryNumber == inventoryNumber);
    }
}
