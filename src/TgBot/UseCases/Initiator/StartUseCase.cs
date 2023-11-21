using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Keyboard;

namespace TgBot.UseCases.Initiator;

public class StartUseCase : BaseUseCase
{
    public StartUseCase(ITelegramBotClient botClient) : base(botClient)
    {

    }

    public override async Task<Message> Run(Message message, CancellationToken cancellationToken)
    {
        const string text = "Вы стали водителем. Можете использовать следующие функции:\n";

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Мои поездки", "myTrip"),
                        InlineKeyboardButton.WithCallbackData("Создать поездку", "addTrip"),
                    },
                });

        return await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: text,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}
