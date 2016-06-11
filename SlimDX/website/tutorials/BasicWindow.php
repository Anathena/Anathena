<?php
	$g_pageTitle = "SlimDX Tutorial - Basic Window";
	$g_pageName = "Tutorials";
	
	require_once("tutorial_header.php");
?>

<div id="content">
  <div class="wrapper">
    <h4>Direct3D 11 - Basic Window</h4>

    <p>
      This tutorial is the start of a series explaining and
      demonstrating the use of Direct3D 11 via SlimDX. Readers are expected to know
      how to use their chosen .NET language and IDE before attempting to understand these
      tutorials, as language concepts will not be explained.
      <br/><br/>
      The first tutorial will deal with getting a SlimDX project
      set up and ready to go, and will then explain the process of initializing a
      window and launching a rendering loop.
      <br/><br/>
    </p>

    <h1>
      Introduction
    </h1>

    <p>
      SlimDX is a free open source framework that enables
      developers to easily build DirectX applications using <i>any</i> .NET language,
      including C#, VB.Net, F#, and IronPython. This tutorial series will serve as
      both an introduction to rendering through Direct3D as well as a guide for
      peculiarities specific to accessing it through SlimDX. In this way, the
      tutorials will be useful both for complete beginners as well as DirectX pros
      who are simply new to either managed code, Direct3D 11, or perhaps both.
      <br/><br/>
    </p>

    <h1>
      Prerequisites
    </h1>

    <p>
      Developing applications using SlimDX requires several
      prerequisites. First and foremost, you must have the <a href="http://slimdx.org/download.php">SlimDX SDK</a> installed. The SDK
      contains everything you need to get started writing applications:
      <br/><br/>

      <ul style="margin-left: 2em;">
        <li>The SlimDX binaries, one each for x86 and x64 in both .NET 2.0 and .NET 4.0</li>
        <li>Compiled documentation and XML comments for Intellisense</li>
        <li>ClickOnce support</li>
        <li>The minimum required installation of DirectX</li>
      </ul>

      <br/>
      The last point is of some interest. While it is possible to
      write fully featured applications using the SlimDX SDK only, additional
      samples, documentation, and debugging tools can be obtained by downloading and
      installing the full <a href="http://msdn.microsoft.com/en-us/directx/aa937788.aspx">DirectX SDK</a>
      from Microsoft. For any non-trivial application development, this is highly
      recommended.
      <br/><br/>
      You will also need an IDE with which to develop your
      applications. The SlimDX SDK supports both Visual Studio 2008 and Visual Studio
      2010. If you do not have access to the full versions of these software
      packages, you can download the <a href="http://www.microsoft.com/express/Downloads/#2010-Visual-CS">express editions</a> from Microsoft for free.
      <br/><br/>
      Other tools, such as those provided by graphics card
      manufacturers, can be of immense use for debugging and development as well.
      NVIDIA’s <a href="http://developer.nvidia.com/object/nvperfhud_home.html">PerfHUD</a>
      tool is commonly used for debugging rendering issues, while <a href="http://news.developer.nvidia.com/2008/07/fx-composer-25.html">FxComposer</a>
      can be used to interactively write shaders and effects. AMD has similar
      offerings, with <a href="http://developer.amd.com/gpu/perfstudio/Pages/default.aspx">GPU PerfStudio</a>
      being used debugging and analysis and <a href="http://developer.amd.com/gpu/rendermonkey/Pages/default.aspx">RenderMonkey</a>
      being used for shader authoring. Both manufacturers also provide a wealth of
      other tools, whitepapers, debugging information, and samples, so check out
      their developer sites for more information.
      <br/><br/>
      Finally, as we will be working with Direct3D 11, it’s
      required that you run either Windows Vista with Service Pack 2 installed, or
      Windows 7. It’s helpful to have a D3D11-capable card to take advantage of the
      new hardware features, but thanks to the new feature level system it’s possible
      to use any Direct3D 9 or later hardware alongside Direct3D 11.
      <br/><br/>
    </p>

    <h1>
      Project Setup
    </h1>

    <p>
      After creating a new .NET Windows Forms Application project
      in your chosen IDE, the first step towards writing a SlimDX application is to
      add a reference to the SlimDX DLL itself. This process isn’t as straightforward
      as for most .NET libraries, as the issue of x86 vs. x64 needs to be taken into
      account. The SlimDX installer ships with binaries for.NET 2.0-3.5, which will
      be installed in the <a href="http://msdn.microsoft.com/en-us/library/yf1d93sz.aspx">Global Assembly Cache</a> (GAC).
      <br/><br/>
      <img border='0' width='561' height='446' id="_x0000_i1026" src="BasicWindow_files/image001.png">

        <br/><br/>
        Applications that run on the CLR are inherently
        instruction-set neutral, meaning the JIT will determine at runtime whether your
        process will run in 32-bit or 64-bit mode. However, native DLLs cannot be
        loaded into a process unless they match the instruction-set size, so SlimDX
        ships with both binaries which are each compiled against the corresponding
        DirectX DLLs. Chances are good that you’d like your application to run under
        both x64 and x86 automatically without having to compile specific versions, so
        following is the method for doing so:
        <br/><br/>

        <ul style="margin-left: 2em;list-style-type: decimal;">
          <li>
            First, select either assembly listed for your chosen architecture and
            add it to the project. It doesn’t matter which you pick, because we will be
            modifying it in a moment.
          </li>

          <li>
            Right click the project in the solution explorer and select “Unload
            Project”.
          </li>

          <li>
            Right click the project again and select “Edit csproj”.
          </li>

          <li>
            The project will open in the text editor. Scroll down to where you
            can see the SlimDX reference element:
          </li>
          <br/>
          <pre class="prettyprint">&lt;ItemGroup&gt;
  &lt;Reference Include="SlimDX, Version=2.0.9.42, Culture=neutral, PublicKeyToken=b1b0c32fd1ffe4f9, processorArchitecture=AMD64" /&gt;
  &lt;Reference Include="System" /&gt;
  &lt;Reference Include="System.Drawing" /&gt;
  &lt;Reference Include="System.Windows.Forms" /&gt;
&lt;/ItemGroup&gt;</pre>
          <br/>
          <li>
            Highlight the <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:blue'>processorArchitecture=...</span> tag and delete it
            completely. This will cause the CLR to load the appropriate version of the DLL
            once the process is launched and the correct platform can be determined.
          </li>

          <li>
            Save the file, right click the project, and select “Reload project”.
          </li>
        </ul>
        <br/>

        Note that this technique will only work if SlimDX is
        installed into the GAC. If you are building from source or redistributing the
        SlimDX binaries alongside your application, you will have to target a specific
        version manually.
        <br/><br/>
      </p>

    <h2>.NET Framework 4.0</h2>

    <p>
      The SlimDX SDK contains a set of binaries built against .NET
      4.0, but as of the June release there is no end-user installer for them. This
      is due to changes in the VC redist format for VS 2010, which no longer contains
      both x86 and x64 redists in one package. We are still working on getting an
      installer package for .NET 4.0, but until then you will need to browse manually
      to the reference of choice and redistribute the binaries alongside your
      application. As mentioned above, doing this will require you to target a
      specific platform version manually instead of being able to take advantage of
      automatic processor architecture selection.
      <br/><br/>
    </p>

    <h1>
      Window Creation
    </h1>

    <p>
      For a C++ / DirectX tutorial, this is normally a long and
      involved section detailing how to properly set up the window and ensure that it
      plays nicely with the operating system. Luckily, .NET provides a vastly
      simplified wrapper around all of this, and it is therefore quite easy to get a
      window visible and ready for rendering.
      <br/><br/>
      While it’s quite easy and perfectly valid to make use of the
      default provided <a href='http://msdn.microsoft.com/en-us/library/system.windows.forms.form.aspx' 
                          style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Form</a>
      object as your rendering surface (or any other type derived from <a href='http://msdn.microsoft.com/en-us/library/system.windows.forms.control.aspx' 
                                                                          style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Control</a>),
      SlimDX also provides a specialized <a href='http://slimdx.org/docs/#T_SlimDX_Windows_RenderForm' 
                                            style='font-size: 10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>RenderForm</a>
      class that overrides a few message handlers to reduce flickering and sets
      defaults for the size of the client area. Additionally, it automatically loads
      the SlimDX icon and uses it as the icon for the window. Use of this class is
      optional, but it will be employed throughout the tutorial series.
      <br/><br/>

      <pre class="prettyprint">var form = new RenderForm("Tutorial 1: Basic Window");</pre>
      <br/>
    </p>

    <h1>
      Message Loop
    </h1>

    <p>
      All Windows applications make use of a message loop to pump
      operating system messages to the appropriate windows and handle the correct
      responses. For real time applications, rendering and game update logic is
      commonly performed whenever there is idle time between window messages. Since a
      C++ application performs all message pumping itself, it is easy to insert this
      code into the correct place.
      <br/><br/>
      For .NET code, however, this process is hidden away beneath
      the <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.application.run.aspx" style='font-size:10.0pt;line-height:115%;font-family:"Courier New";
color:#2B91AF'>Application</a><span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black;'>.Run</span> method. In order to achieve
      continuous rendering, several techniques are often employed, with varying
      levels of success. These suboptimal methods are presented here along with their
      downsides, and then the preferred method for running a game loop in managed
      code is revealed. The SlimDX library contains code to support the last method,
      and so it will be the operation of choice for this tutorial series.
      <br/><br/>
    </p>

    <h2>
      The Paint Loop
    </h2>

    <p>
      One of the most naive ways that people tend to use to get a
      continuous stream of render calls in .NET is to hijack the Windows paint
      messages and ensure that they are continuously generated. This could look
      something like this:

      <br/><br/>
      <pre class='prettyprint'>protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);
    RenderFrame();    // do rendering
    Invalidate();     // ensure that OnPaint is called again right away
}</pre><br/>
      
      The problem with this is twofold. First, <a href='http://msdn.microsoft.com/en-us/library/598t492a.aspx' 
                                                  style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Invalidate</a>
      ends up making several small allocations to handle the wiring of the events.
      Normally this isn't a problem for the .NET garbage collector, but when you're
      calling it continuously throughout the life of your application, the little
      extra garbage can add extra strain that isn't needed. The second problem is
      that you're appropriating an operating system process that is designed to
      handle painting of windows intermittently. It simply isn't designed to handle continuous
      painting and repainting, and you're going to see some overhead from redrawing
      the window at every possible opportunity.
      <br/><br/>
    </p>

    <h2>
      Application.DoEvents
    </h2>

    <p>
      The second method often employed to pump messages is the <a href='http://msdn.microsoft.com/en-us/library/system.windows.forms.application.doevents.aspx' 
                                                                  style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Application</a><span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.DoEvents</span>
      method. This method, when called, handles all pending window messages and then
      immediately returns. Here is what such a method would look like:
      
      <br/><br/>
      <pre class='prettyprint'>while (running)
{
    RenderFrame();
    Application.DoEvents();    // handle any pending window messages
}</pre><br/>
      
      This code has the advantage of avoiding the painting process
      for windows, is simple, and lets you render continuously while still handling
      any incoming window messages. Unfortunately, as shown by <a href="http://blogs.msdn.com/tmiller/archive/2003/11/07/57524.aspx"
target="_blank">Tom Miller</a>, <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.application.doevents.aspx" style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>DoEvents</a> still ends up allocating each call,
      which can increase the frequency of garbage collections. While gen0 collections
      of short lived temporaries are quite fast, having them often can promote your
      own short-lived objects into gen1, which will be detrimental to performance.
      <br/><br/>
    </p>

    <h2>
      The Solution: P/Invoke
    </h2>

    <p>
      The solution to this dilemma is to use interop to directly
      call into Win32 methods to bypass any allocations on the managed side. Once
      again Tom Miller provides an example and an overview of this solution in his <a href="http://blogs.msdn.com/tmiller/archive/2005/05/05/415008.aspx" target="_blank">blog</a>. 
      While this method isn't as simple as the others, it
      is certainly the most optimal in terms of speed and memory usage. This method
      was the preferred method for all of the now-deprecated MDX samples, and is the
      preferred method for SlimDX.
      
      <br/><br/>
      <pre class="prettyprint">[StructLayout(LayoutKind.Sequential)]
public struct Message
{
    public IntPtr hWnd;
    public uint msg;
    public IntPtr wParam;
    public IntPtr lParam;
    public uint time;
    public Point p;
}
        
[SuppressUnmanagedCodeSecurity]
[DllImport("user32.dll", CharSet = CharSet.Auto)]
[return: MarshalAs(UnmanagedType.Bool)]
public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

static bool AppStillIdle
{
    get
    {
        Message msg;
        return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
    }
}

public void MainLoop()
{
    // hook the application's idle event
    Application.Idle += new EventHandler(OnApplicationIdle);
    Application.Run(form);
}
        
void OnApplicationIdle(object sender, EventArgs e)
{
    while (AppStillIdle)
    {
        // Render a frame during idle time (no messages are waiting)
        RenderFrame();
    }
}</pre><br/>

      As can be seen, this method takes a lot more code and is
      more complex than the other, simpler methods. SlimDX presents a solution to
      this issue, to cut down on tedious boilerplate code while still taking
      advantage of the performance and memory benefits provided by this option:

<br/><br/><pre class="prettyprint">MessagePump.Run(form, RenderFrame);</pre><br/>
     
      The <a href="http://slimdx.org/docs/#T_SlimDX_Windows_MessagePump" 
             style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>MessagePump</a> class provides
      three overloads of the <a href='http://slimdx.org/docs/#Overload_SlimDX_Windows_MessagePump_Run' 
                                style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Run</a> method, allowing you to completely hide
      the use of <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Application</span><span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black;'>.Run</span>. If you wish to still hook the <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Application</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.Idle</span>
      event yourself but don’t want to P/Invoke <a href="http://msdn.microsoft.com/en-us/library/ms644943%28VS.85%29.aspx">PeekMessage</a>,
      you can use the provided <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:#2B91AF'>MessagePump</span><span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.IsApplicationIdle</span>
      property to do so.
      <br/><br/>
    </p>

    <h1>
      Conclusion
    </h1>

    <p>
      This tutorial served as a gentle introduction to the SlimDX
      API and development kit. Necessary prerequisites were listed and explained, and
      then the process for setting up a bare bones project was presented in
      step-by-step detail. Finally, different methods for performing window
      initialization and message pumping were presented and dissected.

<br/><br/>
      Future tutorials will build off of this foundational code,
      so ensure that it is all well understood. Next up: an introduction to Direct3D
      and an overview of initializing it and presenting to the window.

<br/><br/>
        <img border='0' width='370' height='306' id="_x0000_i1025" src="BasicWindow_files/image003.png"/>
      <br/><br/>
      
      Download the source code for this tutorial: <a href="BasicWindow_files/BasicWindow.zip">BasicWindow.zip</a>
      
    </p>
    <br/>
    <br/>

  </div>
</div>

<?php	
	require_once("../footer.php");
?>