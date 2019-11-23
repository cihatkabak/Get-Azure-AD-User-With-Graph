using Newtonsoft.Json;

namespace GetADUsersWithGraph
{
    public class GraphUsersId
    {
        [JsonProperty("id")]
        public GraphUserList Id { get; set; }
    }
}
