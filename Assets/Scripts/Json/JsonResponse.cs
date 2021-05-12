using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonResponse
{
    [JsonProperty("Id")]
    public long id { get; set; }
    [JsonProperty("PacketAmount")]
    public int packetAmount { get; set; }
    [JsonProperty("PacketNumber")]
    public int packetNumber { get; set; }
    [JsonProperty("Data")]
    public string data { get; set; }
}
