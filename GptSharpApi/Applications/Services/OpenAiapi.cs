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

using System.Net.Http.Headers;
using System.Text;
using GptSharp.Applications.Dto;
using Newtonsoft.Json;

namespace GptSharp.Applications.Services;

public class OpenAiapi : IOpenAiapi
{
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _engine;

    public OpenAiapi(HttpClient client, string apiKey, string engine)
    {
        _client = client;
        _apiKey = apiKey;
        _engine = engine;
    }

    public async Task<string> GenerateText(OpenAIAPIRequest dtoReq)
    {
        var response = await SendRequest(dtoReq);
        return response.Choices.FirstOrDefault()?.Text!;
    }

    private async Task<OpenAIAPIResponse> SendRequest(OpenAIAPIRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        var response = await _client.PostAsync($"https://api.openai.com/v1/engines/{_engine}/completions", content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OpenAIAPIResponse>(responseContent)!;
    }
}