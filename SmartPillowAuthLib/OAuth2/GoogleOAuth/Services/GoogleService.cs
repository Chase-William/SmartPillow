using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartPillowAuthLib.OAuth2.GoogleOAuth.Services
{
    public class GoogleService
    {
        public async Task<GoogleAccountInfo> GetAccountInfo(GoogleOAuthToken token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
            var json = await httpClient.GetStringAsync("https://openidconnect.googleapis.com/v1/userinfo");
            var info = JsonConvert.DeserializeObject<GoogleAccountInfo>(json);
            return info;
        }
    }
}
