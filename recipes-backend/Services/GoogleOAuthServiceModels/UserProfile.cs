using Newtonsoft.Json;

namespace recipes_backend.Services.GoogleOAuthServiceModels
{
    public class UserProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Mail { get; set; }
    }
}