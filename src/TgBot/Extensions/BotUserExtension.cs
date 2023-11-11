using Telegram.Bot.Types;

namespace TgBot.Extensions;

public static class BotUserExtension
{
    public static string GetPrettyName(this User messageNewChatMember)
    {
        var names = new List<string>(3);

        if (!string.IsNullOrWhiteSpace(messageNewChatMember.FirstName))
            names.Add(messageNewChatMember.FirstName);
        if (!string.IsNullOrWhiteSpace(messageNewChatMember.LastName))
            names.Add(messageNewChatMember.LastName);
        if (!string.IsNullOrWhiteSpace(messageNewChatMember.Username))
            names.Add("(@" + messageNewChatMember.Username + ")");

        return string.Join(" ", names);
    }

    public static string GetPrettyNames(this IEnumerable<User> users) => string.Join(", ", users.Select(GetPrettyName));
}
