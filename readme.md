Steam Notifications Tray App
============================
This program displays your Steam notifications count in your notifications
area. Clicking on the icon opens up the notifications list, just like it
does on Steam. From there you can click on the notification item, and it
will open the matching page in your browser. Double click on the icon to
open Steam.

You will most likely need to tell Windows to not hide the icons for the
two icons' positions to stay fixed. Best to do this when you have a
notification. The intended order is for the count to come first, and the
mail icon to come second, the same way it's displayed on Steam.

System requirements
-------------------
Requires .NET Framework 4.5.1.

Settings
========

General
-------
- **Refresh interval:** how often to refresh the notifications count. Ranges
  from 1 second to 1 day.
- **Open links in Steam:** self-explanatory; opens notifications in the Steam
  client instead of the browser.
- **Enable balloon notifications:** show a balloon notification when there
  are (more) new notifications since the last check.
- **Open links to new items on clicking balloon:** click on the balloon popup
  to open the pages associated with the items listed in the popup.
- **Single icon mode:** show only the count when there are unread notifications
  instead of both the count and mail icon.
- **Enable anti-flapping (experimental):** when Steam Community is down for
  maintenance, it often returns zero notifications one poll, then the proper
  count the next poll. This can get annoying if you have balloon popups
  enabled. This feature attempts to reduce that, by requiring a few zero-
  notifications polls before actually setting notification counts to zero.
- **Reset:** resets settings to defaults.
- **Log Out:** clears authentication tokens and stops checking for
  notifications.

Items
-----
Here, you can choose which items are always visible on the popup menu.

Colors
------
These are the colors used in rendering the icon and the popup menu. You can
customize them to your liking.

- **No notifications:** the color used in the tray icon when there are no
  notifications. This is transparent by default.
- **Unread notifications:** the color used in the tray icon when there are
  unread notifications.
- **New notifications:** the color used in the tray icon when there are new
  notifications since you last opened the popup menu.
- **Notification item text:** the color of an item with 0 count on the popup.
- **Unread notification item text:** the color of an item with non-0 count
  on the popup.
- **Popup background:** the color of the background of the popup menu.
- **Popup border:** the color of the border of the popup menu.
- **Popup item focused:** the color of the highlighting when you hover over
  a notification item in the popup menu.
- **Popup item separator:** the color of the separator between notification
  items on the popup menu.

About login security
====================
Originally, it was planned that Internet Explorer's cookies would be used for
authenticating with Steam servers. However, that turned out to be a bit messy
and didn't really work, so I implemented my own authentication system based
off of Steam Community's login system. This program only stores your Steam
ID, "remember login" token, and machine auth token, just like what a regular
browser would store. It's stored along with the rest of the app settings, in
a .NET Framework defined location, and is encrypted using DPAPI. If you would
like a bit of extra security against someone stealing your credentials, you
can recompile the program with your own strong name signing key, which will
modify the encryption on the credentials.

Bugs and suggestions
====================
Please use GitHub's Issues tab for tracking.
