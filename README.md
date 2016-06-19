# Anathema

Anathema Engine

Anathema is a modern single player game hacking engine designed to make cheating in games easy. Within minutes, create cheats such as:

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

Anathema has all of the traditional memory scanning features with added touches, including the ability to scan with multiple constraints at once, such as a value that has both increased and is greater than zero.

==Automated Memory Scanning==

Automatically discover hacks without doing all of the work. Let Anathema correlate your key presses with memory changes to automatically find useful cheats, such as the player's position or ammo.

Anathema also features several other automated scanning features, such as a Chunk Scanner to quickly locate useful memory, or the Change Counter to determine what memory changes frequently, or infrequently.

==LUA Scripting==

Write sophisticated hacks with minimal effort using the user friendly scripting language LUA. Leverage the power of the FASM compiler to write your own code to replace the code in the game on the fly. 

==RELEASE STEPS==

1) Update version number for Anathema in Properties -> AssemblyInfo as well as in OneClick publish settings.


REPEAT FOR x86 and x64:

2) Compile for {x86/x64}.

3) Obfuscate executables in bin/{x86/x64}/Release using .Net Reactor.

4) Replace the executables in bin/{x86/x64}/Release AND obj/{x86/x64}/Release.

5) Publish for {x86/x64} in OneClick to the proper AnathemaWeb {x86/x64} release folder. ENSURE BUILD DOES NOT RECOMPILE -- otherwise we will lose obfuscation changes.


6) Ensure published assemblies are obfuscated.

7) Push assemblies to develop/master branch, deploy to elastic beanstalk.