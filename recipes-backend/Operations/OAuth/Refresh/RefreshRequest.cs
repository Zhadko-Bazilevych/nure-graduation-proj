namespace recipes_backend.Operations.OAuth.Refresh
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
        public string Device { get; set; }
    }
}
