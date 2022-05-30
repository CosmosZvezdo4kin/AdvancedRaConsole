# BetterRaList
More handy and customizable list of players in the RA-Panel (SCP:SL Plugin)

## How to install plugin?
Put **BetterRaList.dll** under the release tab into `%appdata%\EXILED\Plugins` (Windows) or `.config/EXILED/Plugins` (Linux) folder.

## Config
| Field | Description | Default Value |
| ------------- | ------------- | ------------- |
| `SortListByPlayerId` | Enable sorting players by increasing their PlayerId | `true` |
| `OverwatchFormat`  | Overwatch text format | `<link=RA_OverwatchEnabled><color=white>[</color><color=#03f8fc>OV</color><color=white>]</color></link> ` |
| `PermissionGroupFormat` | PermissionGroup text format. {0} - group color. {1} - group name (leave it blank to disable) | `<link=RA_Admin>[<color=#{0}>{1}.</color>]</link> ` |
| `ResetField`  | Field to drop the selection (leave blank to disable) | `Reset Selection` |

## Default Configs
```yaml
BetterRaList:
# Is plugin enabled ?
  is_enabled: true
  # RaPlayerList Settings
  ra_player_list:
  # Enable sorting players by increasing their PlayerId
    sort_list_by_player_id: true
    # Overwatch text format
    overwatch_format: '[ХУЙ]'
    # PermissionGroup text format. {0} - group color. {1} - group name (leave it blank to disable)
    permission_group_format: ''
    # Field to drop the selection (leave blank to disable)
    reset_field: Сбросить Выделение
```

## Example picture
[example_image](https://cdn.discordapp.com/attachments/962743978988564590/980913930308304927/unknown.png)
