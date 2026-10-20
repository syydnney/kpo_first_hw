
using VyshKat.Domain.Models;

namespace VyshKat.Domain.Interfaces;

public interface IServiceCenter
{
    bool Inspect(Transport transport);
}
