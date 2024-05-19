using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddEndDateEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly UserManager<AppUser> _userManager;

    public AddEndDateEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State(UserStates.TripAddEndDate);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        var timePattern = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

        string digitsOnly = new string(message.Text!.Where(char.IsDigit).ToArray());

        var formattedTime = string.Empty;

        if (digitsOnly.Length == 1)
        {
            formattedTime = $"0{digitsOnly.Substring(0, 1)}:00";
        }
        else if (digitsOnly.Length == 2)
        {
            formattedTime = $"{digitsOnly.Substring(0, 2)}:00";
        }
        else if (digitsOnly.Length == 3)
        {
            formattedTime = $"0{digitsOnly.Substring(0, 1)}:{digitsOnly.Substring(1, 2)}";
        }
        else if (digitsOnly.Length >= 4)
        {
            formattedTime = $"{digitsOnly.Substring(0, 2)}:{digitsOnly.Substring(2, 2)}";
        }
        else
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Неверный формат времени. Введите время в формате: 1 60",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        if (!Regex.IsMatch(formattedTime, timePattern))
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Время введено неправильно!!!",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var timeOnly = TimeOnly.Parse(formattedTime);
        var requiresTime = new TimeSpan(0, 30, 0);
        //проверка что время больше 30 минут
        if (DateTime.Now.TimeOfDay - timeOnly.ToTimeSpan() <= requiresTime)
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Время должно быть не меньше 30 минут",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var trip = _cache.GetTripOrNull(message.From!.Id)!;

        var dateOnly = new DateOnly(trip.StartDateLocal.Year, trip.StartDateLocal.Month, trip.StartDateLocal.Day);
        trip.EndDateLocal = dateOnly.ToDateTime(timeOnly);

        _cache.SetTrip(message.From!.Id, trip, TimeSpan.FromMinutes(5));

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "1"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2", "2"),
                        InlineKeyboardButton.WithCallbackData("3", "3"),
                        InlineKeyboardButton.WithCallbackData("4", "4"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("5", "5"),
                        InlineKeyboardButton.WithCallbackData("6", "6"),
                        InlineKeyboardButton.WithCallbackData("7", "7"),
                    },
                });

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: "Выберите - Количество свободных мест",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);

        _userBotService.NetxState(message.From!.Id);
    }
}
