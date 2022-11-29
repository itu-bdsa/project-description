namespace GitInsight.Entities;
using Newtonsoft.Json;
// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);


[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Owner
    {
        [JsonProperty("login")]
        public string login { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

    }


[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Fork
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        #nullable enable
        public string ?name { get; set; }

        [JsonProperty("owner")]
        public Owner ?owner { get; set; }

        [JsonProperty("html_url")]
        public string ?html_url { get; set; }   
        
    }

