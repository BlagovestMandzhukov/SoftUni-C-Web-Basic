using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IUsersService
    {
        void CreateUser(string name, string password, string email);

        string GetUserId(string name, string password);

    }
}
