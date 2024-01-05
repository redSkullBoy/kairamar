﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.Endpoints.Trip;

public class AddEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;

    public AddEndpoint(IUserBotService userBotService)
    {
        _userBotService = userBotService;
    }

    public override void Configure()
    {
        Routes("addTrip");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await BotClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        const string info = """
                            Для создания поездки укажите следующую информацию:
                            - Пункт отправления
                            - Пункт назначения
                            - Дату и время отправления
                            - Количество свободных мест
                            - Стоимость поездки
                            """;

        await BotClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Пункт отправления",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetState(callbackQuery.From.Id, "AddDeparturePoint");
    }
}