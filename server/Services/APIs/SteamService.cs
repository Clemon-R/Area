using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Narochno.Steam;

namespace Area.Services.APIs
{
    public class SteamService
    {

        void Test()
        {
            var client = new ServiceCollection().AddSteam(new SteamConfig()).BuildServiceProvider().GetService<ISteamClient>();

        }
    }
}
