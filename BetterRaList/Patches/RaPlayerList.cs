using HarmonyLib;
using RemoteAdmin;
using System;
using UnityEngine;
using System.Linq;

namespace BetterRaList.Patches
{
	[HarmonyPatch(typeof(RemoteAdmin.Communication.RaPlayerList), nameof(RemoteAdmin.Communication.RaPlayerList.ReceiveData), new Type[] { typeof(global::CommandSender), typeof(string) })]
	public class RaPlayerList
	{
		public static bool Prefix(RemoteAdmin.Communication.RaPlayerList __instance, global::CommandSender sender, string data)
		{
			string[] array = data.Split(new char[]
			{
				' '
			});

			if (array.Length != 1)
			{
				return false;
			}

			int num = 0;

			if (!int.TryParse(array[0], out num))
			{
				return false;
			}

			bool shouldLogToConsole = num == 1;
			bool canViewHidenBadges = CommandProcessor.CheckPermissions(sender, global::PlayerPermissions.ViewHiddenBadges);
			bool canViewHidenGlobalBadges = CommandProcessor.CheckPermissions(sender, global::PlayerPermissions.ViewHiddenGlobalBadges);

			PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;

			if (playerCommandSender != null && playerCommandSender.ServerRoles.Staff)
			{
				canViewHidenBadges = true;
				canViewHidenGlobalBadges = true;
			}

			string text = string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.ResetField) ? string.Empty : $"\n<size=18><color=white>(0) {Core.Singleton.Config.RaPlayerList.ResetField}</color></size>\n";

			ReferenceHub[] referenceHubs = global::ReferenceHub.GetAllHubs().Values.ToArray();

			if (Core.Singleton.Config.RaPlayerList.SortListByPlayerId)
				Array.Sort(referenceHubs, (x, y) => x.queryProcessor.PlayerId.CompareTo(y.queryProcessor.PlayerId));

			foreach (global::ReferenceHub referenceHub in referenceHubs)
			{
				if (referenceHub.isDedicatedServer || !referenceHub.Ready)
					continue;

				QueryProcessor queryProcessor = referenceHub.queryProcessor;

				string raPermissionText = string.Empty;

				bool isOverwatchEnabled = false;

				global::ServerRoles serverRoles = referenceHub.serverRoles;

				try
				{
					if (string.IsNullOrEmpty(serverRoles.HiddenBadge) || (serverRoles.GlobalHidden && canViewHidenGlobalBadges) || (!serverRoles.GlobalHidden && canViewHidenBadges))
					{
						if (serverRoles.RaEverywhere)
						{
							raPermissionText = "<link=RA_RaEverywhere><color=white>[<color=#EFC01A></color><color=white>]</color></link> ";
						}
						else if (serverRoles.Staff)
						{
							raPermissionText = "<link=RA_StudioStaff><color=white>[<color=#005EBC></color><color=white>]</color></link> ";
						}
						else if (serverRoles.RemoteAdmin)
						{
							string groupColor = string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.PermissionGroupFormat) ? string.Empty : Color.white.ToHex();
							string groupName = string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.PermissionGroupFormat) || string.IsNullOrEmpty(serverRoles?.Group?.BadgeText) ? string.Empty : (serverRoles.Group.BadgeText.Length < 6) ? serverRoles.Group.BadgeText : serverRoles.Group.BadgeText.Substring(0, 6).Trim();

							if (!string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.PermissionGroupFormat) && !string.IsNullOrEmpty(serverRoles.Group.BadgeColor))
							{
								foreach (global::ServerRoles.NamedColor namedColor in serverRoles.NamedColors)
								{
									if (namedColor.Name == serverRoles.Group.BadgeColor)
									{
										groupColor = namedColor.ColorHex;
										break;
									}
								}
							}

							raPermissionText = string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.PermissionGroupFormat) ? "<link=RA_Admin><color=white>[]</color></link> " : string.Format(Core.Singleton.Config.RaPlayerList.PermissionGroupFormat, groupColor, groupName);
						}
						else
						{
							raPermissionText = string.Empty;
						}
					}

					isOverwatchEnabled = serverRoles.OverwatchEnabled;
				}
				catch
				{
				}

				text = string.Concat(new object[]
				{
					text,
					raPermissionText,
					isOverwatchEnabled ? (string.IsNullOrEmpty(Core.Singleton.Config.RaPlayerList.OverwatchFormat) ? "<link=RA_OverwatchEnabled><color=white>[</color><color=#03f8fc></color><color=white>]</color></link> " : Core.Singleton.Config.RaPlayerList.OverwatchFormat) : string.Empty,
					"<color={RA_ClassColor}>(",
					queryProcessor.PlayerId,
					") ",
					referenceHub.nicknameSync.CombinedName.Replace("\n", string.Empty).Replace("RA_", string.Empty),
					"</color>"
				});

				text += "\n";
			}

			sender.RaReply(string.Format("${0} {1}", __instance.DataId, text), true, !shouldLogToConsole, string.Empty);

			return false;
		}
	}
}
