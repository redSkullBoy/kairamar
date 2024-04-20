using DataAccess.Sqlite;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Telegram.Bot;
using Telegram.Bot.Types;
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
        PreRoutes("/start");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        if(message.From == null)
            return;

        var info = new UserLoginInfo("Telegram", message.From.Id.ToString(), "Telegram");
        // Если у пользователя уже есть логин (т.е. если есть запись в таблице AspNetUserLogins),
        // то войдите в систему пользователя с помощью этого внешнего поставщика логинов
        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);

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

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
        };

        await _signInManager.SignInWithClaimsAsync(user, true, claims);
        //
        var prettyUserName = message.From.GetPrettyName();

        await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Привет, {prettyUserName}!!! \nЯ бот по подбору поездок. \nЕсть две роли: водитель, пассажир. \nВыберите роль в меню",
                        cancellationToken: cancellationToken);

        return;
    }
}
