
using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Models;

namespace VyshKat.Domain.Services;

public class ServiceCenter : IServiceCenter
{
    public bool Inspect(Transport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);

        return transport.IsTechnicallySound;
    }
}
