using DataAccess.Sqlite;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.Templates;

namespace TgBot.Endpoints.Initiator;

public class IsInitiatorEndpoint : CallbackQueryEndpoint
{
    private readonly ITelegramBotClient _botClient;
    private readonly UserManager<AppUser> _userManager;

    public IsInitiatorEndpoint(ITelegramBotClient botClient, UserManager<AppUser> userManager)
    {
        _botClient = botClient;
        _userManager = userManager;
    }

    public override void Configure()
    {
        Routes("userIsInitiator");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        string text = "Поздравляю вы стали водителем. Можете использовать следующие функции:\n";

        if (callbackQuery.From == null)
            return;

        var info = new UserLoginInfo("Telegram", callbackQuery.From.Id.ToString(), "Telegram");

        // Если у пользователя уже есть логин (т.е. если есть запись в таблице AspNetUserLogins),
        // то войдите в систему пользователя с помощью этого внешнего поставщика логинов
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user == null)
            return;

        user.UserType = AppUserType.Initiator;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            text = "упс, ошибка";
        }

        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text,
            replyMarkup: InitiatorsTemplates.Menu(),
            cancellationToken: cancellationToken);
    }
}
