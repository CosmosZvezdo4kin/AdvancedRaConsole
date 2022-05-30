using System;
using Exiled.API.Features;
using Exiled.API.Enums;
using HarmonyLib;

namespace BetterRaList
{
    public class Core : Plugin<Config>
    {
        public static Core Singleton = new Core();
        public override string Prefix => "BetterRaList";
        public override string Name => "BetterRaList";
        public override string Author => "SpaceJulien";
        public override Version Version => new Version(1, 0, 0);
        public override PluginPriority Priority => PluginPriority.Default;
        public Harmony Harmony { get; private set; }
        public override void OnEnabled()
        {
            Singleton = this;

            Patch();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;

            Unpatch();

            base.OnDisabled();
        }
        private void Patch()
        {
            this.Harmony = new Harmony("betterralist.julien."+DateTime.Now.Ticks);

            try
            {
                this.Harmony.PatchAll();
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }
        private void Unpatch()
        {
            this.Harmony?.UnpatchAll();
        }
    }
}
