# Anathena

Anathena

Anathena is a modern single player game hacking engine designed to make cheating in games easy. Within minutes, create cheats such as:

Godmode

Teleportation

Infinite Money

Infinite Ammo

Increased Speed

Unlimited Stat Points

Item Duplication


And many, many more.

==Browse and Share Cheats==

Search for cheats that others have already discovered, or discover your own and share them with other members of the community. This process is made simple and easy, which is perfect for those with minimal experiance that would prefer to use cheats written by experts.

==Traditional Memory Scanning Features==

Anathena has all of the traditional memory scanning features with added touches, including the ability to scan with multiple constraints at once, such as a value that has both increased and is greater than zero.

==Automated Memory Scanning==

Automatically discover hacks without doing all of the work. Let Anathena correlate your key presses with memory changes to automatically find useful cheats, such as the player's position or ammo.

Anathena also features several other automated scanning features, such as a Chunk Scanner to quickly locate useful memory, or the Change Counter to determine what memory changes frequently, or infrequently.

==LUA Scripting==

Write sophisticated hacks with minimal effort using the user friendly scripting language LUA. Leverage the power of the FASM compiler to write your own code to replace the code in the game on the fly. 

==RELEASE STEPS==

1) Update version number for Anathena in Properties -> AssemblyInfo, and for Anathena.csproj Publish settings under Publish version AND in the Updates button menu

2) Compile for Release as AnyCPU

3) Publish for Release in OneClick to the proper AnathenaWeb release folder

4) Manually copy over every single SharpDx DLL, and delete the existing '.deploy' SharpDx DLLs. Add '.deploy' to the copied over SharpDx DLLs. You can thank microsoft for this, SharpCli.exe will patch the SharpDx DLLs, but OneClick creates the published files before they get patched.

5) Push assemblies to develop/master branch, deploy to site