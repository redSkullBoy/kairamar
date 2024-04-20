using DataAccess.Sqlite;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddPriceEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly UserManager<AppUser> _userManager;

    public AddPriceEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State(UserStates.TripAddPrice);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        string digitsOnly = new string(message.Text!.Where(char.IsDigit).ToArray());

        if (!int.TryParse(digitsOnly, out var price))
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Ошибка!!!",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        if (price < 100)
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Не меньше 100",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        if (price > 3000)
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Не больше 3 000",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var trip = _cache.GetTripOrNull(message.From!.Id)!;

        trip.Price = price;

        _cache.SetTrip(message.From!.Id, trip, TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: "Введите - Описание",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(message.From!.Id);
    }
}
