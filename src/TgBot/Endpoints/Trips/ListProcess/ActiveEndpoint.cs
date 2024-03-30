using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using MediatR;
using UseCases.Handlers.Trips.Queries;
using DataAccess.Sqlite;
using Microsoft.AspNetCore.Identity;
using TgBot.Templates;
using System.Text;

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
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

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

        var result = await _mediator.Send(new GetByInitiatorId { Id = user.Id });

        if (!result.IsSuccess)
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Поездок не найдено",
                replyMarkup: InitiatorsTemplates.Menu(),
                cancellationToken: cancellationToken);

            return;
        }

        var text = new StringBuilder();

        foreach (var item in result.Value.Value)
        {
            text.Append($"""
                - Пункт отправления: {item.FromAddressName}
                - Пункт назначения: {item.ToAddressName}
                - Дату и время отправления: {item.StartDateLocal.ToString("f")}
                - Количество свободных мест: {item.RequestedSeats}
                - Стоимость поездки: {item.Price}
                ==========================================
                """);
        }

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text.ToString(),
            cancellationToken: cancellationToken);
    }
}