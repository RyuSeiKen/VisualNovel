INCLUDE everydayChoices
INCLUDE narativeFunctions

Hello world
{ActorEnterScene("someActor", "someEntry")}
#tag1 #tag:2 #tag3
The ink runtime engine is no longer compiled directly into inklecate (the ink compiler), and is instead a separate DLL. This required some tweaks to the structure of how the Unity plugin is structured slightly, though hopefully shouldn't affect anything noticeably!
# actor : princess
#audio : 521
Big optimisation for when you do a large contentless calculation after a line of content (ending in a newline).
Force choices to always come after content - this was possible to get around previously with threads.
//{ActorSpeaker("someActor")}

* [Choice A] A
* [Choice B] B
* [Choice C] C
- <> chosed.
->END
