namespace KvizHub.Models.Base
{
    public abstract class ResponseBase
    {
        public int ResponseId { get; set; }
        public string? Content { get; set; }
        public bool Correct { get; set; }
    }
}
