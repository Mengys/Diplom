using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonProjectile
{
    [JsonProperty("id")]
    public long id { get; set; }
    [JsonProperty("x")]
    public float x { get; set; }
    [JsonProperty("y")]
    public float y { get; set; }
    [JsonProperty("directionX")]
    public float dirX { get; set; }
    [JsonProperty("directionY")]
    public float dirY { get; set; }
}
