using Domain.Entities;
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

public class ToAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public ToAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
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
        State(UserStates.TripFindToAddress);
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

        var filter = _cache.GetTripFilterOrNull(callbackQuery.From!.Id) ?? new TripFilter();

        filter.ToAddressId = addressId;
        filter.ToAddress = result.Value.Value;

        _cache.SetTripFilter(callbackQuery.From!.Id, filter, TimeSpan.FromMinutes(5));

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
