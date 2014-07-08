namespace Meta.Net.Data.SqlServer.SqlServerModuleParser
{
    public enum SqlServerModuleType
    {
        Unknown = 0
      , AggregateFunction = 1
      , ClrScalarFunction = 2
      , ClrStoredProcedure = 3
      , ClrTableValuedFunction = 4
      , ClrTrigger = 5
      , SqlInlineTableValuedFunction = 6
      , SqlScalarFunction = 7
      , SqlStoredProcedure = 8
      , SqlTableValuedFunction = 9
      , SqlTrigger = 10
      , View = 11
    }
}
