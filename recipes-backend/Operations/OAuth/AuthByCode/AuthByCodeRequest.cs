namespace recipes_backend.Operations.OAuth.AuthByCode
{
    public class AuthByCodeRequest
    {
        public string Device { get; set; }
        public string Code { get; set; }
    }
}
