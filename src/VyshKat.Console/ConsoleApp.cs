
using VyshKat.Domain.Models;
using VyshKat.Domain.Services;

public class ConsoleApp
{
    private readonly FleetService _fleet;

    public ConsoleApp(FleetService fleet)
    {
        _fleet = fleet;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=== ВышКат ===");
            Console.WriteLine("1. Добавить транспорт");
            Console.WriteLine("2. Добавить инвентарь");
            Console.WriteLine("3. Отчёт по парку");
            Console.WriteLine("4. Транспорт для новичков");
            Console.WriteLine("5. Весь инвентарь");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTransport();
                    break;
                case "2":
                    AddInventory();
                    break;
                case "3":
                    ShowReport();
                    break;
                case "4":
                    ShowBeginnerTransport();
                    break;
                case "5":
                    ShowInventory();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }

    private void AddTransport()
    {
        Console.WriteLine("\nВид транспорта:");
        Console.WriteLine("1. Электросамокат");
        Console.WriteLine("2. Велосипед");
        Console.WriteLine("3. Электровелосипед");

        int type = ReadInt("Выберите тип: ", 1, 3);
        int number = ReadInt("Инвентарный номер: ", 1);
        int ease = ReadInt("Простота управления (1-10): ", 1, 10);
        bool isSound = ReadBool("Техника исправна? (да/нет): ");

        Transport transport;

        if (type == 1)
        {
            double energy = ReadDouble("Расход энергии (кВт·ч): ");
            transport = new Scooter(number, ease, energy, isSound);
        }
        else if (type == 2)
        {
            transport = new Bicycle(number, ease, isSound);
        }
        else
        {
            double energy = ReadDouble("Расход энергии (кВт·ч): ");
            transport = new ElectricBicycle(number, ease, energy, isSound);
        }

        bool accepted = _fleet.AddTransport(transport);

        Console.WriteLine(accepted
            ? "Техника прошла техосмотр и добавлена в парк."
            : "Техника не принята: техосмотр не пройден или номер занят.");
    }

    private void AddInventory()
    {
        Console.WriteLine("\nТип инвентаря:");
        Console.WriteLine("1. Шлем");
        Console.WriteLine("2. Зарядная станция");

        int type = ReadInt("Выберите тип: ", 1, 2);
        int number = ReadInt("Инвентарный номер: ", 1);

        InventoryItem item;

        if (type == 1)
        {
            item = new Helmet(number);
        }
        else
        {
            double energy = ReadDouble("Расход энергии (кВт·ч): ");
            item = new ChargingStation(number, energy);
        }

        bool added = _fleet.AddInventoryItem(item);

        Console.WriteLine(added
            ? "Инвентарь добавлен."
            : "Инвентарный номер уже занят.");
    }

    private void ShowReport()
    {
        Console.WriteLine("\n=== Отчёт по парку ===");
        Console.WriteLine($"Транспортных средств: {_fleet.GetTransportCount()}");
        Console.WriteLine($"Всего инвентарных объектов: {_fleet.GetInventoryCount()}");
        Console.WriteLine(
            $"Суточное энергопотребление: " +
            $"{_fleet.GetTotalDailyEnergyConsumption():F2} кВт·ч");
    }

    private void ShowBeginnerTransport()
    {
        var items = _fleet.GetBeginnerTransport();

        Console.WriteLine("\n=== Транспорт для новичков ===");

        if (items.Count == 0)
        {
            Console.WriteLine("Подходящий транспорт отсутствует.");
            return;
        }

        foreach (var item in items)
        {
            Console.WriteLine(
                $"{item.Name}, №{item.InventoryNumber}, " +
                $"простота: {item.EaseOfUse}");
        }
    }

    private void ShowInventory()
    {
        var items = _fleet.GetInventory();

        Console.WriteLine("\n=== Инвентарь ===");

        if (items.Count == 0)
        {
            Console.WriteLine("Парк пуст.");
            return;
        }

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name}, №{item.InventoryNumber}");
        }
    }

    private static int ReadInt(
        string message,
        int min,
        int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value) &&
                value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }

    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (double.TryParse(Console.ReadLine(), out double value) &&
                double.IsFinite(value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Введите неотрицательное число.");
        }
    }

    private static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? answer = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (answer == "да")
            {
                return true;
            }

            if (answer == "нет")
            {
                return false;
            }

            Console.WriteLine("Введите 'да' или 'нет'.");
        }
    }
}
