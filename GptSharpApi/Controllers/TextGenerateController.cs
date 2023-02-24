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

using GptSharp.Applications.Dto;
using GptSharp.Applications.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GptSharp.Controllers;

public class TextGenerateController : Controller
{
    private readonly ILogger<TextGenerateController> _logger;
    private readonly IOpenAiapi _openAiapi;
    private readonly ITelegramBotClient _telegramBotClient;
    
    public TextGenerateController(ILogger<TextGenerateController> logger, IOpenAiapi openAiapi, ITelegramBotClient telegramBotClient)
    {
        _logger = logger;
        _openAiapi = openAiapi;
        _telegramBotClient = telegramBotClient;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateText([FromBody] Update update)
    {
        _logger.LogInformation("Incoming update: {@Update}", update);

        var chatId = update.Message!.Chat.Id;
        var prompt = update.Message.Text;
        var response = await _openAiapi.GenerateText(new OpenAIAPIRequest { Prompt = prompt! });

        var message = response;
        if (string.IsNullOrWhiteSpace(message))
        {
            message = "Unable to generate text.";
        }

        await _telegramBotClient.SendTextMessageAsync(chatId, message);

        return Ok();
    }
}