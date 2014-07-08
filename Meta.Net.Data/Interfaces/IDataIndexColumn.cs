namespace Meta.Net.Data.Interfaces
{
    public interface IDataIndexColumn
    {
        bool IsDescendingKey { get; set; }

        bool IsIncludedColumn { get; set; }

        int KeyOrdinal { get; set; }

        int PartitionOrdinal { get; set; }
    }
}