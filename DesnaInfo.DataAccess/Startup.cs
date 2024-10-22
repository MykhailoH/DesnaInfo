using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DesnaInfo.DataAccess
{
    public static class Startup
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IUserDataController, UserDataController>();
        }
    }
}
