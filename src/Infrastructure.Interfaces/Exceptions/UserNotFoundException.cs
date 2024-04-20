using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"Не найден пользователь с именем: {userName}")
    {
    }
}
