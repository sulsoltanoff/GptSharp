#region Corpspace© Apache-2.0
// Copyright © 2023 Sultan Soltanov. All rights reserved.
// Author: Sultan Soltanov
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using Newtonsoft.Json;

namespace GptSharp.Applications.Dto;

public class OpenAIAPIRequest
{
    [JsonProperty("prompt")]
    public string Prompt { get; set; }

    [JsonProperty("temperature")]
    public double Temperature { get; set; } = 0.5;

    [JsonProperty("max_tokens")]
    public int MaxTokens { get; set; } = 128;

    [JsonProperty("top_p")]
    public double TopP { get; set; } = 1;

    [JsonProperty("frequency_penalty")]
    public double FrequencyPenalty { get; set; } = 0;

    [JsonProperty("presence_penalty")]
    public double PresencePenalty { get; set; } = 0;

    [JsonProperty("stop")]
    public string[] Stop { get; set; } = new string[] { "\n", "===" };
}