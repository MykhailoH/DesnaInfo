using System;
using System.Collections.Generic;
using System.Text;
using DesnaInfo.Business.UserService;
using DesnaInfo.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DesnaInfo.Business
{
    public static class Startup
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddDataAccess();
            services.AddSingleton<IUserService, UserService.UserService>();
        }
    }
}
