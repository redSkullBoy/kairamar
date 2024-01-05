using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.Extensions;

namespace TgBot.Endpoints;

public class StartEndpoint : MessageEndpoint
{
    public StartEndpoint()
    {
    }

    public override void Configure()
    {
        Routes("/start");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        var unauthorizedUser = message.From ?? new User();
        //var chatUser = await BotClient.GetChatMemberAsync(message.Chat.Id, unauthorizedUser.Id);

        //if (chatUser == null || chatUser.Status == ChatMemberStatus.Left)
        //    return;

        var prettyUserName = unauthorizedUser.GetPrettyName();

        await BotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Привет, {prettyUserName}!!!",
                        cancellationToken: cancellationToken);

        var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Водитель", "userIsInitiator"),
                        InlineKeyboardButton.WithCallbackData("Попутчик", "userIsCompanion"),
                    },
                });

        await BotClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Выберите роль",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
    }
}
