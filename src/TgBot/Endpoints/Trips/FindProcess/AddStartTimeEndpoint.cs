using Domain.Entities;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.FindProcess;

public class StartTimeEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly UserManager<AppUser> _userManager;
    private readonly IDateTime _dateTime;

    public StartTimeEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
        UserManager<AppUser> userManager, IDateTime dateTime)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
        _dateTime = dateTime;
    }

    public override void Configure()
    {
        State(UserStates.TripFindStartTime);
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
                text: "Неверный формат времени. Введите время в формате: 16 20",
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

        var user = await _userManager.FindByNameAsync(message.From!.Username!);

        var filter = _cache.GetTripFilterOrNull(message.From!.Id)!;

        var timeOnly = TimeOnly.Parse(formattedTime);

        var oneHourLater = _dateTime.TimeZoneNow(user!.TimeZoneId).AddHours(1);
        var inputDateTime = new DateOnly(oneHourLater.Year, oneHourLater.Month, oneHourLater.Day).ToDateTime(timeOnly);
        //проверка что время больше 1 часа
        if (inputDateTime <= oneHourLater
            && filter.StartDateLocal.Day == _dateTime.TimeZoneNow(user!.TimeZoneId).Day)
        {
            await _botClient.SendTextMessageAsync(
                chatId: message!.Chat.Id,
                text: "Время должно быть на час больше",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var dateOnly = new DateOnly(filter.StartDateLocal.Year, filter.StartDateLocal.Month, filter.StartDateLocal.Day);
        filter.StartDateLocal = dateOnly.ToDateTime(timeOnly);

        _cache.SetTripFilter(message.From!.Id, filter, TimeSpan.FromMinutes(5));

        if (user != null)
        {
            user.LastAddressId = filter.ToAddressId;
            await _userManager.UpdateAsync(user);
        }

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Найти", "findList"),
                    },
                });

        string info = $"""
                            Данные поиска:
                            - Пункт отправления {filter.FromAddress}
                            - Пункт назначения {filter.ToAddress}
                            - Дату и время отправления {_dateTime.ToRussianString(filter.StartDateLocal)}
                            """;

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: info,
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);

        _userBotService.NetxState(message.From!.Id);
    }
}
