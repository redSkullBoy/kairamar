using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;
using TgBot.Templates;
using UseCases.Handlers.Trips.Commands;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddDescriptionEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly IMediator _mediator;

    public AddDescriptionEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, IMediator mediator)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _mediator = mediator;
    }

    public override void Configure()
    {
        State(UserStates.TripAddDescription);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        var trip = _cache.GetTripOrNull(message.From!.Id)!;

        trip.Description = message.Text ?? string.Empty;

        _cache.SetTrip(message.From!.Id, trip, TimeSpan.FromMinutes(5));

        var result = await _mediator.Send(new CreateCommand { Value = trip });

        string info = $"""
            - Пункт отправления: {result.Value.FromAddressName}
            - Пункт назначения: {result.Value.ToAddressName}
            - Дату и время отправления: {result.Value.StartDateLocal}
            - Количество свободных мест: {result.Value.RequestedSeats}
            - Стоимость поездки: {result.Value.Price}

            Поездка сохранена
            """;

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: info,
            replyMarkup: InitiatorsTemplates.Menu(),
            cancellationToken: cancellationToken);

        _userBotService.SetProcess(message.From.Id, UserProcesses.TripList);
    }
}
