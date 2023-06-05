using recipes_backend.Common;

namespace recipes_backend.Operations.OAuth.GetUserData
{
    public class GetUserDataResponse : BaseResponse
    {
        public int? Id { get; set; }
        public string? Mail { get; set; }
        public string? Name { get; set; }
    }
}
