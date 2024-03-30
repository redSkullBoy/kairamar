using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;
using TgBot.Templates;

namespace TgBot.Endpoints.Default;

public class DefaultCallbackQueryEndpoint : CallbackQueryEndpoint
{
    private readonly ITelegramBotClient _botClient;

    public DefaultCallbackQueryEndpoint(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task DefaultHandleAsync(CallbackQuery callbackQuery, string userState, CancellationToken cancellationToken)
    {
        //Поиск имени кнопки
        string buttonText = string.Empty;
        var inlineKeyboards = callbackQuery.Message?.ReplyMarkup?.InlineKeyboard;

        foreach(var inlineKeyboard in inlineKeyboards)
        {
            var button = inlineKeyboard.FirstOrDefault(s => s.CallbackData == callbackQuery.Data);

            if (button != null)
            {
                buttonText = button.Text;
                break;
            }
        }

        string text = $"{buttonText} - команда не найдена";

        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text,
            replyMarkup: InitiatorsTemplates.Menu(),
            cancellationToken: cancellationToken);
    }
}
