using System.ComponentModel;
using Exiled.API.Interfaces;

using BetterRaList.Configs;

namespace BetterRaList
{
    public class Config : IConfig
    {
        [Description("Is plugin enabled ?")]
        public bool IsEnabled { get; set; } = true;

        [Description("RaPlayerList Settings")]
        public RaPlayerList RaPlayerList { get; set; } = new RaPlayerList();
    }
}
