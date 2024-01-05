using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Receiveds;
using TgBot.Filters;

namespace TgBot.Controllers;

public class BotController : ControllerBase
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
        catch (Exception ex) { }

        return Ok();
    }
}