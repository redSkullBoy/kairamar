using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Extensions;
using TgBot.Keyboard;

namespace TgBot.UseCases;

public class LoginUseCase : BaseUseCase
{
    public LoginUseCase(ITelegramBotClient botClient) : base(botClient)
    {

    }

    public override async Task<Message> Run(Message message, CancellationToken cancellationToken)
    {
        var prettyUserName = string.Empty;

        foreach (var unauthorizedUser in message.NewChatMembers ?? Array.Empty<User>())
        {
            var chatUser = await _botClient.GetChatMemberAsync(message.Chat.Id, unauthorizedUser.Id);

            //if (chatUser == null || chatUser.Status == ChatMemberStatus.Left)
            //    return;

            prettyUserName = unauthorizedUser.GetPrettyName();

        }

        return await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Привет, {prettyUserName}!!!",
                        cancellationToken: cancellationToken);
    }
}
