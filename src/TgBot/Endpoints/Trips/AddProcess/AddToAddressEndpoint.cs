using Ardalis.Result;
using DataAccess.Sqlite;
using Domain.Entities.Model;
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
using UseCases.Handlers.Trips.Queries;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddToAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public AddToAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
        UserManager<AppUser> userManager, IMediator mediator)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
        _mediator = mediator;
    }

    public override void Configure()
    {
        State(UserStates.TripAddToAddress);
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

        var trip = _cache.GetTripOrNull(callbackQuery.From!.Id) ?? new CreateTripDto();

        var result = await _mediator.Send(new CoincidSettlementRequest { FromAddressId = trip.FromAddressId, ToAddressId = addressId }, cancellationToken);

        if (!result.IsSuccess)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Совпадает с адресом назначения!!!\nВведите - Пункт назначения: город, деревня",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            _userBotService.PreviousState(callbackQuery.From!.Id);

            return;
        }

        trip.ToAddressId = addressId;

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Сегодня", "today"),
                        InlineKeyboardButton.WithCallbackData("Завтра", "tomorrow"),
                        InlineKeyboardButton.WithCallbackData("Послезавтра", "afterTomorrow"),
                    },
                });

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Выберите дату начала",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
