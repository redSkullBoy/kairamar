using DataAccess.Sqlite;
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

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddFromAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public AddFromAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
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
        State(UserStates.TripAddFromAddress);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var username = callbackQuery.From.Username;

        var user = await _userManager.FindByNameAsync(username!);

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

        var trip = new CreateTripDto
        {
            FromAddressId = addressId,
            InitiatorId = user!.Id
        };

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        //await _botClient.SendInitiatorMenu(callbackQuery.Message!.Chat.Id, cancellationToken);

        string info = $"""
                    - Пункт отправления: {result.Value.Value}
                    - Пункт назначения
                    - Дату и время отправления
                    - Количество свободных мест
                    - Стоимость поездки

                    Введите - Пункт назначения: город, деревня
                    """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
