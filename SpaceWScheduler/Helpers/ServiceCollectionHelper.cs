using SpaceWScheduler.Services.Interfaces;
using SpaceWScheduler.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection AddUserDefinedServices(this IServiceCollection serviceCollection) 
        {
            serviceCollection
                .AddScheduleGetter()
                .AddMockDB();
            
            return serviceCollection;
        }

        public static IServiceCollection AddScheduleGetter(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddScoped<IScheduleGetter, ScheduleGetter>();
            return serviceCollection;
        }

        public static IServiceCollection AddMockDB(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddSingleton<IMockDB, MockDB>();
            return serviceCollection;
        }
    }
}
