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

namespace TgBot.Endpoints.Trips.ListProcess;

public class ActiveEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public ActiveEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, IMediator mediator, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _mediator = mediator;
        _userManager = userManager;
    }

    public override void Configure()
    {
        Routes("activeTrips");
        State(UserStates.TripActive);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        int.TryParse(callbackQuery.Data, out int pageNumber);

        if (pageNumber == 0)
            pageNumber = 1;

        var username = callbackQuery.From.Username;

        if (username == null)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Пользователь не найден. Перейдите на /start",
                cancellationToken: cancellationToken);

            return;
        }

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Пользователь не найден. Перейдите на /start",
                cancellationToken: cancellationToken);

            return;
        }

        var result = await _mediator.Send(new GetByInitiatorId { Id = user.Id, PageNumber = pageNumber });

        if (!result.IsSuccess)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Поездок не найдено",
                replyMarkup: InitiatorsTemplates.Menu(),
                cancellationToken: cancellationToken);

            return;
        }

        foreach (var item in result.Value)
        {
            var r = $"""
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

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Меню", $"menuInitiator"),
                    },
                });

        if (result.PageNumber == 1 && result.PageNumber < result.TotalPages)
        {
            inlineKeyboard = new(
                new []
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Меню", $"menu"),
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
                        InlineKeyboardButton.WithCallbackData("Меню", $"menu"),
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
                        InlineKeyboardButton.WithCallbackData("Меню", $"menu"),
                        InlineKeyboardButton.WithCallbackData(">>", $"{result.PageNumber + 1}"),
                    },
                });
        }

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"страница {result.PageNumber}",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);

        _userBotService.SetProcess(callbackQuery.From.Id, UserProcesses.TripList);
    }
}