using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Actions which can be performed by the application or bound to keys in a <see cref="Scintilla" /> control.
    /// </summary>
    public enum Command
    {
        /// <summary>
        /// When bound to keys performs the standard platform behavior.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Performs no action and when bound to keys prevents them from propagating to the parent window.
        /// </summary>
        Null = NativeMethods.SCI_NULL,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret down one line.
        /// </summary>
        LineDown = NativeMethods.SCI_LINEDOWN,

        /// <summary>
        /// Extends the selection down one line.
        /// </summary>
        LineDownExtend = NativeMethods.SCI_LINEDOWNEXTEND,

        /// <summary>
        /// Extends the rectangular selection down one line.
        /// </summary>
        LineDownRectExtend = NativeMethods.SCI_LINEDOWNRECTEXTEND,

        /// <summary>
        /// Scrolls down one line.
        /// </summary>
        LineScrollDown = NativeMethods.SCI_LINESCROLLDOWN,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret up one line.
        /// </summary>
        LineUp = NativeMethods.SCI_LINEUP,

        /// <summary>
        /// Extends the selection up one line.
        /// </summary>
        LineUpExtend = NativeMethods.SCI_LINEUPEXTEND,

        /// <summary>
        /// Extends the rectangular selection up one line.
        /// </summary>
        LineUpRectExtend = NativeMethods.SCI_LINEUPRECTEXTEND,

        /// <summary>
        /// Scrolls up one line.
        /// </summary>
        LineScrollUp = NativeMethods.SCI_LINESCROLLUP,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret down one paragraph.
        /// </summary>
        ParaDown = NativeMethods.SCI_PARADOWN,

        /// <summary>
        /// Extends the selection down one paragraph.
        /// </summary>
        ParaDownExtend = NativeMethods.SCI_PARADOWNEXTEND,

        /// <summary>
        /// Moves the caret up one paragraph.
        /// </summary>
        ParaUp = NativeMethods.SCI_PARAUP,

        /// <summary>
        /// Extends the selection up one paragraph.
        /// </summary>
        ParaUpExtend = NativeMethods.SCI_PARAUPEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret left one character.
        /// </summary>
        CharLeft = NativeMethods.SCI_CHARLEFT,

        /// <summary>
        /// Extends the selection left one character.
        /// </summary>
        CharLeftExtend = NativeMethods.SCI_CHARLEFTEXTEND,

        /// <summary>
        /// Extends the rectangular selection left one character.
        /// </summary>
        CharLeftRectExtend = NativeMethods.SCI_CHARLEFTRECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret right one character.
        /// </summary>
        CharRight = NativeMethods.SCI_CHARRIGHT,

        /// <summary>
        /// Extends the selection right one character.
        /// </summary>
        CharRightExtend = NativeMethods.SCI_CHARRIGHTEXTEND,

        /// <summary>
        /// Extends the rectangular selection right one character.
        /// </summary>
        CharRightRectExtend = NativeMethods.SCI_CHARRIGHTRECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the start of the previous word.
        /// </summary>
        WordLeft = NativeMethods.SCI_WORDLEFT,

        /// <summary>
        /// Extends the selection to the start of the previous word.
        /// </summary>
        WordLeftExtend = NativeMethods.SCI_WORDLEFTEXTEND,

        /// <summary>
        /// Moves the caret to the start of the next word.
        /// </summary>
        WordRight = NativeMethods.SCI_WORDRIGHT,

        /// <summary>
        /// Extends the selection to the start of the next word.
        /// </summary>
        WordRightExtend = NativeMethods.SCI_WORDRIGHTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the end of the previous word.
        /// </summary>
        WordLeftEnd = NativeMethods.SCI_WORDLEFTEND,

        /// <summary>
        /// Extends the selection to the end of the previous word.
        /// </summary>
        WordLeftEndExtend = NativeMethods.SCI_WORDLEFTENDEXTEND,

        /// <summary>
        /// Moves the caret to the end of the next word.
        /// </summary>
        WordRightEnd = NativeMethods.SCI_WORDRIGHTEND,

        /// <summary>
        /// Extends the selection to the end of the next word.
        /// </summary>
        WordRightEndExtend = NativeMethods.SCI_WORDRIGHTENDEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the previous word segment (case change or underscore).
        /// </summary>
        WordPartLeft = NativeMethods.SCI_WORDPARTLEFT,

        /// <summary>
        /// Extends the selection to the previous word segment (case change or underscore).
        /// </summary>
        WordPartLeftExtend = NativeMethods.SCI_WORDPARTLEFTEXTEND,

        /// <summary>
        /// Moves the caret to the next word segment (case change or underscore).
        /// </summary>
        WordPartRight = NativeMethods.SCI_WORDPARTRIGHT,

        /// <summary>
        /// Extends the selection to the next word segment (case change or underscore).
        /// </summary>
        WordPartRightExtend = NativeMethods.SCI_WORDPARTRIGHTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the start of the line.
        /// </summary>
        Home = NativeMethods.SCI_HOME,

        /// <summary>
        /// Extends the selection to the start of the line.
        /// </summary>
        HomeExtend = NativeMethods.SCI_HOMEEXTEND,

        /// <summary>
        /// Extends the rectangular selection to the start of the line.
        /// </summary>
        HomeRectExtend = NativeMethods.SCI_HOMERECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the start of the display line.
        /// </summary>
        HomeDisplay = NativeMethods.SCI_HOMEDISPLAY,

        /// <summary>
        /// Extends the selection to the start of the display line.
        /// </summary>
        HomeDisplayExtend = NativeMethods.SCI_HOMEDISPLAYEXTEND,

        /// <summary>
        /// Moves the caret to the start of the display or document line.
        /// </summary>
        HomeWrap = NativeMethods.SCI_HOMEWRAP,

        /// <summary>
        /// Extends the selection to the start of the display or document line.
        /// </summary>
        HomeWrapExtend = NativeMethods.SCI_HOMEWRAPEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the first non-whitespace character of the line.
        /// </summary>
        VcHome = NativeMethods.SCI_VCHOME,

        /// <summary>
        /// Extends the selection to the first non-whitespace character of the line.
        /// </summary>
        VcHomeExtend = NativeMethods.SCI_VCHOMEEXTEND,

        /// <summary>
        /// Extends the rectangular selection to the first non-whitespace character of the line.
        /// </summary>
        VcHomeRectExtend = NativeMethods.SCI_VCHOMERECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the first non-whitespace character of the display or document line.
        /// </summary>
        VcHomeWrap = NativeMethods.SCI_VCHOMEWRAP,

        /// <summary>
        /// Extends the selection to the first non-whitespace character of the display or document line.
        /// </summary>
        VcHomeWrapExtend = NativeMethods.SCI_VCHOMEWRAPEXTEND,

        /// <summary>
        /// Moves the caret to the first non-whitespace character of the display line.
        /// </summary>
        VcHomeDisplay = NativeMethods.SCI_VCHOMEDISPLAY,

        /// <summary>
        /// Extends the selection to the first non-whitespace character of the display line.
        /// </summary>
        VcHomeDisplayExtend = NativeMethods.SCI_VCHOMEDISPLAYEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the end of the document line.
        /// </summary>
        LineEnd = NativeMethods.SCI_LINEEND,

        /// <summary>
        /// Extends the selection to the end of the document line.
        /// </summary>
        LineEndExtend = NativeMethods.SCI_LINEENDEXTEND,

        /// <summary>
        /// Extends the rectangular selection to the end of the document line.
        /// </summary>
        LineEndRectExtend = NativeMethods.SCI_LINEENDRECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the end of the display line.
        /// </summary>
        LineEndDisplay = NativeMethods.SCI_LINEENDDISPLAY,

        /// <summary>
        /// Extends the selection to the end of the display line.
        /// </summary>
        LineEndDisplayExtend = NativeMethods.SCI_LINEENDDISPLAYEXTEND,

        /// <summary>
        /// Moves the caret to the end of the display or document line.
        /// </summary>
        LineEndWrap = NativeMethods.SCI_LINEENDWRAP,

        /// <summary>
        /// Extends the selection to the end of the display or document line.
        /// </summary>
        LineEndWrapExtend = NativeMethods.SCI_LINEENDWRAPEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret to the start of the document.
        /// </summary>
        DocumentStart = NativeMethods.SCI_DOCUMENTSTART,

        /// <summary>
        /// Extends the selection to the start of the document.
        /// </summary>
        DocumentStartExtend = NativeMethods.SCI_DOCUMENTSTARTEXTEND,

        /// <summary>
        /// Moves the caret to the end of the document.
        /// </summary>
        DocumentEnd = NativeMethods.SCI_DOCUMENTEND,

        /// <summary>
        /// Extends the selection to the end of the document.
        /// </summary>
        DocumentEndExtend = NativeMethods.SCI_DOCUMENTENDEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret up one page.
        /// </summary>
        PageUp = NativeMethods.SCI_PAGEUP,

        /// <summary>
        /// Extends the selection up one page.
        /// </summary>
        PageUpExtend = NativeMethods.SCI_PAGEUPEXTEND,

        /// <summary>
        /// Extends the rectangular selection up one page.
        /// </summary>
        PageUpRectExtend = NativeMethods.SCI_PAGEUPRECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret down one page.
        /// </summary>
        PageDown = NativeMethods.SCI_PAGEDOWN,

        /// <summary>
        /// Extends the selection down one page.
        /// </summary>
        PageDownExtend = NativeMethods.SCI_PAGEDOWNEXTEND,

        /// <summary>
        /// Extends the rectangular selection down one page.
        /// </summary>
        PageDownRectExtend = NativeMethods.SCI_PAGEDOWNRECTEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret up one window or page.
        /// </summary>
        StutteredPageUp = NativeMethods.SCI_STUTTEREDPAGEUP,

        /// <summary>
        /// Extends the selection up one window or page.
        /// </summary>
        StutteredPageUpExtend = NativeMethods.SCI_STUTTEREDPAGEUPEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the caret down one window or page.
        /// </summary>
        StutteredPageDown = NativeMethods.SCI_STUTTEREDPAGEDOWN,

        /// <summary>
        /// Extends the selection down one window or page.
        /// </summary>
        StutteredPageDownExtend = NativeMethods.SCI_STUTTEREDPAGEDOWNEXTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Deletes the character left of the caret.
        /// </summary>
        DeleteBack = NativeMethods.SCI_DELETEBACK,

        /// <summary>
        /// Deletes the character (excluding line breaks) left of the caret.
        /// </summary>
        DeleteBackNotLine = NativeMethods.SCI_DELETEBACKNOTLINE,

        // --------------------------------------------------------------------

        /// <summary>
        /// Deletes from the caret to the start of the previous word.
        /// </summary>
        DelWordLeft = NativeMethods.SCI_DELWORDLEFT,

        /// <summary>
        /// Deletes from the caret to the start of the next word.
        /// </summary>
        DelWordRight = NativeMethods.SCI_DELWORDRIGHT,

        /// <summary>
        /// Deletes from the caret to the end of the next word.
        /// </summary>
        DelWordRightEnd = NativeMethods.SCI_DELWORDRIGHTEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Deletes the characters left of the caret to the start of the line.
        /// </summary>
        DelLineLeft = NativeMethods.SCI_DELLINELEFT,

        /// <summary>
        /// Deletes the characters right of the caret to the start of the line.
        /// </summary>
        DelLineRight = NativeMethods.SCI_DELLINERIGHT,

        /// <summary>
        /// Deletes the current line.
        /// </summary>
        LineDelete = NativeMethods.SCI_LINEDELETE,

        // --------------------------------------------------------------------

        /// <summary>
        /// Removes the current line and places it on the clipboard.
        /// </summary>
        LineCut = NativeMethods.SCI_LINECUT,

        /// <summary>
        /// Copies the current line and places it on the clipboard.
        /// </summary>
        LineCopy = NativeMethods.SCI_LINECOPY,

        /// <summary>
        /// Transposes the current and previous lines.
        /// </summary>
        LineTranspose = NativeMethods.SCI_LINETRANSPOSE,

        /// <summary>
        /// Duplicates the current line.
        /// </summary>
        LineDuplicate = NativeMethods.SCI_LINEDUPLICATE,

        // --------------------------------------------------------------------

        /// <summary>
        /// Converts the selection to lowercase.
        /// </summary>
        Lowercase = NativeMethods.SCI_LOWERCASE,

        /// <summary>
        /// Converts the selection to uppercase.
        /// </summary>
        Uppercase = NativeMethods.SCI_UPPERCASE,

        /// <summary>
        /// Cancels autocompletion, calltip display, and drops any additional selections.
        /// </summary>
        Cancel = NativeMethods.SCI_CANCEL,

        /// <summary>
        /// Toggles overtype. See <see cref="Scintilla.Overtype" />.
        /// </summary>
        EditToggleOvertype = NativeMethods.SCI_EDITTOGGLEOVERTYPE,

        // --------------------------------------------------------------------

        /// <summary>
        /// Inserts a newline character.
        /// </summary>
        NewLine = NativeMethods.SCI_NEWLINE,

        /// <summary>
        /// Inserts a form feed character.
        /// </summary>
        FormFeed = NativeMethods.SCI_FORMFEED,

        /// <summary>
        /// Adds a tab (indent) character.
        /// </summary>
        Tab = NativeMethods.SCI_TAB,

        /// <summary>
        /// Removes a tab (indent) character from the start of a line.
        /// </summary>
        BackTab = NativeMethods.SCI_BACKTAB,

        // --------------------------------------------------------------------

        /// <summary>
        /// Duplicates the current selection.
        /// </summary>
        SelectionDuplicate = NativeMethods.SCI_SELECTIONDUPLICATE,

        /// <summary>
        /// Moves the caret vertically to the center of the screen.
        /// </summary>
        VerticalCenterCaret = NativeMethods.SCI_VERTICALCENTRECARET,

        // --------------------------------------------------------------------

        /// <summary>
        /// Moves the selected lines up.
        /// </summary>
        MoveSelectedLinesUp = NativeMethods.SCI_MOVESELECTEDLINESUP,

        /// <summary>
        /// Moves the selected lines down.
        /// </summary>
        MoveSelectedLinesDown = NativeMethods.SCI_MOVESELECTEDLINESDOWN,

        // --------------------------------------------------------------------

        /// <summary>
        /// Scrolls to the start of the document without changing the selection.
        /// </summary>
        ScrollToStart = NativeMethods.SCI_SCROLLTOSTART,

        /// <summary>
        /// Scrolls to the end of the document without changing the selection.
        /// </summary>
        ScrollToEnd = NativeMethods.SCI_SCROLLTOEND,

        // --------------------------------------------------------------------

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.ZoomIn" />.
        /// </summary>
        ZoomIn = NativeMethods.SCI_ZOOMIN,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.ZoomOut" />.
        /// </summary>
        ZoomOut = NativeMethods.SCI_ZOOMOUT,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.Undo" />.
        /// </summary>
        Undo = NativeMethods.SCI_UNDO,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.Redo" />.
        /// </summary>
        Redo = NativeMethods.SCI_REDO,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.SwapMainAnchorCaret" />
        /// </summary>
        SwapMainAnchorCaret = NativeMethods.SCI_SWAPMAINANCHORCARET,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.RotateSelection" />
        /// </summary>
        RotateSelection = NativeMethods.SCI_ROTATESELECTION,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.MultipleSelectAddNext" />
        /// </summary>
        MultipleSelectAddNext = NativeMethods.SCI_MULTIPLESELECTADDNEXT,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.MultipleSelectAddEach" />
        /// </summary>
        MultipleSelectAddEach = NativeMethods.SCI_MULTIPLESELECTADDEACH,

        /// <summary>
        /// Command equivalent to <see cref="Scintilla.SelectAll" />
        /// </summary>
        SelectAll = NativeMethods.SCI_SELECTALL
    }
}
