﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Frontend.Client.Responses
{
    internal class ScriptResponse
    {
        [JsonProperty] public List<ActionResponse> Actions { get; set; } = new List<ActionResponse>();

        [JsonProperty] public bool IsSuccess { get; set; } = true;
    }
}