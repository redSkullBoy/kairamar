using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Sqlite.Identity;

internal class UserNotFoundException : Exception
{
    internal UserNotFoundException(string userName) : base($"Не найден пользователь с именем: {userName}")
    {
    }
}
