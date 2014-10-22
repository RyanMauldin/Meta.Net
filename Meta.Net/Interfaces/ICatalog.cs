using Meta.Net.Objects;

namespace Meta.Net.Interfaces
{
    public interface ICatalog : IServer
    {
        Catalog Catalog { get; }
    }
}