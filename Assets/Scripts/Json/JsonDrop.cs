using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonDrop
{
    [JsonProperty("id")]
    public long id { get; set; }
    [JsonProperty("item_id")]
    public long itemId { get; set; }
    [JsonProperty("x")]
    public float x { get; set; }
    [JsonProperty("y")]
    public float y { get; set; }
}
