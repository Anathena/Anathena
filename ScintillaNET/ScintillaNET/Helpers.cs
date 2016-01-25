using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    internal static class Helpers
    {
        #region Fields

        private static bool registeredFormats;
        private static uint CF_HTML;
        private static uint CF_RTF;
        private static uint CF_LINESELECT;
        private static uint CF_VSLINETAG;

        #endregion Fields

        #region Methods

        public static unsafe byte[] BitmapToArgb(Bitmap image)
        {
            // This code originally used Image.LockBits and some fast byte copying, however, the endianness
            // of the image formats was making my brain hurt. For now I'm going to use the slow but simple
            // GetPixel approach.

            var bytes = new byte[4 * image.Width * image.Height];

            var i = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var color = image.GetPixel(x, y);
                    bytes[i++] = color.R;
                    bytes[i++] = color.G;
                    bytes[i++] = color.B;
                    bytes[i++] = color.A;
                }
            }

            return bytes;
        }

        public static unsafe byte[] ByteToCharStyles(byte* styles, byte* text, int length, Encoding encoding)
        {
            // This is used by annotations and margins to get all the styles in one call.
            // It converts an array of styles where each element corresponds to a BYTE
            // to an array of styles where each element corresponds to a CHARACTER.

            var bytePos = 0; // Position within text BYTES and style BYTES (should be the same)
            var charPos = 0; // Position within style CHARACTERS
            var decoder = encoding.GetDecoder();
            var result = new byte[encoding.GetCharCount(text, length)];

            while (bytePos < length)
            {
                if (decoder.GetCharCount(text + bytePos, 1, false) > 0)
                    result[charPos++] = *(styles + bytePos); // New char

                bytePos++;
            }

            return result;
        }

        public static unsafe byte[] CharToByteStyles(byte[] styles, byte* text, int length, Encoding encoding)
        {
            // This is used by annotations and margins to style all the text in one call.
            // It converts an array of styles where each element corresponds to a CHARACTER
            // to an array of styles where each element corresponds to a BYTE.

            var bytePos = 0; // Position within text BYTES and style BYTES (should be the same)
            var charPos = 0; // Position within style CHARACTERS
            var decoder = encoding.GetDecoder();
            var result = new byte[length];

            while (bytePos < length && charPos < styles.Length)
            {
                result[bytePos] = styles[charPos];
                if (decoder.GetCharCount(text + bytePos, 1, false) > 0)
                    charPos++; // Move a char

                bytePos++;
            }

            return result;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static int ClampMin(int value, int min)
        {
            if (value < min)
                return min;

            return value;
        }

        public static void Copy(Scintilla scintilla, CopyFormat format, bool useSelection, bool allowLine, int startBytePos, int endBytePos)
        {
            // Plain text
            if ((format & CopyFormat.Text) > 0)
            {
                if (useSelection)
                {
                    if (allowLine)
                        scintilla.DirectMessage(NativeMethods.SCI_COPYALLOWLINE);
                    else
                        scintilla.DirectMessage(NativeMethods.SCI_COPY);
                }
                else
                {
                    scintilla.DirectMessage(NativeMethods.SCI_COPYRANGE, new IntPtr(startBytePos), new IntPtr(endBytePos));
                }
            }

            // RTF and/or HTML
            if ((format & (CopyFormat.Rtf | CopyFormat.Html)) > 0)
            {
                // If we ever allow more than UTF-8, this will have to be revisited
                Debug.Assert(scintilla.DirectMessage(NativeMethods.SCI_GETCODEPAGE).ToInt32() == NativeMethods.SC_CP_UTF8);

                if (!registeredFormats)
                {
                    // Register non-standard clipboard formats.
                    // Scintilla -> ScintillaWin.cxx
                    // NppExport -> HTMLExporter.h
                    // NppExport -> RTFExporter.h

                    CF_LINESELECT = NativeMethods.RegisterClipboardFormat("MSDEVLineSelect");
                    CF_VSLINETAG = NativeMethods.RegisterClipboardFormat("VisualStudioEditorOperationsLineCutCopyClipboardTag");
                    CF_HTML = NativeMethods.RegisterClipboardFormat("HTML Format");
                    CF_RTF = NativeMethods.RegisterClipboardFormat("Rich Text Format");
                    registeredFormats = true;
                }

                var lineCopy = false;
                StyleData[] styles = null;
                List<ArraySegment<byte>> styledSegments = null;

                if (useSelection)
                {
                    var selIsEmpty = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONEMPTY) != IntPtr.Zero;
                    if (selIsEmpty)
                    {
                        if (allowLine)
                        {
                            // Get the current line
                            styledSegments = GetStyledSegments(scintilla, false, true, 0, 0, out styles);
                            lineCopy = true;
                        }
                    }
                    else
                    {
                        // Get every selection
                        styledSegments = GetStyledSegments(scintilla, true, false, 0, 0, out styles);
                    }
                }
                else if (startBytePos != endBytePos)
                {
                    // User-specified range
                    styledSegments = GetStyledSegments(scintilla, false, false, startBytePos, endBytePos, out styles);
                }

                // If we have segments and can open the clipboard
                if (styledSegments != null && styledSegments.Count > 0 && NativeMethods.OpenClipboard(scintilla.Handle))
                {
                    if ((format & CopyFormat.Text) == 0)
                    {
                        // Do the things default (plain text) processing would normally give us
                        NativeMethods.EmptyClipboard();

                        if (lineCopy)
                        {
                            // Clipboard tags
                            NativeMethods.SetClipboardData(CF_LINESELECT, IntPtr.Zero);
                            NativeMethods.SetClipboardData(CF_VSLINETAG, IntPtr.Zero);
                        }
                    }

                    // RTF
                    if ((format & CopyFormat.Rtf) > 0)
                        CopyRtf(scintilla, styles, styledSegments);

                    // HTML
                    if ((format & CopyFormat.Html) > 0)
                        CopyHtml(scintilla, styles, styledSegments);

                    NativeMethods.CloseClipboard();
                }
            }
        }

        private static unsafe void CopyHtml(Scintilla scintilla, StyleData[] styles, List<ArraySegment<byte>> styledSegments)
        {
            // NppExport -> NppExport.cpp
            // NppExport -> HTMLExporter.cpp
            // http://blogs.msdn.com/b/jmstall/archive/2007/01/21/html-clipboard.aspx
            // http://blogs.msdn.com/b/jmstall/archive/2007/01/21/sample-code-html-clipboard.aspx
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015.aspx

            try
            {
                long pos = 0;
                byte[] bytes;

                // Write HTML
                using (var ms = new NativeMemoryStream(styledSegments.Sum(s => s.Count)))
                using (var tw = new StreamWriter(ms, new UTF8Encoding(false)))
                {
                    const int INDEX_START_HTML = 23;
                    const int INDEX_START_FRAGMENT = 65;
                    const int INDEX_END_FRAGMENT = 87;
                    const int INDEX_END_HTML = 41;

                    tw.WriteLine("Version:0.9");
                    tw.WriteLine("StartHTML:00000000");
                    tw.WriteLine("EndHTML:00000000");
                    tw.WriteLine("StartFragment:00000000");
                    tw.WriteLine("EndFragment:00000000");
                    tw.Flush();

                    // Patch header
                    pos = ms.Position;
                    ms.Seek(INDEX_START_HTML, SeekOrigin.Begin);
                    ms.Write((bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8"))), 0, bytes.Length);
                    ms.Seek(pos, SeekOrigin.Begin);

                    tw.WriteLine("<html>");
                    tw.WriteLine("<head>");
                    tw.WriteLine(@"<meta charset=""utf-8"" />");
                    tw.WriteLine(@"<title>ScintillaNET v{0}</title>", scintilla.GetType().Assembly.GetName().Version.ToString(3));
                    tw.WriteLine("</head>");
                    tw.WriteLine("<body>");
                    tw.Flush();

                    // Patch header
                    pos = ms.Position;
                    ms.Seek(INDEX_START_FRAGMENT, SeekOrigin.Begin);
                    ms.Write((bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8"))), 0, bytes.Length);
                    ms.Seek(pos, SeekOrigin.Begin);
                    tw.WriteLine("<!--StartFragment -->");

                    // Write the styles.
                    // We're doing the style tag in the body to include it in the "fragment".
                    tw.WriteLine(@"<style type=""text/css"" scoped="""">");
                    tw.Write("div#segments {");
                    tw.Write(" float: left;");
                    tw.Write(" white-space: pre;");
                    tw.Write(" line-height: {0}px;", scintilla.DirectMessage(NativeMethods.SCI_TEXTHEIGHT, new IntPtr(0)).ToInt32());
                    tw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[Style.Default].BackColor >> 0) & 0xFF, (styles[Style.Default].BackColor >> 8) & 0xFF, (styles[Style.Default].BackColor >> 16) & 0xFF);
                    tw.WriteLine(" }");

                    for (int i = 0; i < styles.Length; i++)
                    {
                        if (!styles[i].Used)
                            continue;

                        tw.Write("span.s{0} {{", i);
                        tw.Write(@" font-family: ""{0}"";", styles[i].FontName);
                        tw.Write(" font-size: {0}pt;", styles[i].SizeF);
                        tw.Write(" font-weight: {0};", styles[i].Weight);
                        if (styles[i].Italic != 0)
                            tw.Write(" font-style: italic;");
                        if (styles[i].Underline != 0)
                            tw.Write(" text-decoration: underline;");
                        tw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[i].BackColor >> 0) & 0xFF, (styles[i].BackColor >> 8) & 0xFF, (styles[i].BackColor >> 16) & 0xFF);
                        tw.Write(" color: #{0:X2}{1:X2}{2:X2};", (styles[i].ForeColor >> 0) & 0xFF, (styles[i].ForeColor >> 8) & 0xFF, (styles[i].ForeColor >> 16) & 0xFF);
                        switch ((StyleCase)styles[i].Case)
                        {
                            case StyleCase.Upper:
                                tw.Write(" text-transform: uppercase;");
                                break;
                            case StyleCase.Lower:
                                tw.Write(" text-transform: lowercase;");
                                break;
                        }

                        if (styles[i].Visible == 0)
                            tw.Write(" visibility: hidden;");
                        tw.WriteLine(" }");
                    }

                    tw.WriteLine("</style>");
                    tw.Write(@"<div id=""segments""><span class=""s{0}"">", Style.Default);
                    tw.Flush();

                    var tabSize = scintilla.DirectMessage(NativeMethods.SCI_GETTABWIDTH).ToInt32();
                    var tab = new string(' ', tabSize);

                    tw.AutoFlush = true;
                    var lastStyle = Style.Default;
                    var unicodeLineEndings = ((scintilla.DirectMessage(NativeMethods.SCI_GETLINEENDTYPESACTIVE).ToInt32() & NativeMethods.SC_LINE_END_TYPE_UNICODE) > 0);
                    foreach (var seg in styledSegments)
                    {
                        var endOffset = seg.Offset + seg.Count;
                        for (int i = seg.Offset; i < endOffset; i += 2)
                        {
                            var ch = seg.Array[i];
                            var style = seg.Array[i + 1];

                            if (lastStyle != style)
                            {
                                tw.Write(@"</span><span class=""s{0}"">", style);
                                lastStyle = style;
                            }

                            switch (ch)
                            {
                                case (byte)'<':
                                    tw.Write("&lt;");
                                    break;

                                case (byte)'>':
                                    tw.Write("&gt;");
                                    break;

                                case (byte)'&':
                                    tw.Write("&amp;");
                                    break;

                                case (byte)'\t':
                                    tw.Write(tab);
                                    break;

                                case (byte)'\r':
                                    if (i + 2 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == (byte)'\n')
                                            i += 2;
                                    }

                                    // Either way, this is a line break
                                    goto case (byte)'\n';

                                case 0xC2:
                                    if (unicodeLineEndings && i + 2 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == 0x85) // NEL \u0085
                                        {
                                            i += 2;
                                            goto case (byte)'\n';
                                        }
                                    }

                                    // Not a Unicode line break
                                    goto default;

                                case 0xE2:
                                    if (unicodeLineEndings && i + 4 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA8) // LS \u2028
                                        {
                                            i += 4;
                                            goto case (byte)'\n';
                                        }
                                        else if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA9) // PS \u2029
                                        {
                                            i += 4;
                                            goto case (byte)'\n';
                                        }
                                    }

                                    // Not a Unicode line break
                                    goto default;

                                case (byte)'\n':
                                    // All your line breaks are belong to us
                                    tw.Write("\r\n");
                                    break;

                                default:

                                    if (ch == 0)
                                    {
                                        // Scintilla behavior is to allow control characters except for
                                        // NULL which will cause the Clipboard to truncate the string.
                                        tw.Write(" "); // Replace with space
                                        break;
                                    }

                                    ms.WriteByte(ch);
                                    break;
                            }
                        }
                    }

                    tw.AutoFlush = false;
                    tw.WriteLine("</span></div>");
                    tw.Flush();

                    // Patch header
                    pos = ms.Position;
                    ms.Seek(INDEX_END_FRAGMENT, SeekOrigin.Begin);
                    ms.Write((bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8"))), 0, bytes.Length);
                    ms.Seek(pos, SeekOrigin.Begin);
                    tw.WriteLine("<!--EndFragment-->");

                    tw.WriteLine("</body>");
                    tw.WriteLine("</html>");
                    tw.Flush();

                    // Patch header
                    pos = ms.Position;
                    ms.Seek(INDEX_END_HTML, SeekOrigin.Begin);
                    ms.Write((bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8"))), 0, bytes.Length);
                    ms.Seek(pos, SeekOrigin.Begin);

                    // Terminator
                    ms.WriteByte(0);

                    var str = GetString(ms.Pointer, (int)ms.Length, Encoding.UTF8);
                    if (NativeMethods.SetClipboardData(CF_HTML, ms.Pointer) != IntPtr.Zero)
                        ms.FreeOnDispose = false; // Clipboard will free memory
                }
            }
            catch (Exception ex)
            {
                // Yes, we swallow any exceptions. That may seem like code smell but this matches
                // the behavior of the Clipboard class, Windows Forms controls, and native Scintilla.
                Debug.Fail(ex.Message, ex.ToString());
            }
        }

        private static unsafe void CopyRtf(Scintilla scintilla, StyleData[] styles, List<ArraySegment<byte>> styledSegments)
        {
            // NppExport -> NppExport.cpp
            // NppExport -> RTFExporter.cpp
            // http://en.wikipedia.org/wiki/Rich_Text_Format
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649013.aspx
            // http://forums.codeguru.com/showthread.php?242982-Converting-pixels-to-twips
            // http://en.wikipedia.org/wiki/UTF-8

            try
            {
                // Calculate twips per space
                int twips;
                var fontStyle = FontStyle.Regular;
                if (styles[Style.Default].Weight >= 700)
                    fontStyle |= FontStyle.Bold;
                if (styles[Style.Default].Italic != 0)
                    fontStyle |= FontStyle.Italic;
                if (styles[Style.Default].Underline != 0)
                    fontStyle |= FontStyle.Underline;

                using (var graphics = scintilla.CreateGraphics())
                using (var font = new Font(styles[Style.Default].FontName, styles[Style.Default].SizeF, fontStyle))
                {
                    var width = graphics.MeasureString(" ", font).Width;
                    twips = (int)((width / graphics.DpiX) * 1440);
                    // TODO The twips value calculated seems too small on my computer
                }

                // Write RTF
                using (var ms = new NativeMemoryStream(styledSegments.Sum(s => s.Count)))
                using (var tw = new StreamWriter(ms, Encoding.ASCII))
                {
                    var tabWidth = scintilla.DirectMessage(NativeMethods.SCI_GETTABWIDTH).ToInt32();
                    var deftab = tabWidth * twips;

                    tw.WriteLine(@"{{\rtf1\ansi\deff0\deftab{0}", deftab);
                    tw.Flush();

                    // Build the font table
                    tw.Write(@"{\fonttbl");
                    tw.Write(@"{{\f0 {0};}}", styles[Style.Default].FontName);
                    var fontIndex = 1;
                    for (int i = 0; i < styles.Length; i++)
                    {
                        if (!styles[i].Used)
                            continue;

                        if (i == Style.Default)
                            continue;

                        // Not a completely unique list, but close enough
                        if (styles[i].FontName != styles[Style.Default].FontName)
                        {
                            styles[i].FontIndex = fontIndex++;
                            tw.Write(@"{{\f{0} {1};}}", styles[i].FontIndex, styles[i].FontName);
                        }
                    }
                    tw.WriteLine("}"); // fonttbl
                    tw.Flush();

                    // Build the color table
                    tw.Write(@"{\colortbl");
                    tw.Write(@"\red{0}\green{1}\blue{2};", (styles[Style.Default].ForeColor >> 0) & 0xFF, (styles[Style.Default].ForeColor >> 8) & 0xFF, (styles[Style.Default].ForeColor >> 16) & 0xFF);
                    tw.Write(@"\red{0}\green{1}\blue{2};", (styles[Style.Default].BackColor >> 0) & 0xFF, (styles[Style.Default].BackColor >> 8) & 0xFF, (styles[Style.Default].BackColor >> 16) & 0xFF);
                    styles[Style.Default].ForeColorIndex = 0;
                    styles[Style.Default].BackColorIndex = 1;
                    var colorIndex = 2;
                    for (int i = 0; i < styles.Length; i++)
                    {
                        if (!styles[i].Used)
                            continue;

                        if (i == Style.Default)
                            continue;

                        // Not a completely unique list, but close enough
                        if (styles[i].ForeColor != styles[Style.Default].ForeColor)
                        {
                            styles[i].ForeColorIndex = colorIndex++;
                            tw.Write(@"\red{0}\green{1}\blue{2};", (styles[i].ForeColor >> 0) & 0xFF, (styles[i].ForeColor >> 8) & 0xFF, (styles[i].ForeColor >> 16) & 0xFF);
                        }
                        else
                        {
                            styles[i].ForeColorIndex = styles[Style.Default].ForeColorIndex;
                        }

                        if (styles[i].BackColor != styles[Style.Default].BackColor)
                        {
                            styles[i].BackColorIndex = colorIndex++;
                            tw.Write(@"\red{0}\green{1}\blue{2};", (styles[i].BackColor >> 0) & 0xFF, (styles[i].BackColor >> 8) & 0xFF, (styles[i].BackColor >> 16) & 0xFF);
                        }
                        else
                        {
                            styles[i].BackColorIndex = styles[Style.Default].BackColorIndex;
                        }
                    }
                    tw.WriteLine("}"); // colortbl
                    tw.Flush();

                    // Start with the default style
                    tw.Write(@"\f{0}\fs{1}\cf{2}\chshdng0\chcbpat{3}\cb{3} ", styles[Style.Default].FontIndex, (int)(styles[Style.Default].SizeF * 2), styles[Style.Default].ForeColorIndex, styles[Style.Default].BackColorIndex);
                    if (styles[Style.Default].Italic != 0)
                        tw.Write(@"\i");
                    if (styles[Style.Default].Underline != 0)
                        tw.Write(@"\ul");
                    if (styles[Style.Default].Weight >= 700)
                        tw.Write(@"\b");

                    tw.AutoFlush = true;
                    var lastStyle = Style.Default;
                    var unicodeLineEndings = ((scintilla.DirectMessage(NativeMethods.SCI_GETLINEENDTYPESACTIVE).ToInt32() & NativeMethods.SC_LINE_END_TYPE_UNICODE) > 0);
                    foreach (var seg in styledSegments)
                    {
                        var endOffset = seg.Offset + seg.Count;
                        for (int i = seg.Offset; i < endOffset; i += 2)
                        {
                            var ch = seg.Array[i];
                            var style = seg.Array[i + 1];

                            if (lastStyle != style)
                            {
                                // Change the style
                                if (styles[lastStyle].FontIndex != styles[style].FontIndex)
                                    tw.Write(@"\f{0}", styles[style].FontIndex);
                                if (styles[lastStyle].SizeF != styles[style].SizeF)
                                    tw.Write(@"\fs{0}", (int)(styles[style].SizeF * 2));
                                if (styles[lastStyle].ForeColorIndex != styles[style].ForeColorIndex)
                                    tw.Write(@"\cf{0}", styles[style].ForeColorIndex);
                                if (styles[lastStyle].BackColorIndex != styles[style].BackColorIndex)
                                    tw.Write(@"\chshdng0\chcbpat{0}\cb{0}", styles[style].BackColorIndex);
                                if (styles[lastStyle].Italic != styles[style].Italic)
                                    tw.Write(@"\i{0}", styles[style].Italic != 0 ? "" : "0");
                                if (styles[lastStyle].Underline != styles[style].Underline)
                                    tw.Write(@"\ul{0}", styles[style].Underline != 0 ? "" : "0");
                                if (styles[lastStyle].Weight != styles[style].Weight)
                                {
                                    if (styles[style].Weight >= 700 && styles[lastStyle].Weight < 700)
                                        tw.Write(@"\b");
                                    else if (styles[style].Weight < 700 && styles[lastStyle].Weight >= 700)
                                        tw.Write(@"\b0");
                                }

                                // NOTE: We don't support StyleData.Visible and StyleData.Case in RTF

                                lastStyle = style;
                                tw.Write(" "); // Delimiter
                            }

                            switch (ch)
                            {
                                case (byte)'{':
                                    tw.Write(@"\{");
                                    break;

                                case (byte)'}':
                                    tw.Write(@"\}");
                                    break;

                                case (byte)'\\':
                                    tw.Write(@"\\");
                                    break;

                                case (byte)'\t':
                                    tw.Write(@"\tab ");
                                    break;

                                case (byte)'\r':
                                    if (i + 2 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == (byte)'\n')
                                            i += 2;
                                    }

                                    // Either way, this is a line break
                                    goto case (byte)'\n';

                                case 0xC2:
                                    if (unicodeLineEndings && i + 2 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == 0x85) // NEL \u0085
                                        {
                                            i += 2;
                                            goto case (byte)'\n';
                                        }
                                    }

                                    // Not a Unicode line break
                                    goto default;

                                case 0xE2:
                                    if (unicodeLineEndings && i + 4 < endOffset)
                                    {
                                        if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA8) // LS \u2028
                                        {
                                            i += 4;
                                            goto case (byte)'\n';
                                        }
                                        else if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA9) // PS \u2029
                                        {
                                            i += 4;
                                            goto case (byte)'\n';
                                        }
                                    }

                                    // Not a Unicode line break
                                    goto default;

                                case (byte)'\n':
                                    // All your line breaks are belong to us
                                    tw.WriteLine(@"\par");
                                    break;

                                default:

                                    if (ch == 0)
                                    {
                                        // Scintilla behavior is to allow control characters except for
                                        // NULL which will cause the Clipboard to truncate the string.
                                        tw.Write(" "); // Replace with space
                                        break;
                                    }

                                    if (ch > 0x7F)
                                    {
                                        // Treat as UTF-8 code point
                                        int unicode = 0;
                                        if (ch < 0xE0 && i + 2 < endOffset)
                                        {
                                            unicode |= ((0x1F & ch) << 6);
                                            unicode |= (0x3F & seg.Array[i + 2]);
                                            tw.Write(@"\u{0}?", unicode);
                                            i += 2;
                                            break;
                                        }
                                        else if (ch < 0xF0 && i + 4 < endOffset)
                                        {
                                            unicode |= ((0xF & ch) << 12);
                                            unicode |= ((0x3F & seg.Array[i + 2]) << 6);
                                            unicode |= (0x3F & seg.Array[i + 4]);
                                            tw.Write(@"\u{0}?", unicode);
                                            i += 4;
                                            break;
                                        }
                                        else if (ch < 0xF8 && i + 6 < endOffset)
                                        {
                                            unicode |= ((0x7 & ch) << 18);
                                            unicode |= ((0x3F & seg.Array[i + 2]) << 12);
                                            unicode |= ((0x3F & seg.Array[i + 4]) << 6);
                                            unicode |= (0x3F & seg.Array[i + 6]);
                                            tw.Write(@"\u{0}?", unicode);
                                            i += 6;
                                            break;
                                        }
                                    }

                                    // Regular ANSI char
                                    ms.WriteByte(ch);
                                    break;
                            }
                        }
                    }

                    tw.AutoFlush = false;
                    tw.WriteLine("}"); // rtf1
                    tw.Flush();

                    // Terminator
                    ms.WriteByte(0);

                    // var str = GetString(ms.Pointer, (int)ms.Length, Encoding.ASCII);
                    if (NativeMethods.SetClipboardData(CF_RTF, ms.Pointer) != IntPtr.Zero)
                        ms.FreeOnDispose = false; // Clipboard will free memory
                }
            }
            catch (Exception ex)
            {
                // Yes, we swallow any exceptions. That may seem like code smell but this matches
                // the behavior of the Clipboard class, Windows Forms controls, and native Scintilla.
                Debug.Fail(ex.Message, ex.ToString());
            }
        }

        public static unsafe byte[] GetBytes(string text, Encoding encoding, bool zeroTerminated)
        {
            if (string.IsNullOrEmpty(text))
                return (zeroTerminated ? new byte[] { 0 } : new byte[0]);

            int count = encoding.GetByteCount(text);
            byte[] buffer = new byte[count + (zeroTerminated ? 1 : 0)];

            fixed (byte* bp = buffer)
            fixed (char* ch = text)
            {
                encoding.GetBytes(ch, text.Length, bp, count);
            }

            if (zeroTerminated)
                buffer[buffer.Length - 1] = 0;

            return buffer;
        }

        public static unsafe byte[] GetBytes(char[] text, int length, Encoding encoding, bool zeroTerminated)
        {
            fixed (char* cp = text)
            {
                var count = encoding.GetByteCount(cp, length);
                var buffer = new byte[count + (zeroTerminated ? 1 : 0)];
                fixed (byte* bp = buffer)
                    encoding.GetBytes(cp, length, bp, buffer.Length);

                if (zeroTerminated)
                    buffer[buffer.Length - 1] = 0;

                return buffer;
            }
        }

        public static string GetHtml(Scintilla scintilla, int startBytePos, int endBytePos)
        {
            // If we ever allow more than UTF-8, this will have to be revisited
            Debug.Assert(scintilla.DirectMessage(NativeMethods.SCI_GETCODEPAGE).ToInt32() == NativeMethods.SC_CP_UTF8);

            if (startBytePos == endBytePos)
                return string.Empty;

            StyleData[] styles = null;
            List<ArraySegment<byte>> styledSegments = GetStyledSegments(scintilla, false, false, startBytePos, endBytePos, out styles);

            using (var ms = new NativeMemoryStream(styledSegments.Sum(s => s.Count))) // Hint
            using (var sw = new StreamWriter(ms, new UTF8Encoding(false)))
            {
                // Write the styles
                sw.WriteLine(@"<style type=""text/css"" scoped="""">");
                sw.Write("div#segments {");
                sw.Write(" float: left;");
                sw.Write(" white-space: pre;");
                sw.Write(" line-height: {0}px;", scintilla.DirectMessage(NativeMethods.SCI_TEXTHEIGHT, new IntPtr(0)).ToInt32());
                sw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[Style.Default].BackColor >> 0) & 0xFF, (styles[Style.Default].BackColor >> 8) & 0xFF, (styles[Style.Default].BackColor >> 16) & 0xFF);
                sw.WriteLine(" }");

                for (int i = 0; i < styles.Length; i++)
                {
                    if (!styles[i].Used)
                        continue;

                    sw.Write("span.s{0} {{", i);
                    sw.Write(@" font-family: ""{0}"";", styles[i].FontName);
                    sw.Write(" font-size: {0}pt;", styles[i].SizeF);
                    sw.Write(" font-weight: {0};", styles[i].Weight);
                    if (styles[i].Italic != 0)
                        sw.Write(" font-style: italic;");
                    if (styles[i].Underline != 0)
                        sw.Write(" text-decoration: underline;");
                    sw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[i].BackColor >> 0) & 0xFF, (styles[i].BackColor >> 8) & 0xFF, (styles[i].BackColor >> 16) & 0xFF);
                    sw.Write(" color: #{0:X2}{1:X2}{2:X2};", (styles[i].ForeColor >> 0) & 0xFF, (styles[i].ForeColor >> 8) & 0xFF, (styles[i].ForeColor >> 16) & 0xFF);
                    switch ((StyleCase)styles[i].Case)
                    {
                        case StyleCase.Upper:
                            sw.Write(" text-transform: uppercase;");
                            break;
                        case StyleCase.Lower:
                            sw.Write(" text-transform: lowercase;");
                            break;
                    }

                    if (styles[i].Visible == 0)
                        sw.Write(" visibility: hidden;");

                    sw.WriteLine(" }");
                }

                sw.WriteLine("</style>");

                var unicodeLineEndings = ((scintilla.DirectMessage(NativeMethods.SCI_GETLINEENDTYPESACTIVE).ToInt32() & NativeMethods.SC_LINE_END_TYPE_UNICODE) > 0);
                var tabSize = scintilla.DirectMessage(NativeMethods.SCI_GETTABWIDTH).ToInt32();
                var tab = new string(' ', tabSize);
                var lastStyle = Style.Default;

                // Write the styled text
                sw.Write(@"<div id=""segments""><span class=""s{0}"">", Style.Default);
                sw.Flush();
                sw.AutoFlush = true;

                foreach (var seg in styledSegments)
                {
                    var endOffset = seg.Offset + seg.Count;
                    for (int i = seg.Offset; i < endOffset; i += 2)
                    {
                        var ch = seg.Array[i];
                        var style = seg.Array[i + 1];

                        if (lastStyle != style)
                        {
                            sw.Write(@"</span><span class=""s{0}"">", style);
                            lastStyle = style;
                        }

                        switch (ch)
                        {
                            case (byte)'<':
                                sw.Write("&lt;");
                                break;

                            case (byte)'>':
                                sw.Write("&gt;");
                                break;

                            case (byte)'&':
                                sw.Write("&amp;");
                                break;

                            case (byte)'\t':
                                sw.Write(tab);
                                break;

                            case (byte)'\r':
                                if (i + 2 < endOffset)
                                {
                                    if (seg.Array[i + 2] == (byte)'\n')
                                        i += 2;
                                }

                                // Either way, this is a line break
                                goto case (byte)'\n';

                            case 0xC2:
                                if (unicodeLineEndings && i + 2 < endOffset)
                                {
                                    if (seg.Array[i + 2] == 0x85) // NEL \u0085
                                    {
                                        i += 2;
                                        goto case (byte)'\n';
                                    }
                                }

                                // Not a Unicode line break
                                goto default;

                            case 0xE2:
                                if (unicodeLineEndings && i + 4 < endOffset)
                                {
                                    if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA8) // LS \u2028
                                    {
                                        i += 4;
                                        goto case (byte)'\n';
                                    }
                                    else if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA9) // PS \u2029
                                    {
                                        i += 4;
                                        goto case (byte)'\n';
                                    }
                                }

                                // Not a Unicode line break
                                goto default;

                            case (byte)'\n':
                                // All your line breaks are belong to us
                                sw.Write("\r\n");
                                break;

                            default:

                                if (ch == 0)
                                {
                                    // Replace NUL with space
                                    sw.Write(" ");
                                    break;
                                }

                                ms.WriteByte(ch);
                                break;
                        }
                    }
                }

                sw.AutoFlush = false;
                sw.WriteLine("</span></div>");
                sw.Flush();

                return GetString(ms.Pointer, (int)ms.Length, Encoding.UTF8);
            }
        }

        public static unsafe string GetString(IntPtr bytes, int length, Encoding encoding)
        {
            var ptr = (sbyte*)bytes;
            var str = new string(ptr, 0, length, encoding);

            return str;
        }

        private static unsafe List<ArraySegment<byte>> GetStyledSegments(Scintilla scintilla, bool currentSelection, bool currentLine, int startBytePos, int endBytePos, out StyleData[] styles)
        {
            var segments = new List<ArraySegment<byte>>();
            if (currentSelection)
            {
                // Get each selection as a segment.
                // Rectangular selections are ordered top to bottom and have line breaks appended.
                var ranges = new List<Tuple<int, int>>();
                var selCount = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONS).ToInt32();
                for (int i = 0; i < selCount; i++)
                {
                    var selStartBytePos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNSTART, new IntPtr(i)).ToInt32();
                    var selEndBytePos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNEND, new IntPtr(i)).ToInt32();

                    ranges.Add(Tuple.Create(selStartBytePos, selEndBytePos));
                }

                var selIsRect = scintilla.DirectMessage(NativeMethods.SCI_SELECTIONISRECTANGLE) != IntPtr.Zero;
                if (selIsRect)
                    ranges.OrderBy(r => r.Item1); // Sort top to bottom

                foreach (var range in ranges)
                {
                    var styledText = GetStyledText(scintilla, range.Item1, range.Item2, selIsRect);
                    segments.Add(styledText);
                }
            }
            else if (currentLine)
            {
                // Get the current line
                var mainSelection = scintilla.DirectMessage(NativeMethods.SCI_GETMAINSELECTION).ToInt32();
                var mainCaretPos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNCARET, new IntPtr(mainSelection)).ToInt32();
                var lineIndex = scintilla.DirectMessage(NativeMethods.SCI_LINEFROMPOSITION, new IntPtr(mainCaretPos)).ToInt32();
                var lineStartBytePos = scintilla.DirectMessage(NativeMethods.SCI_POSITIONFROMLINE, new IntPtr(lineIndex)).ToInt32();
                var lineLength = scintilla.DirectMessage(NativeMethods.SCI_POSITIONFROMLINE, new IntPtr(lineIndex)).ToInt32();

                var styledText = GetStyledText(scintilla, lineStartBytePos, (lineStartBytePos + lineLength), false);
                segments.Add(styledText);
            }
            else // User-specified range
            {
                Debug.Assert(startBytePos != endBytePos);
                var styledText = GetStyledText(scintilla, startBytePos, endBytePos, false);
                segments.Add(styledText);
            }

            // Build a list of (used) styles
            styles = new StyleData[NativeMethods.STYLE_MAX + 1];

            styles[Style.Default].Used = true;
            styles[Style.Default].FontName = scintilla.Styles[Style.Default].Font;
            styles[Style.Default].SizeF = scintilla.Styles[Style.Default].SizeF;
            styles[Style.Default].Weight = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETWEIGHT, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].Italic = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETITALIC, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].Underline = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETUNDERLINE, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].BackColor = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETBACK, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].ForeColor = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETFORE, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].Case = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETCASE, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();
            styles[Style.Default].Visible = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETVISIBLE, new IntPtr(Style.Default), IntPtr.Zero).ToInt32();

            foreach (var seg in segments)
            {
                for (int i = 0; i < seg.Count; i += 2)
                {
                    var style = seg.Array[i + 1];
                    if (!styles[style].Used)
                    {
                        styles[style].Used = true;
                        styles[style].FontName = scintilla.Styles[style].Font;
                        styles[style].SizeF = scintilla.Styles[style].SizeF;
                        styles[style].Weight = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETWEIGHT, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].Italic = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETITALIC, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].Underline = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETUNDERLINE, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].BackColor = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETBACK, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].ForeColor = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETFORE, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].Case = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETCASE, new IntPtr(style), IntPtr.Zero).ToInt32();
                        styles[style].Visible = scintilla.DirectMessage(NativeMethods.SCI_STYLEGETVISIBLE, new IntPtr(style), IntPtr.Zero).ToInt32();
                    }
                }
            }

            return segments;
        }

        private static unsafe ArraySegment<byte> GetStyledText(Scintilla scintilla, int startBytePos, int endBytePos, bool addLineBreak)
        {
            Debug.Assert(endBytePos > startBytePos);

            // Make sure the range is styled
            scintilla.DirectMessage(NativeMethods.SCI_COLOURISE, new IntPtr(startBytePos), new IntPtr(endBytePos));

            var byteLength = (endBytePos - startBytePos);
            var buffer = new byte[(byteLength * 2) + (addLineBreak ? 4 : 0) + 2];
            fixed (byte* bp = buffer)
            {
                NativeMethods.Sci_TextRange* tr = stackalloc NativeMethods.Sci_TextRange[1];
                tr->chrg.cpMin = startBytePos;
                tr->chrg.cpMax = endBytePos;
                tr->lpstrText = new IntPtr(bp);

                scintilla.DirectMessage(NativeMethods.SCI_GETSTYLEDTEXT, IntPtr.Zero, new IntPtr(tr));
                byteLength *= 2;
            }

            // Add a line break?
            // We do this when this range is part of a rectangular selection.
            if (addLineBreak)
            {
                var style = buffer[byteLength - 1];

                buffer[byteLength++] = (byte)'\r';
                buffer[byteLength++] = style;
                buffer[byteLength++] = (byte)'\n';
                buffer[byteLength++] = style;

                // Fix-up the NULL terminator just in case
                buffer[byteLength] = 0;
                buffer[byteLength + 1] = 0;
            }

            return new ArraySegment<byte>(buffer, 0, byteLength);
        }

        public static int TranslateKeys(Keys keys)
        {
            int keyCode;

            // For some reason Scintilla uses different values for these keys...
            switch (keys & Keys.KeyCode)
            {
                case Keys.Down:
                    keyCode = NativeMethods.SCK_DOWN;
                    break;
                case Keys.Up:
                    keyCode = NativeMethods.SCK_UP;
                    break;
                case Keys.Left:
                    keyCode = NativeMethods.SCK_LEFT;
                    break;
                case Keys.Right:
                    keyCode = NativeMethods.SCK_RIGHT;
                    break;
                case Keys.Home:
                    keyCode = NativeMethods.SCK_HOME;
                    break;
                case Keys.End:
                    keyCode = NativeMethods.SCK_END;
                    break;
                case Keys.Prior:
                    keyCode = NativeMethods.SCK_PRIOR;
                    break;
                case Keys.Next:
                    keyCode = NativeMethods.SCK_NEXT;
                    break;
                case Keys.Delete:
                    keyCode = NativeMethods.SCK_DELETE;
                    break;
                case Keys.Insert:
                    keyCode = NativeMethods.SCK_INSERT;
                    break;
                case Keys.Escape:
                    keyCode = NativeMethods.SCK_ESCAPE;
                    break;
                case Keys.Back:
                    keyCode = NativeMethods.SCK_BACK;
                    break;
                case Keys.Tab:
                    keyCode = NativeMethods.SCK_TAB;
                    break;
                case Keys.Return:
                    keyCode = NativeMethods.SCK_RETURN;
                    break;
                case Keys.Add:
                    keyCode = NativeMethods.SCK_ADD;
                    break;
                case Keys.Subtract:
                    keyCode = NativeMethods.SCK_SUBTRACT;
                    break;
                case Keys.Divide:
                    keyCode = NativeMethods.SCK_DIVIDE;
                    break;
                case Keys.LWin:
                    keyCode = NativeMethods.SCK_WIN;
                    break;
                case Keys.RWin:
                    keyCode = NativeMethods.SCK_RWIN;
                    break;
                case Keys.Apps:
                    keyCode = NativeMethods.SCK_MENU;
                    break;
                case Keys.Oem2:
                    keyCode = (byte)'/';
                    break;
                case Keys.Oem3:
                    keyCode = (byte)'`';
                    break;
                case Keys.Oem4:
                    keyCode = '[';
                    break;
                case Keys.Oem5:
                    keyCode = '\\';
                    break;
                case Keys.Oem6:
                    keyCode = ']';
                    break;
                default:
                    keyCode = (int)(keys & Keys.KeyCode);
                    break;
            }

            // No translation necessary for the modifiers. Just add them back in.
            var keyDefinition = keyCode | (int)(keys & Keys.Modifiers);
            return keyDefinition;
        }

        #endregion Methods

        #region Types

        private struct StyleData
        {
            public bool Used;
            public string FontName;
            public int FontIndex; // RTF Only
            public float SizeF;
            public int Weight;
            public int Italic;
            public int Underline;
            public int BackColor;
            public int BackColorIndex; // RTF Only
            public int ForeColor;
            public int ForeColorIndex; // RTF Only
            public int Case; // HTML only
            public int Visible; // HTML only
        }

        #endregion Types
    }
}
