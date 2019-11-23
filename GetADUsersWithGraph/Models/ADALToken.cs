using Newtonsoft.Json;

namespace GetADUsersWithGraph
{
    public class ADALToken
    {
        public ADALToken() { }
        public ADALToken(ADALToken token)
        {
            Scope = token.Scope;
            ExpiresIn = token.ExpiresIn;
            ExtExpiresIn = token.ExtExpiresIn;
            ExpiresOn = token.ExpiresOn;
            NotBefore = token.NotBefore;
            Resource = token.Resource;
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
            IdToken = token.IdToken;
        }
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "ext_expires_in")]
        public int ExtExpiresIn { get; set; }
        [JsonProperty(PropertyName = "expires_on")]
        public long ExpiresOn { get; set; }
        [JsonProperty(PropertyName = "not_before")]
        public long NotBefore { get; set; }
        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}