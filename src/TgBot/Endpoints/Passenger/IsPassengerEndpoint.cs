using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.Templates;

namespace TgBot.Endpoints.Passenger;

public class IsPassengerEndpoint : MessageEndpoint
{
    private readonly ITelegramBotClient _botClient;
    private readonly UserManager<AppUser> _userManager;

    public IsPassengerEndpoint(ITelegramBotClient botClient, UserManager<AppUser> userManager)
    {
        _botClient = botClient;
        _userManager = userManager;
    }

    public override void Configure()
    {
        PreRoutes("/userispassenger");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        string text = "Роль пассажир. Можете использовать следующие функции:\n";

        if (message.From == null)
            return;

        var info = new UserLoginInfo("Telegram", message.From.Id.ToString(), "Telegram");

        // Если у пользователя уже есть логин (т.е. если есть запись в таблице AspNetUserLogins),
        // то войдите в систему пользователя с помощью этого внешнего поставщика логинов
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user == null)
        {
            user = new AppUser
            {
                UserName = message.From.Username,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName,
            };

            //Это создаст нового пользователя в таблице AspNetUsers без пароля
            await _userManager.CreateAsync(user);

            await _userManager.AddLoginAsync(user, info);
        }

        user.UserType = AppUserType.Passenger;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            text = "упс, ошибка";
        }

        //await _botClient.AnswerCallbackQueryAsync(
        //    callbackQueryId: callbackQuery.Id,
        //    cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: text,
            replyMarkup: PassengersTemplates.Menu(),
            cancellationToken: cancellationToken);
    }
}
