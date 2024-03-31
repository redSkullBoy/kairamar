using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Receiveds;
using TgBot.Filters;

namespace TgBot.Controllers;

public class BotController(ILogger<BotController> logger) : ControllerBase
{
    [HttpPost]
    [ValidateTelegramBot]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] ReceivedContext receivedContext,
        CancellationToken cancellationToken)
    {
        try
        {
            await receivedContext.HandleAsync(update, cancellationToken);
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "Ошибка");
        }

        return Ok();
    }
}