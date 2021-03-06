﻿using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Commands.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveSeven
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterTwitchCommand<T>(this IServiceCollection services)
            where T : ITwitchCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(ITwitchCommand), typeof(LoggingCommand<T>));
        }

        public static IServiceCollection RegisterBattleCommand<T>(this IServiceCollection services)
            where T : ITwitchCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(BattleOnlyCommand<T>))
                .AddSingleton(typeof(ITwitchCommand), typeof(LoggingCommand<BattleOnlyCommand<T>>));
        }

        public static IServiceCollection RegisterNonBattleCommand<T>(this IServiceCollection services)
            where T : ITwitchCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(NonBattleCommand<T>))
                .AddSingleton(typeof(ITwitchCommand), typeof(LoggingCommand<NonBattleCommand<T>>));
        }

        public static IServiceCollection RegisterEquipmentData(this IServiceCollection services)
        {
            return services.AddSingleton<EquipmentData<Weapon>>()
                .AddSingleton<EquipmentData<Armlet>>()
                .AddSingleton<EquipmentData<Accessory>>();
        }
    }
}
