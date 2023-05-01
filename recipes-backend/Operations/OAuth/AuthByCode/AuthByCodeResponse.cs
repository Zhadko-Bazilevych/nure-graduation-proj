using recipes_backend.Common;

namespace recipes_backend.Operations.OAuth.AuthByCode
{
    public class AuthByCodeResponse : BaseResponse
    {
        public string? Mail { get; set; }
        public string? Name { get; set; }
        public string? AccessToken { get; set; } = null;
        public string? RefreshToken { get; set; } = null;
    }
}
