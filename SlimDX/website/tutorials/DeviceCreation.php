<?php
	$g_pageTitle = "SlimDX Tutorial - Device Creation";
	$g_pageName = "Tutorials";
	
	require_once("tutorial_header.php");
?>

<div id="content">
  <div class="wrapper">
    <h4>Direct3D 11 - Device Creation</h4>

    <p>
      In this second tutorial, we give an overview of the Direct3D
      11 graphics device and show how to initialize it properly for rendering. The
      Device object is the primary means of communicating with a virtual graphics
      adapter installed on the system, and will be used for all resource creation and
      the final rendering of 3D primitives. The sample will conclude by showing how
      to use the device to clear the background to a solid color.
      <br/><br/>
    </p>

    <h1>The 3D Device</h1>

    <p>
      There are two integral interfaces used in Direct3D to
      perform rendering: the device and the swap chain. The device is a
      representation of a virtual adapter located on the user’s system. An adapter is
      usually implemented in hardware by a graphics card, but not always. For
      example, an adapter can be implemented in software, such as with the WARP
      performance renderer or the Direct3D reference rasterizer. The device provides
      a unified interface to access all of these adapters and uses them to render graphical
      images to one or more outputs.
      <br/><br/>
      <a href="http://msdn.microsoft.com/en-us/library/bb205075%28VS.85%29.aspx">From the DirectX SDK</a>:

      <br/><br/>

      <img border="0" width="573" height="379" id="Picture 7" src="DeviceCreation_files/image001.jpg"/>

      <br/><br/>

      A swap chain is a set of two or more buffers that receive
      rendering data. The first buffer is the front buffer, and is immediately
      visible to the user. All other buffers form a chain of back buffers that are
      flipped between to perform smooth tear-free rendering. A swap chain is tied to
      a single window at creation time, and all of its drawing is clipped to that
      window by the operating system. The entire system of adapters, outputs, and swap
      chains is managed by the DirectX Graphics Infrastructure (DXGI), a layer
      beneath Direct3D that resides in kernel mode of the operating system.

      <br/><br/>

      A Direct3D 11 application must create at least one device
      and one swap chain in order to get anything visible on the screen. Since
      creating both a swap chain and a device at the same time is such a common
      operation, a single method is provided that can do both at once:

      <br/><br/>
      <pre class="prettyprint">public static Result Device.CreateWithSwapChain(DriverType driverType, DeviceCreationFlags flags, 
    FeatureLevel[] featureLevels, SwapChainDescription swapChainDescription, out Device device, out SwapChain swapChain);</pre>
      <br/>

      This method has several overloads depending on what settings
      you prefer to pass. There are two that take a DXGI <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>Adapter</span>
      object, and two that take a desired <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:#2B91AF'>DriverType</span> instead. When
      an adapter is passed, the driver type is inferred from the actual
      representation of that specific adapter. When a driver type is passed, the
      function will find the primary adapter that fits the requested type.
      <br/><br/>
      Enumerating a list of adapters installed on a system in
      order to pass one to D3D11 is a non-trivial process and will be covered in a
      future tutorial. Here, we will use the overload that takes a driver type:

      <ul style="margin-left: 2em;">
        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Null</span>
          – used when you don’t need the device to perform rendering. This isn’t commonly
          used.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Reference</span>
          – employs the use of the Direct3D reference rasterizer. This is useful for
          debugging rendering issues and faulty drivers, but it won’t be present on the
          end user’s system and so should not be used in production code.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Warp</span>
          – uses a high performance software renderer built into Direct3D.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Software</span>
          – specifies that you want to use a custom software renderer.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Hardware</span>
          – the most commonly used option; specifies that you want a hardware accelerated
          graphics device.
        </li>
      </ul>
      <br/>

      Hardware devices are by far the most commonly used, since
      they provide the greatest performance and make use of dedicated graphics
      hardware in the user’s computer. <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black;'>CreateWithSwapChain</span> also asks for a
      second set of flags detailing additional settings for the device. These are
      useful if you wish to enable the debug or reference layers, or if you wish to
      diagnose suspect results by disabling threading and other optimizations.
      Typically for the final product though, you’ll just pass <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>DeviceCreationFlags</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New"'>.None</span>.
      <br/><br/>
    </p>

    <h2>Feature Levels</h2>

    <p>
      The third parameter to the device creation function is an
      array of feature levels. Feature levels provide a unified method by which
      Direct3D 11 can run on lower end hardware. Each feature level mandates a
      specific set of functionality that an adapter must expose, enabling application
      developers to reliably scale their applications depending on the hardware a
      user might have. The array allows you to specify a set of feature levels that
      you’d like Direct3D to try to use. It will try them in order and create a
      device with the first one that works. You can use the <span style='font-size:
10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>Device</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.GetSupportedFeatureLevel</span>
      method to get the highest feature level currently supported by the primary
      adapter. If you don’t care to specify a set and just want to use the highest
      available, you can skip the parameter and use the less generic overload. Once
      the device has been created, you can access the <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>Device</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.FeatureLevel</span>
      property to see which feature level is currently active. The <a
href="http://msdn.microsoft.com/en-us/library/ff476148%28VS.85%29.aspx">10Level9</a>
      reference on MSDN gives in-depth details about exactly which portions of the
      API are available on which feature levels.
      <br/><br/>
    </p>

    <h2>Swap Chain Setup</h2>

    <p>
      The fourth parameter asks for a <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>SwapChainDescription</span>,
      which specifies details of the swap chain that will be created. Direct3D has
      many such description structures that are used to create various API resources;
      this will be the first of many to come. Each member of the swap chain
      description must be filled out or the method may fail.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>BufferCount</span> – The number of buffers to create, including
      the front buffer. For a windowed device, the desktop is an implicit front
      buffer and doesn’t need to be taken into account. At least one back buffer
      should be created in order to enable flipping, as opposed to copying the video
      memory each frame.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>Usage</span> – The expected usage of the back buffers. Since we
      will be rendering to this, we will specify <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>Usage</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>.RenderTargetOutput</span>.
      The only other valid member for a swap chain is ShaderInput, which lets you use
      the back buffer as an input for postprocessing effects.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>OutputHandle</span> – Designates the handle of the window to
      which the swap chain will be attached. This member can be used to tie the form
      to the swap chain via the form’s <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black;'>Handle</span> property.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>IsWindowed</span> – Specifies whether to operate in fullscreen
      or windowed mode. The DirectX SDK recommends that you always create the swap
      chain in windowed mode and then manually transition to fullscreen in order to
      avoid mismatching the size of the output with the swap chain buffer, which can
      reduce performance.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>ModeDescription </span>– Allows you to specify the desired
      display mode of the swap chain. When starting in windowed mode, you typically
      set the size of the window as the resolution and pick a default refresh rate of
      60 Hz and one of the standard back buffer formats. Passing zero for the width
      and height will automatically size the swap chain to the attached window.

      <br/><br/>

      <pre class='prettyprint'>ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm)</pre>

      <br/>
    </p>

    <h3>                DXGI Surface Formats</h3>

    <p style='margin-left:.5in'>
      DXGI exposes a number of different
      formats, and it can be confusing to know which one to pick. For the swap chain
      back buffers, you’re going to want a format that exposes all three components
      of the color data (R, G, and B). Typically, matching up with the desktop format
      is the best idea, but in this case we’re hard coding to one that we know is
      supported by most adapters (32 bits with each component being 8 bits). Each
      DXGI format has a suffix, in this case _UNorm, specifying the format of the data
      in addition to its size. Additionally, there are a few formats with an
      additional suffix of _SRGB. These formats are calibrated to produce colors that
      more correctly match the human eye color curve, so they may be preferrable for
      output formats.
      <br/><br/>
    </p>

    <p>
      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>SampleDescription</span> – Allows you to specify multisampling
      parameters. The <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>Count</span> parameter lets you pick the number of samples per
      pixel, and the <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Quality</span>
      parameter picks an antialiasing quality level.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>Flags</span> – Specifies additional flags that affect the
      behavior of the swap chain. Of interest is the <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:black;'>AllowModeSwitch</span> flag, which
      tells DXGI that the swap chain is allowed to adjust the display mode to a more
      optimal one if necessary.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>SwapEffect</span> – This determines how data in the back buffers
      are moved to the front buffer. <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>Discard</span> lets DXGI dump all back buffer data
      after it has been used, which allows it to do things in the most efficient
      manner. <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Sequential</span>
      preserves the contents of the back buffer, which may be necessary if you wish
      to read its contents.

      <br/><br/>

      Typically many of these members are set after enumerating
      adapter and output information from DXGI. This allows you to pick values that
      you know will work on a given computer configuration. For now, however, we will
      simply pick default values in order to keep things simple:

      <br/><br/>

      <pre class='prettyprint'>var description = new SwapChainDescription()
{
    BufferCount = 1,
    Usage = Usage.RenderTargetOutput,
    OutputHandle = form.Handle,
    IsWindowed = true,
    ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
    SampleDescription = new SampleDescription(1, 0),
    Flags = SwapChainFlags.AllowModeSwitch,
    SwapEffect = SwapEffect.Discard
};</pre>

      <br/>
    </p>

    <h2>Device Creation</h2>

    <p>
      Now that we have all the information we need to create the
      device and an associate swap chain, let us do so:

      <br/><br/>

      <pre class='prettyprint'>Device device;
SwapChain swapChain;
Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, description, out device, out swapChain);</pre>

      <br/>

      If all goes well, we can now use the device and swap chain
      to perform any sort of rendering we’d like.
      <br/><br/>
    </p>

    <h2>Device Context</h2>

    <p>
      New to Direct3D 11 is the concept of a device <i>context</i>,
      which now handles most of the drawing related functionality. Several contexts
      can be created and rendered to from different threads and then merged at the
      end of the frame, providing true multithreaded rendering capabilities. Each
      device has an implicit <i>immediate</i> context that we can use if we don’t
      care about multithreaded rendering. It can be accessed via the device’s <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>ImmediateContext</span>
      property:

      <br/><br/>

      <pre class='prettyprint'>var context = device.ImmediateContext;</pre>

      <br/>
    </p>

    <h2>DXGI Alt+Enter Functionality</h2>

    <p>
      DXGI has built in window-management functionality, which
      includes switching between full screen and windowed mode whenever Alt+Enter is
      pressed. Unfortunately, this default handling does not work properly with
      Winforms, so we need to disable it and handle the switch ourselves.

      <br/><br/>
      
      The base DXGI interface, called <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>Factory</span>,
      contains a method called <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>SetWindowAssociation</span> that will allow us to
      disable automatic alt+enter handling.
      
      <br/><br/>

      <pre class='prettyprint'>// prevent DXGI handling of alt+enter, which doesn't work properly with Winforms
using (var factory = swapChain.GetParent&lt;Factory&gt;())
    factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);</pre>

      <br/>

      We are now free to handle this key combo ourselves if we
      wish. Doing so is easily accomplished by hooking the form’s keyboard event
      handler and flipping the <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>swapChain.IsFullScreen</span> flag.

<br/><br/>

      <pre class='prettyprint'>// handle alt+enter ourselves
form.KeyDown += (o, e) =>
{
    if (e.Alt &amp;&amp; e.KeyCode == Keys.Enter)
        swapChain.IsFullScreen = !swapChain.IsFullScreen;
};</pre>

      <br/>
    
      Note that the key combo Alt+Enter will be interpreted as a missing
      menu mnemonic which will cause an annoying beep to occur when the app comes out
      of full screen mode. This can be solved by overriding the WndProc handler for
      the form and processing the WM_MENUCHAR message yourself. The <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>
        RenderForm
      </span>class already handles this detail for you if you choose to make use of it.

      <br/><br/>
    </p>

    <h1>Setting Up the Graphics Pipeline</h1>

    <p>
      The 3D graphics pipeline is a process that takes input
      primitive data at one end, passes it through multiple stages that manipulate
      the data, and at the end rasterizes it to the screen. Along the way each stage
      can be configured and implemented via state settings and shaders.

      <br/><br/>

      The pipeline has grown more complex and more customizable
      with each passing Direct3D version. The current pipeline in its entirety is
      depicted in the following diagram:

      <br/><br/>
      
      <a href="http://msdn.microsoft.com/en-us/library/ff476882%28VS.85%29.aspx">From the DirectX SDK:</a>

      <br/><br/>
      
      <img border="0" width="388" height="640" id="Picture 12" src="DeviceCreation_files/image002.jpg"/>

      <br/><br/>

      In this tutorial we will only focus on setting the bare
      minimum to get something visible on the screen, but we’ll come back to this
      time and time again to achieve all of our rendering and visual effects.

      <br/><br/>

      For our purposes, we will be configuring the Rasterizer
      Stage and the Output-Merger stage. For the former, we need to set a viewport
      specifying the bounds of our rendering. The Rasterizer Stage will use the
      viewport to transform vertices that are in homogenous clip space (-1 to 1 for X
      and Y and 0 to 1 for the Z range) into pixel coordinates. Typically, you set
      the viewport to be the size of the window to which you’ll be rendering:

      <br/><br/>

      <pre class="prettyprint">var viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);
context.Rasterizer.SetViewports(viewport);</pre>

      <br/>

      Additionally, we need to configure the output-merger state,
      which is responsible for combining all pixel data and sending it to the
      appropriate render targets. We need to specify at least one render target in
      order to render anything. In our case we want to use the swap chain’s back
      buffer as the render target, so we need to go about creating a <i>resource view</i>
      of the back buffer. In Direct3D 11, resources are typically manipulated using
      views instead of referring to the resource itself; this allows a single
      resource to be interpreted in different ways depending on how it is being used
      in that particular portion of the pipeline.

      <br/><br/>
      
      In order to create our render target view, we need to
      extract the back buffer from the swap chain. This can be done as shown, using
      the <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";
color:#2B91AF'>Resource</span><span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>.FromSwapChain</span> method:

<br/><br/>

      <pre class='prettyprint'>RenderTargetView renderTarget;
using (var resource = Resource.FromSwapChain&lt;Texture2D&gt;(swapChain, 0))
    renderTarget = new RenderTargetView(device, resource);</pre>

      <br/>

      Typically we extract the back buffer as a 2D image texture,
      but there may be situations where some other data type may be more appropriate,
      which is why the method is generic.

      <br/><br/>

      Finally, we set the render target in the output merger
      stage:

      <br/><br/>

      <pre class='prettyprint'>context.OutputMerger.SetTargets(renderTarget);</pre>

      <br/>
    </p>

    <h1>Rendering</h1>

    <p>
      We can now render to our device and see the output on the
      screen. In this tutorial we will be clearing the render target to a constant
      color and presenting the result to the screen. We do this inside the main loop
      so that it constantly updates and clears the screen. If you cover the window
      with another and then show it again, the constant clearing will ensure that no
      visual remnants of the occluding window will remain visible.

      <br/><br/>
      
      Clearing the render target is performed with the aptly named
      <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>ClearRenderTargetView</span>
      method:

      <br/><br/>

      <pre class='prettyprint'>public void ClearRenderTargetView(RenderTargetView view, Color4 color);</pre>

      <br/>
      
      This method takes a view of our render target and a color to
      clear to. The SlimDX <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:#2B91AF'>Color4</span><span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New"'> </span>type can take input in a
      variety of forms, and has an implicit conversion defined from the <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>
        System.Drawing.<span
style='color:#2B91AF'>Color</span>
      </span> type as well, so there is a great
      deal of flexibility in how you specify your colors.

      <br/><br/>
      
      Once the target has been cleared, we use the swap chain’s <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New"'>Present</span>
      method to flip the back buffer into front buffer memory.

      <br/><br/>

      <pre class='prettyprint'>public Result Present(int syncInterval, PresentFlags flags);</pre>

      <br/>
      
      The <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>syncInterval</span> parameter lets us specify
      options to synchronize back buffer flipping with the vertical blanking period
      of the monitor, which prevents tearing. Specifying zero won’t perform any
      synchronization at all, which can increase frame rates but may produce
      artifacts. The <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>flags</span>
      parameter lets you specify a few rarely-used presentation settings. Consult the
      documentation for more information on them.

      <br/><br/>
      
      Our final render loop now looks like this:
      
      <pre class='prettyprint'>MessagePump.Run(form, () =>
{
    // clear the render target to a soothing blue
    context.ClearRenderTargetView(renderTarget, new Color4(0.5f, 0.5f, 1.0f));
    swapChain.Present(0, PresentFlags.None);
});</pre>
      
      <br/>
    </p>

    <h1>Cleanup</h1>

    <p>
      Direct3D objects, and consequently SlimDX objects, need to
      be released when you are finished using them in order to clean up the
      associated memory and handles. Because Direct3D objects need to be released on
      the same thread on which they were created, SlimDX objects don’t implement a
      finalizer, and the user must call Dispose on each object manually.
 
 <br/><br/>

      <pre class='prettyprint'>// clean up all resources
// anything we missed will show up in the debug output
renderTarget.Dispose();
swapChain.Dispose();
device.Dispose();</pre>
      
      <br/>
      
      SlimDX tracks all live objects in the <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>ObjectTable</span>.
      You can access this class to get information about live objects. Additionally,
      when the program exits a list of all unreleased objects will get written to the
      debug output of Visual Studio. You can check this to make sure there aren’t any
      leaks in your program.
      <br/><br/>
    </p>

    <h1>Conclusion</h1>

    <p>
      The work spent creating the device lets us have a nice
      cleared window and provides a base for future rendering operations.  A
      screenshot of our work is shown:

      <br/><br/>
      
      <img border='0' width='476' height='366' id="Picture 15" src="DeviceCreation_files/image003.jpg"/>

      <br/><br/>

      Not much interesting yet, but it’s getting there. In our next tutorial, we will work on rendering our first 3D primitive.

      <br/><br/>

      Download the source code for this tutorial: <a href="DeviceCreation_files/DeviceCreation.zip">DeviceCreation.zip</a>
    </p>
    <br/>
    <br/>

  </div>
</div>

<?php	
	require_once("../footer.php");
?>