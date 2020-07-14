using Newtonsoft.Json;

namespace SmartPillowAuthLib.OAuth2.FacebookOAuth
{
    public class FacebookProfile
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public Picture Picture { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
    }
}
