using System.ComponentModel;

namespace BetterRaList.Configs
{
    public class RaPlayerList
    {
        [Description("Enable sorting players by increasing their PlayerId")]
        public bool SortListByPlayerId { get; set; } = true;

        [Description("Overwatch text format (leave it blank to disable)")]
        public string OverwatchFormat { get; set; } = "<link=RA_OverwatchEnabled><color=white>[</color><color=#03f8fc>OV</color><color=white>]</color></link> ";

        [Description("PermissionGroup text format. {0} - group color. {1} - group name (leave it blank to disable)")]
        public string PermissionGroupFormat { get; set; } = "<link=RA_Admin>[<color=#{0}>{1}.</color>]</link> ";

        [Description("Field to drop the selection (leave blank to disable)")]
        public string ResetField { get; set; } = "Reset Selection";
    }
}
