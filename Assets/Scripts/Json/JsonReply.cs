using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReply
{
    [JsonProperty("id")]
    public long id { get; set; }
    [JsonProperty("Action")]
    public string action { get; set; }
    [JsonProperty("Params")]
    public List<string> parameters { get; set; }

    public JsonReply(long id, string action, List<string> parameters)
    {
        this.id = id;
        this.action = action;
        this.parameters = parameters;
    }
}
