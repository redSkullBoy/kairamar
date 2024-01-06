using DataAccess.Sqlite;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.Extensions;

namespace TgBot.Endpoints;

public class StartEndpoint : MessageEndpoint
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITelegramBotClient _botClient;

    public StartEndpoint(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITelegramBotClient botClient)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes("/start");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        var info = new UserLoginInfo("Telegram", message.From.Id.ToString(), "Telegram");
        // Если у пользователя уже есть логин (т.е. если есть запись в таблице AspNetUserLogins),
        // то войдите в систему пользователя с помощью этого внешнего поставщика логинов
        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
            return;

        // Если в таблице AspNetUserLogins нет записи, у пользователя может не быть локальной учетной записи
        var username = message.From.Username;

        if (username == null)
            return;

        // Создайте нового пользователя без пароля, если у нас еще нет такого пользователя
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            user = new AppUser
            {
                UserName = username,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName,
            };

            //Это создаст нового пользователя в таблице AspNetUsers без пароля
            await _userManager.CreateAsync(user);
        }

        // Добавьте логин (т.е. вставьте строку для пользователя в таблицу AspNetUserLogins)
        await _userManager.AddLoginAsync(user, info);

        //Затем подписываем пользователя
        await _signInManager.SignInAsync(user, isPersistent: false);
        //
        var prettyUserName = message.From.GetPrettyName();

        await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Привет, {prettyUserName}!!!",
                        cancellationToken: cancellationToken);

        var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Водитель", "userIsInitiator"),
                InlineKeyboardButton.WithCallbackData("Попутчик", "userIsCompanion"),
            },
                });

        await _botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Выберите роль",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);

        return;
    }
}
