using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Specifies the lexer to use for syntax highlighting in a <see cref="Scintilla" /> control.
    /// </summary>
    public enum Lexer
    {
        /// <summary>
        /// Lexing is performed by the <see cref="Scintilla" /> control container (host) using
        /// the <see cref="Scintilla.StyleNeeded" /> event.
        /// </summary>
        Container = NativeMethods.SCLEX_CONTAINER,

        /// <summary>
        /// No lexing should be performed.
        /// </summary>
        Null = NativeMethods.SCLEX_NULL,

        /// <summary>
        /// The Ada (95) language lexer.
        /// </summary>
        Ada = NativeMethods.SCLEX_ADA,

        /// <summary>
        /// The assembly language lexer.
        /// </summary>
        Asm = NativeMethods.SCLEX_ASM,

        /// <summary>
        /// The batch file lexer.
        /// </summary>
        Batch = NativeMethods.SCLEX_BATCH,

        /// <summary>
        /// The C language family (C++, C, C#, Java, JavaScript, etc...) lexer.
        /// </summary>
        Cpp = NativeMethods.SCLEX_CPP,

        /// <summary>
        /// The Cascading Style Sheets (CSS, SCSS) lexer.
        /// </summary>
        Css = NativeMethods.SCLEX_CSS,

        /// <summary>
        /// The Fortran language lexer.
        /// </summary>
        Fortran = NativeMethods.SCLEX_FORTRAN,

        /// <summary>
        /// The FreeBASIC language lexer.
        /// </summary>
        FreeBasic = NativeMethods.SCLEX_FREEBASIC,

        /// <summary>
        /// The HyperText Markup Language (HTML) lexer.
        /// </summary>
        Html = NativeMethods.SCLEX_HTML,

        /// <summary>
        /// The Lisp language lexer.
        /// </summary>
        Lisp = NativeMethods.SCLEX_LISP,

        /// <summary>
        /// The Lua scripting language lexer.
        /// </summary>
        Lua = NativeMethods.SCLEX_LUA,

        /// <summary>
        /// The Pascal language lexer.
        /// </summary>
        Pascal = NativeMethods.SCLEX_PASCAL,

        /// <summary>
        /// The Perl language lexer.
        /// </summary>
        Perl = NativeMethods.SCLEX_PERL,

        /// <summary>
        /// The PHP: Hypertext Preprocessor (PHP) script lexer.
        /// </summary>
        PhpScript = NativeMethods.SCLEX_PHPSCRIPT,

        /// <summary>
        /// Properties file (INI) lexer.
        /// </summary>
        Properties = NativeMethods.SCLEX_PROPERTIES,

        /// <summary>
        /// The PureBasic language lexer.
        /// </summary>
        PureBasic = NativeMethods.SCLEX_PUREBASIC,

        /// <summary>
        /// The Python language lexer.
        /// </summary>
        Python = NativeMethods.SCLEX_PYTHON,

        /// <summary>
        /// The Ruby language lexer.
        /// </summary>
        Ruby = NativeMethods.SCLEX_RUBY,

        /// <summary>
        /// The SmallTalk language lexer.
        /// </summary>
        Smalltalk = NativeMethods.SCLEX_SMALLTALK,

        /// <summary>
        /// The Structured Query Language (SQL) lexer.
        /// </summary>
        Sql = NativeMethods.SCLEX_SQL,

        /// <summary>
        /// The Visual Basic (VB) lexer.
        /// </summary>
        Vb = NativeMethods.SCLEX_VB,

        /// <summary>
        /// The Visual Basic Script (VBScript) lexer.
        /// </summary>
        VbScript = NativeMethods.SCLEX_VBSCRIPT,

        /// <summary>
        /// The Verilog hardware description language lexer.
        /// </summary>
        Verilog = NativeMethods.SCLEX_VERILOG,

        /// <summary>
        /// The Extensible Markup Language (XML) lexer.
        /// </summary>
        Xml = NativeMethods.SCLEX_XML,

        /// <summary>
        /// The Blitz (Blitz3D, BlitzMax, etc...) variant of Basic lexer.
        /// </summary>
        BlitzBasic = NativeMethods.SCLEX_BLITZBASIC,

        /// <summary>
        /// The Markdown syntax lexer.
        /// </summary>
        Markdown = NativeMethods.SCLEX_MARKDOWN,

        /// <summary>
        /// The R programming language lexer.
        /// </summary>
        R = NativeMethods.SCLEX_R
    }
}
