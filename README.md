# OldExplorer

[![image](https://user-images.githubusercontent.com/79026235/152910441-59ba653c-5607-4f59-90c0-bc2851bf2688.png)Download the zip file](https://github.com/LesFerch/OldExplorer/releases/download/1.0.0/OldExplorer.zip)

## Launch Windows 10 Explorer on Windows 11

The Windows 10 Explorer can be accessed on Windows 11 by opening the Control Panel and then clicking the up arrow a couple of times. **OldExplorer** is simply a launcher that does essentially the same thing, but also lets you provide your preferred starting folder. It actually runs the command **control admintools** and then, by default, sends **This PC** to the navigation bar. If you specify a different folder on the OldExplorer command line, it will navigate there instead.

The goal of this project was to improve on similar PowerShell based solutions with a faster launch, no console window, easy change of the starting folder, use the Explorer icon, and not interfere with an existing open Control Panel window.

## Why run the Windows 10 Explorer on Windows 11?

For some, it's just a preferred interface, but for others there are specific functional reasons, such as using offline files and folders or being able to set the folder type for an entire folder tree.

# How to Download and Run

1. Download the zip file using the link above.
2. Extract **OldExplorer.exe**.
3. Right-click**OldExplorer.exe**, select Properties, check Unblock, and click OK.
4. Optionally move **OldExplorer.exe** to the folder of your choice.
5. Double-click **OldExplorer.exe** to open the Windows 10 Explorer to **This PC**.
6. If you skipped step 3, then, in the SmartScreen window, click More info and then Run anyway.
7. Optionally make a shortcut to **OldExplorer.exe** and edit the command line to open the old Explorer to the folder of your choice. You may use environment variables and spaces are supported, as long as the path is in quotes.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen anytime you download a new executable and may require extra steps to whitelist the file.

# Summary

**OldExplorer** launches the Windows 10 Explorer on Wndows 11 by executing the command **control admintools** and then sends the  starting folder (**This PC** by default) to the navigation bar.

**Note**: OldExplorer.exe is only useful on Windows 11. It will run on Windows 10, but it is pointless to do that.

## Usage examples

**Note**: Typically you would make a shortcut to OldExplorer.exe and then edit the command line of the shortcut, if you want to start in a folder other than **This PC**.

**Example 1**:\
Launch the Windows 10 Explorer to **This PC**:\
`OldExplorer`

**Example 2**:\
Launch the Windows 10 Explorer to **C:\\**:\
`OldExplorer C:\`

**Example 3**:\
Launch the Windows 10 Explorer to **%UserProfile%\Documents**:\
`OldExplorer %UserProfile%\Documents`

**Example 4**:\
Launch the Windows 10 Explorer to **C:\Program Files**:\
`OldExplorer "C:\Program Files"`

\
\
[![image](https://user-images.githubusercontent.com/79026235/153264696-8ec747dd-37ec-4fc1-89a1-3d6ea3259a95.png)](https://github.com/LesFerch/OldExplorer)
