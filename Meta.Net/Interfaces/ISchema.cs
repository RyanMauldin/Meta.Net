using Meta.Net.Objects;

namespace Meta.Net.Interfaces
{
    public interface ISchema : ICatalog
    {
        Schema Schema { get; }
    }
}