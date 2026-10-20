
using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Models;

namespace VyshKat.Tests.Fakes;

public class FakeServiceCenter : IServiceCenter
{
    public bool InspectionResult { get; set; } = true;

    public int InspectionCount { get; private set; }

    public bool Inspect(Transport transport)
    {
        InspectionCount++;
        return InspectionResult;
    }
}
