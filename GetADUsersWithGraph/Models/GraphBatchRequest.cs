using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetADUsersWithGraph
{
    public class GraphBatchRequestList
    {
        public GraphBatchRequestList()
        {
            Requests = new List<GraphBatchRequest>();
        }
        [JsonProperty("requests")]
        public List<GraphBatchRequest> Requests { get; set; }
    }
    public class GraphBatchRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
