using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GetADUsersWithGraph
{
    public class GraphUser
    {
        [JsonProperty("mail")]
        public string Mail { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("givenName")]
        public string GivenName { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("businessPhones")]
        public string[] BusinessPhones { get; set; }
        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }
        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }
        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty("hireDate")]
        public DateTime? HireDate { get; set; }
        [JsonProperty("birthday")]
        public DateTime? BirthDay { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }
        [JsonProperty("manager")]
        public GraphUser Manager { get; set; }
        [RestrictUpdate]
        [JsonProperty("userType")]
        public string UserType { get; set; }
    }
    public class GraphUserList
    {
        [JsonProperty("value")]
        public List<GraphUser> Value { get; set; }
    }
    
}