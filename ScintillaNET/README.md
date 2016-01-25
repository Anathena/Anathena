# ScintillaNET

ScintillaNET is a Windows Forms control, wrapper, and bindings for the versatile [Scintilla](http://www.scintilla.org/) source code editing component.

> "As well as features found in standard text editing components, Scintilla includes features especially useful when editing and debugging source code. These include support for syntax styling, error indicators, code completion and call tips. The selection margin can contain markers like those used in debuggers to indicate breakpoints and the current line. Styling choices are more open than with many editors, allowing the use of proportional fonts, bold and italics, multiple foreground and background colours and multiple fonts." -- [scintilla.org](http://www.scintilla.org/)

ScintillaNET can also be used with WPF using the <a href="https://msdn.microsoft.com/en-us/library/system.windows.forms.integration.windowsformshost(v=vs.110).aspx">WindowsFormsHost</a>.

### Project Status

ScintillaNET is in active development. If you find any issues or just have a question feel free to use the [Issues](https://github.com/jacobslusser/ScintillaNET/issues) feature at our GitHub page.

Compiled versions which are production ready can be downloaded from [NuGet](https://www.nuget.org/packages/jacobslusser.ScintillaNET) or the [Releases](https://github.com/jacobslusser/ScintillaNET/releases) page.

For the latest and greatest you can build the [Master](https://github.com/jacobslusser/ScintillaNET/archive/master.zip) branch from source using Visual Studio 2013.

## Background

This project is a rewrite of the [ScintillaNET project hosted at CodePlex](http://scintillanet.codeplex.com/) and maintained by myself and others. After many years of contributing to that project I decided to think differently about the API we had created and felt I could make better one if I was willing to go back to a blank canvas. Thus, this project is the spiritual successor to the original ScintillaNET but has been written from scratch.

### First Class Characters

One of the issues that ScintillaNET has historically suffered from is the fact that the native Scintilla control operates on bytes, not characters. Prior versions of ScintillaNET did not account for this, and when you're dealing with Unicode, [one byte doesn't always equal one character](http://www.joelonsoftware.com/articles/Unicode.html). The result was an API that sometimes expected byte offsets and at other times expected character offsets. Sometimes things would work as expected and other times random failures and out-of-range exceptions would occur.

No more. **One of the major focuses of this rewrite was to give ScintillaNET an understanding of Unicode from the ground up.** Every API now consistently works with character-based offsets and ranges just like .NET developers expect. Internally we maintain a mapping of character to byte offsets (and vice versa) and do all the translation for you so you never need to worry about it. No more out-of-range exceptions. No more confusion. No more pain. It just works.

### One Library

The second most popular ScintillaNET issue was confusion distributing the ScintillaNET DLL and its native component, the SciLexer DLL. ScintillaNET is a wrapper. Without the SciLexer.dll containing the core Scintilla functionality it is nothing. As a native component, SciLexer.dll has to be compiled separately for 32 and 64-bit versions of Windows. So it was actually three DLLs that developers had to ship with their applications.

This proved a pain point because developers often didn't want to distribute so many libraries or wanted to place them in alternate locations which would break the DLL loading mechanisms used by PInvoke and ScintillaNET. It also causes headaches during design-time in Visual Studio for the same reasons.

To address this ScintillaNET now embeds a 32 and 64-bit version of SciLexer.dll in the ScintillaNET DLL. **Everything you need to run ScintillaNET in one library.** In addition to soothing the pain mentioned above this now makes it possible for us to create a ScintillaNET NuGet package.

### Keeping it Consistent

Another goal of the rewrite was to accept the original Scintilla API for what it is and not try to coerce it into a .NET-style API when it should not or could not be. A good example of this is how ScintillaNET uses indexers to access lines, but not treat them as a .NET collection. Lines in a Scintilla control are not items in a collection. There is no API to Add, Insert, or Remove a line in Scintilla and thus we don't try to create one in ScintillaNET. These deviations from .NET convention are rare, but are done to keep any native Scintilla documentation relevant to the managed wrapper and to avoid situations where trying to force the original API into a more familiar one is more detrimental than helpful.

*NOTE: This is not to say that ScintillaNET cannot add, insert, or remove lines. Those operations, however, are handled as text changes, not line changes.*

## Documentation

Complete API documentation is included with all of our packages. In addition there is extensive documentation at the project [Wiki](https://github.com/jacobslusser/ScintillaNET/wiki) which has recipes for common tasks and questions. If you're new to ScintillaNET, the Wiki is a good place to get started.

As previously noted in the project charter, great effort has been made to keep the ScintillaNET API consist with the native Scintilla API. As such, the [native Scintilla documentation](http://www.scintilla.org/ScintillaDoc.html) continues to be a valuable resource for learning some of the deeper features.

### Conventions

Generally speaking, their API will map to ours in the following ways:

+ A call that has an associated 'get' and 'set' such as `SCI_GETTEXT` and `SCI_SETTEXT(value)`, will map to a similarly named property such as `Text`.
+ A call that requires a number argument to access an item in a 'collection' such as `SCI_INDICSETFORE(indicatorNumber, ...)` or `SCI_STYLEGETSIZE(styleNumber, ...)`, will be accessed through an indexer such as `Indicators[0].ForeColor` or `Styles[0].Size`.

The native Scintilla control has a habit of clamping input values to within acceptable ranges rather than throwing exceptions and so we've kept that behavior in ScintillaNET. For example, the `GotoPosition` method requires a character `position` argument. If that value is less than zero or past the end of the document it will be clamped to either `0` or the `TextLength` rather than throw an `OutOfRangeException`. This tends to result in less exceptions, but the same desired outcome.

## License

The MIT License (MIT)

Copyright (c) 2016, Jacob Slusser, https://github.com/jacobslusser

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
