using Domain.Entities;
using GeoTimeZone;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;
using UseCases.Handlers.Addresses.Queries;
using UseCases.Handlers.Trips.Dto;

namespace TgBot.Endpoints.Trips.FindProcess;

public class FromAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public FromAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
        IMediator mediator, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _mediator = mediator;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State(UserStates.TripFindFromAddress);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        if (!int.TryParse(callbackQuery.Data, out int addressId))
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Ошибка при получение адреса",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var result = await _mediator.Send(new GetByIdRequest { Id = addressId }, cancellationToken);

        if (!result.IsSuccess)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Ошибка при получение адреса",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        double lat = 0;
        double lon = 0;

        double.TryParse(result.Value.GeoLat, System.Globalization.CultureInfo.InvariantCulture, out lat);
        double.TryParse(result.Value.GeoLon, System.Globalization.CultureInfo.InvariantCulture, out lon);

        var user = await _userManager.FindByNameAsync(callbackQuery.From.Username!);

        if (lat != 0 && lon != 0 && user != null)
        {
            string timeZoneId = TimeZoneLookup.GetTimeZone(lat, lon).Result;

            user.TimeZoneId = timeZoneId;
            await _userManager.UpdateAsync(user);
        }

        var filter = new TripFilter
        {
            FromAddressId = addressId,
            FromAddress = result.Value.Value,
        };

        _cache.SetTripFilter(callbackQuery.From!.Id, filter, TimeSpan.FromMinutes(5));

        string info = $"""
                            Для поиска поездки укажите следующую информацию:
                            - Пункт отправления {result.Value.Value}
                            - Пункт назначения
                            - Дату и время отправления

                            Введите - Пункт назначения
                            """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
