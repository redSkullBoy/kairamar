using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Keyboard;

public class ToChooseUseCase : BaseUseCase
{
    public ToChooseUseCase(ITelegramBotClient botClient) : base(botClient)
    {

    }

    public override async Task<Message> Run(Message message, CancellationToken cancellationToken)
    {
        await _botClient.SendChatActionAsync(
                            chatId: message.Chat.Id,
                            chatAction: ChatAction.Typing,
                            cancellationToken: cancellationToken);

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Водитель", "userIsInitiator"),
                        InlineKeyboardButton.WithCallbackData("Попутчик", "userIsCompanion"),
                    },
                });

        return await _botClient.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: "Выберите себя",
                                replyMarkup: inlineKeyboard,
                                cancellationToken: cancellationToken);
    }
}
