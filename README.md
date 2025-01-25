# AutoRespawn
The AutoRespawn plugin is a simple, easy-to-use tool for Unturned servers. It automatically respawns players after a set delay if they don’t respawn manually, keeping the game smooth and hassle-free.

What You Can Do:
Set Respawn Delay: Choose how many seconds players have before they’re auto-respawned.
Change Message Color: Pick a color for the notification to match your server style.
Customize Icon: Add a custom icon link to make the notification unique.
```
Example Configuration:
xml
Copy
Edit
<?xml version="1.0" encoding="utf-8"?>
<AutoRespawnConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Delay>20</Delay>
  <MessageColor>green</MessageColor>
  <MessageIcon>https://i.imgur.com/JZjQEHV.png</MessageIcon>
</AutoRespawnConfiguration>
```
