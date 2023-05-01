using recipes_backend.Common;

namespace recipes_backend.Operations.OAuth.Refresh
{
    public class RefreshResponse : BaseResponse
    {
        public string? Id { get; set; }
        public string? Mail { get; set; }
        public string? Name { get; set; }
        public string? AccessToken { get; set; } = null;
        public string? RefreshToken { get; set; } = null;
    }
}
