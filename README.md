# OldExplorer

[![image](https://github.com/LesFerch/WinSetView/assets/79026235/0188480f-ca53-45d5-b9ff-daafff32869e)Download the zip file](https://github.com/LesFerch/OldExplorer/releases/download/2.1.3/OldExplorer.zip)

## Launch Windows 10 Explorer on Windows 11

The Windows 10 Explorer can be accessed on Windows 11 by opening the Control Panel and then clicking the up arrow a couple of times. **OldExplorer** is simply a launcher that does essentially the same thing, but also lets you provide your preferred starting folder.

This project improves on similar scripted solutions by providing a faster launch, no console window flash, no Control Panel flash, easy change of the starting folder, no interference with an existing open Control Panel window, and improved ease of use via a Windows shortcut.

**Note**: You can make the Windows 10 Explorer the default file manager in Windows 11 with [SwitchExplorer](https://lesferch.github.io/SwitchExplorer/), but note that option will not provide the old details pane that allows direct editing of metadata. Use OldExplorer (i.e. this tool) when you need to get access to the old details pane.

**Note**: OldExplorer.exe is only useful on Windows 11. It will run on Windows 10, but it's pointless to do so.

## Why run the Windows 10 Explorer on Windows 11?

For some, it's just a preferred interface, but for others there are specific functional reasons, such as using offline files and folders or being able to set the folder type for an entire folder tree or getting access to the old details pane that allows direct editing of file metadata.

Another reason is to get better performance, especially with folders that contain a lot of media files.

## How to Download and Run

1. Download the zip file using the link above.
2. Extract **OldExplorer.exe**.
3. Right-click **OldExplorer.exe**, select Properties, check Unblock, and click OK.
4. Optionally move **OldExplorer.exe** to the folder of your choice.
5. Double-click **OldExplorer.exe** to open the Windows 10 Explorer to **C:\\**
6. If you skipped step 3, then, in the SmartScreen window, click More info and then Run anyway.
7. Optionally make a shortcut to **OldExplorer.exe** and edit the command line to open the old Explorer to the folder of your choice. You may use environment variables and spaces are supported, as long as the path is in quotes.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen any time you download a new executable and may require extra steps to whitelist the file.

## Usage

Typically you would make a **shortcut** to `OldExplorer.exe` and then edit the command line of the shortcut, if you want to start in a folder other than Explorer's current start folder.

**Example shortcut**:

![image](https://github.com/user-attachments/assets/c67b5bf6-bb52-4044-a89d-57f14db26719)


**Example 1**:\
Launch the Windows 10 Explorer to Explorer's current "LaunchTo" value (i.e. the "Open File Explorer to" setting)\
`OldExplorer`

**Example 2**:\
Launch the Windows 10 Explorer to **D:\\**\
`OldExplorer D:`

**Example 3**:\
Launch the Windows 10 Explorer to **Pictures**:\
`OldExplorer Pictures`

**Example 4**:\
Launch the Windows 10 Explorer to **This PC**:\
`OldExplorer "This PC"`

**Example 5**:\
Launch the Windows 10 Explorer to **Camera Roll**:\
`OldExplorer "shell:camera roll"`

**Example 6**:\
Launch the Windows 10 Explorer to **Frequent Folders**:\
`OldExplorer shell:::{3936E9E4-D92C-4EEE-A85A-BC16D5EA0819}`

**Example 7**:\
Launch the Windows 10 Explorer to **C:\Users\\[UserName]\AppData\Local**:\
`OldExplorer "%LocalAppData%"`

### Command line options

The following are optional command line arguments

**/d+**\
This will add a "DelegateExecute" registry setting that will change the default Explorer when you execute "explorer" or "explorer.exe" from a shortcut, command line, or Start > Run to instead run OldExplorer.exe. Please note that this option will not completely change the default Explorer. For example, running Explorer with a command line argument will still run Explorer instead of OldExplorer.

**Note**: Do not include /d+ in your shortcut(s). You only need to run OldExplorer.exe _once_ with the /d+ argument to add the DelegateExecute registry entry.

**Note**: Starting with build 26100.4202, this option no longer works in Windows 11 for the default Explorer taskbar icon. It does continue to work when Explorer is launched via a command line or via a shortcut. The default Explorer taskbar icon can be replaced with a shortcut link, but then you will see a second Explorer taskbar icon whenever an Explorer window is open.

You can use the tool [SwitchExplorer](https://lesferch.github.io/SwitchExplorer/) to catch the remaining scenarios where the Windows 11 Explorer would otherwise open.

**/d-**\
This removes the registry entry created by the **/d+** option.

**/x**\
This exits the app without opening Explorer. This is useful if you want to use the tool to apply the registry setting provided by the **/d+** option without opening an Explorer window.

### Start folder location options

The following shortcut keywords are supported (case does not matter):

Desktop\
Documents\
Downloads\
Favorites\
Frequent\
Home\
Libraries\
Music\
Onedrive\
Pictures\
Public\
Recent\
"Recycle bin"\
"This PC"\
Userprofile\
Videos


You can also use any shell shortcuts, shell GUIDs, and environment variables that point to folders. See the links below:

[Shell shortcuts](https://www.elevenforum.com/t/list-of-windows-11-shell-commands-for-shell-folder-shortcuts.1080/)

[Shell GUIDs](https://www.elevenforum.com/t/list-of-windows-11-clsid-key-guid-shortcuts.1075/)

[Environment variables](https://www.elevenforum.com/t/complete-list-of-environment-variables-in-windows-11.11212/)

\
\
[![image](https://github.com/LesFerch/WinSetView/assets/79026235/63b7acbc-36ef-4578-b96a-d0b7ea0cba3a)](https://github.com/LesFerch/OldExplorer)

