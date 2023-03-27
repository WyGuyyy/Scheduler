using Microsoft.EntityFrameworkCore;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Services.Interfaces;
using SpaceWScheduler.Services.Services;

namespace SpaceWScheduler.Models.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection AddUserDefinedServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection
                .AddScheduleGetter()
                .AddSchedulerUpdater()
                .AddEventGetter()
                .AddEventUpdater()
                .AddSchedulerDb(config);
                //.AddMockDB();
            
            return serviceCollection;
        }

        public static IServiceCollection AddScheduleGetter(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddScoped<IScheduleGetter, ScheduleGetter>();
            return serviceCollection;
        }

        public static IServiceCollection AddSchedulerUpdater(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IScheduleUpdater, ScheduleUpdater>();
            return serviceCollection;
        }

        public static IServiceCollection AddEventGetter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventGetter, EventGetter>();
            return serviceCollection;
        }

        public static IServiceCollection AddEventUpdater(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventUpdater, EventUpdater>();
            return serviceCollection;
        }

        public static IServiceCollection AddSchedulerDb(this IServiceCollection serviceCollection, IConfiguration config) 
        {
            serviceCollection.AddDbContextFactory<SchedulerContext>(
                options => options.UseSqlite(config.GetConnectionString("SchedulerDBContext"))
            );
            return serviceCollection;
        }

        /*public static IServiceCollection AddMockDB(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddSingleton<IMockDB, MockDB>();
            return serviceCollection;
        }*/
    }
}
