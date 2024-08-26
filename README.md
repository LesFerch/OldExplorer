# OldExplorer

[![image](https://github.com/LesFerch/WinSetView/assets/79026235/0188480f-ca53-45d5-b9ff-daafff32869e)Download the zip file](https://github.com/LesFerch/OldExplorer/releases/download/1.3.0/OldExplorer.zip)

## Launch Windows 10 Explorer on Windows 11

The Windows 10 Explorer can be accessed on Windows 11 by opening the Control Panel and then clicking the up arrow a couple of times. **OldExplorer** is simply a launcher that does essentially the same thing, but also lets you provide your preferred starting folder. It actually runs the command **control admintools** and then, by default, sends **\\** to the navigation bar. If you specify a different folder on the OldExplorer command line, it will navigate there instead.

The goal of this project is to improve on similar PowerShell based solutions with a faster launch, no console window, easy change of the starting folder, use the Explorer icon, and not interfere with an existing open Control Panel window.

**Note**: You can make the Windows 10 Explorer the default file manager in Windows 11 with [SwitchExplorer](https://lesferch.github.io/SwitchExplorer/), but note that option will not provide the old details pane that allows direct editing of metadata. Use OldExplorer (i.e. this tool) when you need to get access to the old details pane.

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

## Summary

**OldExplorer** launches the Windows 10 Explorer on Windows 11 by executing the command **control admintools** and then sends the  starting folder (**\\** by default) to the navigation bar.

**Note**: OldExplorer.exe is only useful on Windows 11. It will run on Windows 10, but it is pointless to do that.

## Usage examples

**Note**: Typically you would make a shortcut to OldExplorer.exe and then edit the command line of the shortcut, if you want to start in a folder other than **C:\\** or want to change either of the options mentioned below.

**Note**: By Default, OldExplorer uses the clipboard for any path specified on the command line that is greater than 3 characters in length. This allows the path to be entered into the address bar very fast at the expense of overwriting whatever is currently on the clipboard. You can tell OldExplorer to not use the clipboard by specifying /x on the command line.

**Note**: By Default, OldExplorer includes two small pauses of 100 milliseconds and 200 milliseconds for the Explorer interface elements to load before attempting to enter the path in the navigation bar. You may fine-tune these pauses via the /a and /b command line arguments. The values are in milliseconds. For example, if you want to set both pauses to zero, you would include /a:0 and /b:0 (or /a=0 and /b=0) on the command line. Please note that smaller values will provide a quicker load at the risk of Explorer remaining in the Control Panel AdminTools screen. You will still see the AdminTools display briefly, even with the values set to 0.

Example shortcut:

![image](https://github.com/user-attachments/assets/79053340-6a6a-4e66-9196-4e2989772d87)

**Note**: In the above example, the pause values are set to the defaults. You can also leave the /a and /b arguments out completely to get the defaults and you may change the values to lower numbers, at the risk of the Explorer sometimes window remaining at the AdminTools screen. Also note that the "Start in" folder makes no difference to OldExplorer.exe.

**Example 1**:\
Launch the Windows 10 Explorer to **C:\\**\
`OldExplorer`

**Example 2**:\
Launch the Windows 10 Explorer to **D:**\
`OldExplorer D:`

**Example 3**:\
Launch the Windows 10 Explorer to **%UserProfile%\Documents**:\
`OldExplorer %UserProfile%\Documents`

**Example 4**:\
Launch the Windows 10 Explorer to **This PC**:\
`OldExplorer "This PC"`

**Example 5**:\
Launch the Windows 10 Explorer to **This PC** and do not use the clipboard:\
`OldExplorer /x "This PC"`

**Example 6**:\
Launch the Windows 10 Explorer to **D:**\ as quickly as possible (warning: may remain at AdminTools screen):\ 
`OldExplorer /a:0 /b:0 D:`

\
\
[![image](https://github.com/LesFerch/WinSetView/assets/79026235/63b7acbc-36ef-4578-b96a-d0b7ea0cba3a)](https://github.com/LesFerch/OldExplorer)
