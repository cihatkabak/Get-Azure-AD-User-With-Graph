using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetADUsersWithGraph
{
    public class GraphUserResponseList
    {
        public GraphUserResponseList()
        {
            Responses = new List<GraphUserResponse>();
        }
        [JsonProperty("responses")]
        public List<GraphUserResponse> Responses { get; set; }
    }
    public class GraphUserResponse
    {
        [JsonProperty("body")]
        public GraphUser Body { get; set; }
    }
   
}
