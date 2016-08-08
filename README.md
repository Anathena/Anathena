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

3) Obfuscate executables in bin/Release using .Net Reactor

4) Replace the main executable in obj/Release

5) Publish for Release in OneClick to the proper AnathenaWeb release folder. ENSURE BUILD DOES NOT RECOMPILE -- otherwise we will lose obfuscation changes. If there are any changes to the Anathena properties page, they will be auto-saved, and the project will recompile and one will have to start again.

6) Ensure published assemblies are obfuscated (file size check in publish folder, as well as checking that no rebuild has started will suffice)

7) Push assemblies to develop/master branch, deploy to elastic beanstalk.