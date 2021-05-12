using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonData
{
    [JsonProperty("Players")]
    public List<JsonPlayer> players { get; set; }
    [JsonProperty("Mobs")]
    public List<JsonMob> mobs { get; set; }
    [JsonProperty("Projectiles")]
    public List<JsonProjectile> projectiles { get; set; }
    [JsonProperty("Inventory")]
    public List<JsonInventoryItem> inventoryItem { get; set; }
    [JsonProperty("Drops")]
    public List<JsonDrop> drops { get; set; }
}
