# Indicators

Indicators are used to display additional information over the top of styling. They can be used to show, for example, syntax errors, deprecated names and bad indentation by drawing underlines under text or boxes around text.

In many ways, configuring indicators is like configuring styles. The `Scintilla.Indicators` collection allows up to 32 different indicator definitions (0-31). The indexes 0-7 are reserved for use by lexers and so unless you are sure the lexer you are using doesn't require them, it's best to avoid that range.

The following is a non-exhaustive list of the most commonly used `Indicator` properties:

- `Style` - defines the indicator appearance (and sometimes behavior)
- `ForeColor` - the indicator color
- `Alpha` and `OutlineAlpha` - for indicators which use a box `Style`, define the transparency of the `ForeColor` for that box and box outline
- `Under` - determines whether indicators are drawn over or under the text
- `HoverForeColor` - the indicator color used when the caret is within range or the mouse is over
- `HoverStyle` - the style definition index within `Scintilla.Styles` to use when the caret is within range or the mouse is over

The `HoverForeColor` and `HoverStyle` properties cannot be used at the same time. Setting one will reset the other.

## Applying Indicators

Indicators are applied by first setting the `Scintilla.IndicatorCurrent` property to an indicator definition index (0-31). Once set you can repeatedly call the `Scintilla.IndicatorFillRange` method to tag ranges of text with that indicator.

```cs
// Define an indicator
scintilla.Indicators[8].Style = IndicatorStyle.Squiggle;
scintilla.Indicators[8].ForeColor = Color.Red;

// Get ready for fill
scintilla.IndicatorCurrent = 8;

// Fill ranges
scintilla.IndicatorFillRange(2, 5);
scintilla.IndicatorFillRange(25, 33);
// etc...
```

To remove an indicator from a given range, use the `IndicatorClearRange` method.

## Events

You can be notified when a user clicks on text that is tagged with an indicator by listening to the `IndicatorClick` and `IndicatorRelease` events. This can be use, for example, to provide the user with spell correct if you used indicators to mark misspelled words.

A somewhat less useful event is `ChangeIndicator`. This will notify you when an indicator has been "filled" or "cleared". Presumably, however, you wouldn't need to be notified about this because you just called `IndicatorFillRange` or `IndicatorClearRange`.

## Navigating Indicators

It may be useful a times to iterate through text which has been tagged with a specific indicator. The `Indicator.Start` and `Indicator.End` methods allow you to do this. Using these methods, however, can be tricky. Each accepts a document position as an input argument and depending on whether that document position is using the indicator in question, the result of these methods will either tell you where the use of that indicator starts and stops or where the *absence* of that indicator starts and stops. Thus, depending on where you start your search you might get the complete opposite answer from what you wanted. Instead of learning where the indicator is, you will be learning where it is not.

The `Scintilla.IndicatorAllOnFor` method, when used in conjunction with `Indicator.Start` and `Indicator.End`, can help solve this problem. With it you can test wherther a position uses a specific indicator. It does this by returning a bitmap. Each bit in the map corresponds to one of the 32 possible indicator indexes.

Putting that all together, we can iterate through all the cleared and filled ranges for a given indicator like this:

```cs
var textLength = scintilla.TextLength;
var indicator = scintilla.Indicators[8];
var bitmapFlag = (1 << indicator.Index);

var startPos = 0;
var endPos = 0;

do
{
    startPos = indicator.Start(endPos);
    endPos = indicator.End(startPos);

    // Is this range filled with our indicator (8)?
    var bitmap = scintilla.IndicatorAllOnFor(startPos);
    var filled = ((bitmapFlag & bitmap) == bitmapFlag);
    if (filled)
    {
        // Do stuff with indicator range
        Debug.WriteLine(scintilla.GetTextRange(startPos, (endPos - startPos)));
    }

} while (endPos != 0 && endPos < textLength);
```

## Indicator Values

A somewhat advanced feature is the ability for a range of text tagged with an indicator to also store an arbitrary integer value. Similar to the `Scintilla.IndicatorCurrent` property is a `Scintila.IndicatorValue` property. As just mentioned, the value specified here is completely user-defined and will also be stored along with the tagged text when using the `IndicatorFillRange` method. The value stored at these ranges can then be retrieved by calling the `IndicatorValueAt` method.

TODO