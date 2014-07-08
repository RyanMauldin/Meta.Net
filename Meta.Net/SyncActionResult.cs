namespace Meta.Net
{
    public class SyncActionResult
    {
        private string Message { get; set; }
        private SyncActionResultType ResultType { get; set; }

        public SyncActionResult(string message, SyncActionResultType resultType)
        {
            Message = message;
            ResultType = resultType;
        }
    }
}
