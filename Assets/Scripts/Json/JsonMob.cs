using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonMob
{
    [JsonProperty("id")]
    public long id { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("health")]
    public float health { get; set; }
    [JsonProperty("x")]
    public float x { get; set; }
    [JsonProperty("y")]
    public float y { get; set; }
}
