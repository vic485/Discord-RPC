Discord-RPC

Archiving - Can't get IL2CPP builds to work consitently. Worked fine while following pull requests before Discord published their updates, but now occasionally errors in builds or crashes the discord client.
---

I will write proper docs when the 2nd Sykoo game jam is over.
Until then if you need help contact me on Discord (vic485#0001).

Basic instructions:
Step 0 - Setting up an app.

Navigate to [https://discordapp.com/developers](https://discordapp.com/developers)
Under My Apps click create new app and give it the name of your 
game. Make note of the client id. You will need this later.

Step 1 - Setting up the project

Download the latest windows zip file from [here](https://github.com/discordapp/discord-rpc/releases)
Note: If you want to output to other OSes download their files, but
you may need to compile them yourself, and you can only have one
set of DLL files active at a time. !Unload and change dll files
BEFORE changing your build settings!

Under your project's base assets folder, create a folder called "Plugins"
Inside this folder create two more folders "x86" and "x86_64"
(You should now have "Assets/Plugins/x86/" and "Assets/Plugins/x86_64/"

Place each discord-rpc.dll in it's respective folder.
(win32-dynamic goes in x86 and win64-dynamic goes in x86_64)

In the inspector change the platform settings for each dll file.
The dll in x86 should only have x86 checked and the dll in x86_64
should only have x86_64 checked.
!!YOU WILL GET ERRORS WHEN YOU TRY TO PLAY IF YOU DO NOT DO THIS!!

Step 2 - Creating the manager

I would recommend doing this early on in the game (such as the splash screens or main menu)
as it can take a few seconds to connect to discord and for the
presence to show up on your profile.

Create an empty gameobject in your scene and name it PresenceManager
Add the PresenceManager script to the object. From the add component menu,
this is located under Scripts->DiscordPresence->PresenceManager

Put the client id from step 0 into the Application ID box, and set 
initial states for your game. (You should only need to change state,
details, and image texts/keys. Everything else should be their default 
values at start and changed via script at runtime unless otherwise necessary.)

Step 3 - Updating presence in game

Scripts needing to update rich presence will need:
using DiscordPresence;
at the top.

To update the presence call:
PresenceManager.UpdatePresence();
and pass it the parameters you want to change.

PresenceManager.ClearPresence();
will change everything back to default values.

PresenceManager.ClearAndUpdate();
will do the same as ClearPresence AND tell discord to
remove the values of the presence. ONLY do this when
necessary as discord will only let you change presence
at most ONCE EVERY FIFTEEN SECONDS.


And you should be good to go. Make sure to read through the
FAQ and Best Practices and everything under Rich Presence on
the discord developers page. This will give you a lot of
information and may help if I missed some stuff here until
I get the proper docs written.

If you would like to improve stuff or contribute, please feel
free to fork the code and create a pull request. Also contact
me on Discord so I know what's going on/can add you as a 
contributor.

- vic485#0001
