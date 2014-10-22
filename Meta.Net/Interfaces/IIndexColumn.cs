namespace Meta.Net.Interfaces
{
    public interface IIndexColumn : IUserTable
    {
        bool IsDescendingKey { get; set; }
        bool IsIncludedColumn { get; set; }
        int KeyOrdinal { get; set; }
        int PartitionOrdinal { get; set; }
    }
}