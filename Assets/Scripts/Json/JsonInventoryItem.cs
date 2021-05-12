using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonInventoryItem
{
    [JsonProperty("place")]
    public int position { get; set; }
    [JsonProperty("item_id")]
    public long itemId { get; set; }
    [JsonProperty("Amount")]
    public int amount { get; set; }
}
