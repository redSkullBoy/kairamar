using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using MediatR;
using UseCases.Handlers.Trips.Queries;
using DataAccess.Sqlite;
using Microsoft.AspNetCore.Identity;
using TgBot.Templates;
using TgBot.Constants;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.FindProcess;

public class ListEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;
    private readonly MemoryCacheService _cache;

    public ListEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, IMediator mediator, UserManager<AppUser> userManager, MemoryCacheService cache)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _mediator = mediator;
        _userManager = userManager;
        _cache = cache;
    }

    public override void Configure()
    {
        Routes("findList");
        State(UserStates.TripFindList);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        int.TryParse(callbackQuery.Data, out int pageNumber);

        if (pageNumber == 0)
            pageNumber = 1;

        var filter = _cache.GetTripFilterOrNull(callbackQuery.From!.Id);

        if (filter == null)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Филтр не найден",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        filter.PageNumber = pageNumber;

        var result = await _mediator.Send(new GetAllRequest { Value = filter });

        if (!result.IsSuccess || result.Value.Count == 0)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Поездок не найдено",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        foreach (var item in result.Value)
        {
            var r = $"""
                - Пользователь: @{item.InitiatorName}
                - Пункт отправления: {item.FromAddressName}
                - Пункт назначения: {item.ToAddressName}
                - Дату и время отправления: {item.StartDateLocal.ToString("f")}
                - Количество свободных мест: {item.RequestedSeats}
                - Стоимость поездки: {item.Price}
                """;

            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: r,
                cancellationToken: cancellationToken);
        }

        InlineKeyboardMarkup? inlineKeyboard = null;

        if (result.PageNumber == 1 && result.PageNumber < result.TotalPages)
        {
            inlineKeyboard = new(
                new []
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(">>", $"{result.PageNumber + 1}"),
                    },
                });
        }
        else if (result.PageNumber > 1)
        {
            inlineKeyboard = new(
                new []
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("<<", $"{result.PageNumber - 1}"),
                    },
                });
        }
        else if (result.PageNumber < result.TotalPages)
        {
            inlineKeyboard = new(
                new []
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("<<", $"{result.PageNumber - 1}"),
                        InlineKeyboardButton.WithCallbackData(">>", $"{result.PageNumber + 1}"),
                    },
                });
        }

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"страница {result.PageNumber}",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);
    }
}