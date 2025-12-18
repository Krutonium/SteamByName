# Steam By-Name

This simple console application (that you can run as as service if you want!) automatically parses your steam library, including off-disk libraries, and creates a `by-name` directory inside your main `compatdata` directory. Inside of that folder will be a bunch of symlinks to each games folder, normally named by AppID, of games that are recognized. 

On my system, this takes the form of 
```
~/.local/Share/Steam/steamapps/compatdata/by-name 
✔ >>  file VRChat
VRChat: symbolic link to /games/SteamLibrary/steamapps/compatdata/438100
```

Which allows you to quickly and without needing to look up a games AppID; traverse to the games compatdata folder, for installing mods or other such needs.

I hope this helps you out if you need it, I know it's going to save me a bunch of time myself.

Yes, this is a Linux Only issue.
### Requirements:

.NET 10  
That's pretty much it. No Libraries were used, just some pattern matching.
