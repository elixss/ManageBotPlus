﻿using Discord;
using System.Reflection;

namespace ManageBotPlus
{
    public readonly struct Config
    {
        public static readonly int lastUpdate = 1669650735;
        public static readonly Color color = 0xac45ec;
        public static readonly string developer = "elixss#9999";
        public static readonly ulong testGuildId = 890279450376294453;
        public static readonly DateTime startTime = DateTime.UtcNow;
        public static readonly string supportInvite = "https://discord.gg/5mYSubhkrg";
        public static readonly List<Type> modules = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IManageBotModule)))
            .ToList();
        public static readonly int moduleCount = modules.Count;
        public static readonly string invite = "https://discord.com/api/oauth2/authorize?client_id=1043131771258159104&permissions=274878220288&scope=bot%20applications.commands";
    }
}
