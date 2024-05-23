using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using GeoTimeZone;

namespace TgBot.Endpoints;

public class SetLocationEndpoint : MessageEndpoint
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITelegramBotClient _botClient;

    public SetLocationEndpoint(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITelegramBotClient botClient)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _botClient = botClient;
    }

    public override void Configure()
    {
        SetMessageType(MessageType.Location);
    }

    public static async Task PreparationAsync(ITelegramBotClient preBotClient, long chatId, CancellationToken cancellationToken)
    {
        var keyboardBtn = KeyboardButton.WithRequestLocation("Поделиться геолокацией");
        var keyboard = new ReplyKeyboardMarkup(keyboardBtn);

        await preBotClient.SendTextMessageAsync(chatId: chatId, 
            text: "Пожалуйста, поделитесь геолокацией для лучшей работы бота.", 
            replyMarkup: keyboard, 
            cancellationToken: cancellationToken);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        if (message?.Type == MessageType.Location)
        {
            var username = message.From?.Username!;

            // Создайте нового пользователя без пароля, если у нас еще нет такого пользователя
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                string timeZoneId = TimeZoneLookup.GetTimeZone(message.Location!.Latitude, message.Location!.Longitude).Result;

                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

                //user.PhoneNumber = message.Contact?.PhoneNumber;
                //await _userManager.UpdateAsync(user);

                await _botClient.SendTextMessageAsync(
                    chatId: message!.Chat.Id,
                    text: "Таймзон получен: " + timeZoneId,
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            }
        }

        return;
    }
}