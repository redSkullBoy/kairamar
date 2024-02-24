using Dadata.Model;
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
using UseCases.Handlers.Addresses.Commands;
using UseCases.Handlers.Addresses.Queries;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddFromAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly UserManager<AppUser> _userManager; 
    private readonly IMediator _mediator;

    public AddFromAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
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
        State(UserStates.TripAddFromAddress);
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

        var trip = new Trip
        {
            FromAddressId = addressId
        };

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        string info = $"""
                    - Пункт отправления: {result.Value.Note}
                    - Пункт назначения
                    - Дату и время отправления
                    - Количество свободных мест
                    - Стоимость поездки
                    """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Пункт назначения",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
