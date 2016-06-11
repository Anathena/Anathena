<?php
	$g_pageTitle = "SlimDX Tutorial - Simple Triangle";
	$g_pageName = "Tutorials";
	
	require_once("tutorial_header.php");
?>

<div id="content">
  <div class="wrapper">
    <h4>Direct3D 11 - Simple Triangle</h4>

    <p>
      Rendering primitives is the <i>raison d’être</i> of a 3D
      graphics API. This tutorial explains the process of supplying Direct3D with
      primitive data and ensuring that the entire graphics pipeline is properly
      configured so as to produce visual evidence of the primitives on the screen. By
      the end a single rendered triangle will be visible on screen.
      <br/><br/>
    </p>

    <h1>Vertex Data</h1>

    <p>
      The most basic 3D primitive is the triangle, and in order to
      render a triangle you must give Direct3D the positions of its three defining
      points, or vertices. Of course, there is a lot of other data you might want to
      pass in along with just the vertex positions; for example: color data, normals,
      texture coordinates, and blend weights are all types of data commonly described
      on a per-vertex basis. For now though, simple positions will do fine.

      <br/><br/>

      In order to supply vertex data to Direct3D, you need to
      compile all of your vertex data into an unmanaged memory buffer. In SlimDX,
      this can be accomplished using the <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:#2B91AF'>DataStream</span> class. Using a <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>DataStream</span>,
      you can write all of your vertex components into memory and then pass the final
      result to Direct3D. Most commonly a dedicated vertex structure is defined and
      then written into the buffer, but for our sample we will just write the
      vertices directly into the stream.

      <br/><br/>

      <pre class='prettyprint'>var vertices = new DataStream(12 * 3, true, true);
vertices.Write(new Vector3(0.0f, 0.5f, 0.5f));
vertices.Write(new Vector3(0.5f, -0.5f, 0.5f));
vertices.Write(new Vector3(-0.5f, -0.5f, 0.5f));</pre>
      <br/>

      We create the stream with a given starting size which must not
      be overrun. In this case, we have three vertices, with each vertex being the
      size of one three element vector, or 12 bytes. Each vertex is then added to the
      buffer using the generic<span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'> Write</span> method.

      <br/><br/>

      Because <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:#2B91AF'>DataStream</span> is in fact a <i>stream</i>
      of memory and not just a buffer (not the wisest design decision in retrospect,
      but what can you do?), the position of the read and write pointer is
      significant. In order for our vertices to be read in order by Direct3D, we must
      rewind the stream back to the beginning, like so:

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>vertices.Position = 0;</span>

      <br/><br/>
    </p>

    <h2>Vertex Buffer</h2>

    <p>
      Once we have our vertex data in memory, we need to load it
      into a Direct3D <i>vertex buffer</i>, which can then be passed into the
      graphics pipeline.

      <br/><br/>

      <pre class='prettyprint'>var vertexBuffer = new Buffer(device, vertices, 12 * 3, ResourceUsage.Default, 
    BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);</pre>
      <br/>

      There are several parameters here, but the important ones
      are the second and third, which pass in our stream of vertex data and its total
      size, and the fifth, which indentifies this buffer as containing vertex data in
      particular. See the documentation for additional information on the rest of the
      parameters.
      <br/><br/>
    </p>

    <h1>Shaders</h1>

    <p>
      How exactly does Direct3D know how to take the vertex data
      we just gave generated and turn it into pixels on the display? As mentioned in
      the previous tutorial, Direct3D uses a pipeline system to take input vertices
      and process them in multiple ways to produce the final output pixels.

      <br/><br/>

      <img width='388' height='640' id="Picture 12" src="SimpleTriangle_files/image001.jpg"/>

      <br/><br/>

      As also mentioned, each stage is configurable or customizable
      in some way. We saw that for the Output-Merger and Rasterizer stages, we could
      call methods on the <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:#2B91AF'>DeviceContext</span> to configure how they
      operated (such as setting a render target or a viewport). For other stages, we
      can completely specify the operation of the stage by writing short programs
      called shaders in HLSL, or High Level Shader Language. When assigned, Direct3D
      will pass a set of designated data into our shaders and pass their output on to
      the rest of the pipeline.

      <br/><br/>

      In Direct3D 11, there are five different shader types that
      correspond to five of the stages in the pipeline depicted above. They are
      Vertex Shaders, Hull Shaders, Domain Shaders, Geometry Shaders, and Pixel
      Shaders. Direct3D 11 additionally defines a separate set of shaders called Compute
      Shaders that are more generic and don’t directly interact with the graphics
      pipeline, so we won’t worry about them now. Each shader type mentioned is used
      to implement the functionality for the corresponding section of the pipeline.

      <br/><br/>

      Hull shaders, domain shaders, and geometry shaders are all
      optional stages that will be skipped if no HLSL program is provided for them.
      They will be discussed in a future tutorial. For now, we will focus on the two
      stages that <i>must</i> be implemented if any rendering is to be done: vertex
      shaders and pixel shaders.
      <br/><br/>
    </p>

    <h2>Vertex Shaders</h2>

    <p>
      The Vertex Shader Stage is the first stage of the pipeline
      that is completely programmable. It has one main task: take input vertices and
      transform them into homogenous clip space. It can, of course, perform other
      operations, such as displacement mapping, vertex morphing, or per-vertex
      lighting, but it most always output one transformed vertex for every input
      vertex it receives.

      <br/><br/>

      All shaders in Direct3D 11 are written on top of the <i>common shader core</i>,
      which means they share a set of intrinsic functions, syntax,
      and methods for accessing resources and constant buffers. They are all written
      in HLSL, the syntax of which is closely related to C and C++. Shader
      programming can be complex and is certainly a vast topic; whole books are
      written on different techniques and effects you can accomplish with shaders. You
      can find a complete syntax and function reference on <a href="http://msdn.microsoft.com/en-us/library/bb509561%28VS.85%29.aspx">MSDN</a>,
      and can find inspiration for shader content in graphics whitepapers and demos.

      <br/><br/>

      Since our input data for this sample is already in
      homogenous clip coordinates, we don’t have to worry about doing anything
      interesting in our vertex shader. We can simply take the input vertex and pass
      it straight through to the rest of the pipeline:

      <br/><br/>

      <pre class="prettyprint lang-hlsl">float4 VShader(float4 position : POSITION) : SV_POSITION
{
    return position;
}</pre>
      <br/>

      This is our vertex shader function, which we’ve chosen to
      call “VShader”. It takes our input position, as a four element floating point
      vector (<span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";
color:blue'>float4</span>) and returns it directly without modifying it. This
      is all very familiar to anyone comfortable with the C family of programming
      languages, except for the <i>semantics</i> which are attached to each parameter
      and return type by a colon (colored in red above). These semantics are used to
      provide metadata about pieces of information that are flowing through the
      pipeline. Input semantics, such as the one attached to the <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>position</span> parameter,
      are matched up with semantics attached to the vertex data submitted by the
      application. This is how each piece of vertex data is mapped into the vertex
      shader. Output semantics, such as the <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:#A31515'>SV_POSITION</span> semantic
      attached to the return value of the function, mark the important pieces of data
      that will be passed on to the rest of the pipeline.

      <br/><br/>

      The combination of input parameters and their semantics
      define the <i>input signature</i> of the shader, and the same is true for the
      shader outputs forming the <i>output signature</i>. Direct3D 11 provides a <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>ShaderSignature</span>
      class that represents these formats and uses them to validate input and shader setup at runtime.
      <br/><br/>
    </p>

    <h2>Pixel Shaders</h2>

    <p>
      The Pixel Shader Stage follows immediately after the
      Rasterizer Stage, which was discussed in the previous tutorial. After the
      rasterizer has done its job of clipping input data and converting it into
      pixels on screen, the pixel shader runs to calculate the final color value of
      the pixel. As input, the pixel shader takes output data coming from the vertex
      shader, interpolated across the three vertices of the triangle. The pixel
      shader is required to output a final color value for the pixel, and follows the
      same general layout of the vertex shader.

      <br/><br/>

      <pre class='prettyprint lang-hlsl'>float4 PShader(float4 position : SV_POSITION) : SV_Target
{
    return float4(1.0f, 1.0f, 0.0f, 1.0f);
}</pre>
      <br/>

      Here we see the output of the vertex shader, with an <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#A31515'>SV_POSITION</span>
      semantic, being used as the input of the pixel shader. The color value output
      of the pixel shader has the special <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:#A31515'>SV_Target</span> semantic
      designating that the result of the pixel shader is to be sent to the current
      render target.

      <br/><br/>

      In this example we output a constant color value of yellow
      (full red, green, and alpha components) for every input pixel. For a more
      realistic shader other inputs such as texture coordinates and normals would be
      used to provide realistic texturing and lighting of the pixel.
      <br/><br/>
    </p>

    <h2>Loading, Compiling, and Creating Shaders</h2>

    <p>
      Once you have your shaders written, you need to load and
      compile them before they can be used in your pipeline. This is all handled
      through the <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";
color:#2B91AF'>ShaderBytecode </span>class, which contains methods to compile
      shader source. We will be using the <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black;'>CompileFromFile</span> method, which has the
      following signature:

      <br/><br/>

      <pre class='prettyprint'>public static ShaderBytecode CompileFromFile(string fileName, string entryPoint, string profile, ShaderFlags shaderFlags, EffectFlags effectFlags);</pre>
      <br/>

      There are many other overloads for this function that you
      can explore on your own. For now, let’s take a look at some of the parameters.

      <br/><br/>

      <ul style="margin-left: 2em;">
        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>fileName</span>
          is pretty straightforward; it’s the name and path of the shader source file on
          disk. In our example, both shader functions are located in one shared <i>triangle.fx</i>
          file, but they could have been separated into two different files.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>entryPoint</span>
          is the name of the shader function we are targeting. Since a single source file
          could contain multiple functions, this is the name of the shader entry point.
          For our vertex shader it would be <i>VShader</i> and for the pixel shader it
          would be <i>PShader</i>.
        </li>

        <li>
          <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>profile</span>
          is the name of the target profile. HLSL defines <i>shader profiles</i> that specify
          a set of functionality that hardware adapters must support. For any card that
          support Direct3D 10 and up, the corresponding Shader Model 4.0 must be
          supported. See the <a href="http://msdn.microsoft.com/en-us/library/bb509626%28VS.85%29.aspx">DirectX documentation</a> for a list of all shader profiles.
        </li>
      </ul>
      <br/>

      The last two parameters allow you to provide extra flags
      that control advanced options for the shader. We won’t be using any of them
      here.

      <br/><br/>

      Once we have a compiled shader bytecode for a shader, we can
      create the actual shader object. The following code snippet shows the entire
      process for both the vertex shader and the pixel shader.

      <br/><br/>

      <pre class="prettyprint">using (var bytecode = ShaderBytecode.CompileFromFile("triangle.fx", "VShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
    vertexShader = new VertexShader(device, bytecode);

// load and compile the pixel shader
using (var bytecode = ShaderBytecode.CompileFromFile("triangle.fx", "PShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
    pixelShader = new PixelShader(device, bytecode);</pre>
      <br/>

      Once we have these shader interfaces, we can set them to the
      pipeline to customize how our primitives are rendered. We will see how to
      accomplish that in a moment. First, we must work on getting our primitives into
      the pipeline.
      <br/><br/>
    </p>

    <h1>The Input Assembler</h1>

    <p>
      The very beginning of the graphics pipeline is the input
      assembler.  The input assembler is responsible for taking input data, such as
      vertices and indices, assembling all the various components into a consistent
      stream and sending it all into the pipeline to be processed and rendered. In
      order to submit the vertex data for our triangle, we have to provide supporting
      information expected by the input assembler.
      <br/><br/>
    </p>

    <h2>Input Layout</h2>

    <p>
      The first piece of data we need is an <i>input layout</i>,
      which is a set of structures defining the layout of our vertex data. Since our
      vertex data can contain any sort of input we want, we need to match it all up
      with a set of semantics so that it can all be properly identified in the vertex
      shader. To do this, we create an instance of the <span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:#2B91AF'>InputLayout</span>
      class:

      <br/><br/>

      <pre class='prettyprint'>public InputLayout(Device device, ShaderSignature shaderSignature, InputElement[] elements);</pre>
      <br/>

      Besides taking a reference to the 3D device, the <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>InputLayout</span>
      constructor also demands a <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:#2B91AF'>ShaderSignature</span> and a list of <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:#2B91AF'>InputElements</span>.
      As mentioned earlier, the <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:#2B91AF'>ShaderSignature</span><span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New"'> </span>type
      is used to represent the <i>input signature</i> of a vertex shader. Direct3D
      demands such a signature here so that it can validate a given vertex shader
      with the provided input elements to ensure that the data can be successfully
      mapped. Getting such a signature once we have a compile shader bytecode is
      simple; just use the <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:#2B91AF'>ShaderSignature</span><span style='font-size:10.0pt;
line-height:115%;font-family:"Courier New";color:black;'>.GetInputSignature() </span>method
      to extract it and pass it to the function.

      <br/><br/>

      Besides the signature, we need to pass an array of elements
      describing the layout of our vertex data in memory. Each element describes a
      piece of the vertex data, including its offset in memory, its data format and
      size, and the semantic name used to match it to a parameter in the vertex
      shader.

      <br/><br/>

      In our program, each vertex only has a 12 byte position
      element, so we will create our input element list like so:

      <br/><br/>

      <pre class='prettyprint'>var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0) };</pre>
      <br/>

      Note our how <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:#A31515'>POSITION</span> semantic matches the
      one used in the vertex shader we wrote earlier. The input layout will take this
      element and validate it against the shader signature we extracted earlier from
      the vertex shader.
      <br/><br/>
    </p>

    <h2>Primitive Topology</h2>

    <p>
      The data necessary to render a triangle can change depending
      on how it is laid out. In the simplest case, a <i>Triangle List</i>, every
      three vertices in the input stream represent a single triangle. This can,
      however, be changed by taking advantage of other <i>primitive topologies</i>.
      If successively rendered triangles share a side, only one additional vertex is
      necessary to specify the additional triangle. This is called a <i>Triangle Strip</i>. This topology is demonstrated in the following image:

      <br/><br/>

      <img border='0' width='474' height='315' id="Picture 1" src="SimpleTriangle_files/image002.jpg"/>

      <br/><br/>

      When drawing triangle 2 after triangle 1, only the extra
      vertex D need be sent to accomplish a full triangle. The advantage of this is
      that the size of the vertex buffer is reduced, which can improve performance.

      <br/><br/>

      There are several other topologies besides lists and strips.
      Since we only have one triangle, and are specifying all three vertices, we will
      use a <i>Triangle List</i> as our topology of choice.
      <br/><br/>
    </p>

    <h1>Configuring the Pipeline</h1>

    <p>
      Once we have our input layout and topology chosen, we can
      set it all to the Input Assembler along with our vertex buffer to kick off the
      rendering pipeline. Setting the primitive topology and input layout is done by
      simply setting the appropriate property on the Input Assembler:

      <br/><br/>

      <pre class="prettyprint">context.InputAssembler.InputLayout = layout;
context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;</pre>
      <br/>

      To set our vertex buffer, we need to give one more piece of
      information: the stride of the vertex data inside the buffer. The <i>stride</i>
      refers to the amount of memory, in bytes, between each successive vertex. In
      our case, each vertex is 12 bytes long, so our stride is 12.

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>
        context.InputAssembler.SetVertexBuffers(0, <span
style='color:blue'>new</span> <span style='color:#2B91AF'>VertexBufferBinding</span>(vertexBuffer,
        12, 0));
      </span>

      <br/><br/>

      That’s all that’s necessary to fully configure the Input
      Assembler stage of the pipeline. Now we can move on to setting up the Vertex Shader
      and Pixel Shader portions of the pipeline using the vertex shader and pixel
      shader objects we compiled and created earlier. Once again, the actual setting
      of the pipeline state is fairly simple:

      <br/><br/>

      <pre class='prettyprint'>context.VertexShader.Set(vertexShader);
context.PixelShader.Set(pixelShader);</pre>
      <br/>

      Our graphics pipeline is now fully configured and ready to
      render our triangle.
      <br/><br/>
    </p>

    <h1>Drawing the Triangle</h1>

    <p>
      After all that work we did to configure everything, the
      actual drawing is relatively simple. To do so, we call the <span
style='font-size:10.0pt;line-height:115%;font-family:"Courier New";color:black;'>Draw</span>
      method on our device context, which has the following signature:

      <br/><br/>

      <pre class='prettyprint'>public void Draw(int vertexCount, int startVertexLocation);</pre>
      <br/>

      The first parameter specifies the number of vertices to draw
      from the currently active vertex buffer in the Input Assembler. We had three
      vertices for our single triangle. The <span style='font-size:10.0pt;line-height:
115%;font-family:"Courier New";color:black'>startVertexLocation</span> parameter specifies
      an offset into the vertex buffer, in case we didn’t want to use all of the
      vertices there. Since we do want to use all of our vertices, the final draw
      call looks like this:

      <br/><br/>

      <span style='font-size:10.0pt;line-height:115%;font-family:
"Courier New";color:black;'>context.Draw(3, 0);</span>

      <br/><br/>Voila! We have a triangle.<br/><br/>

      <img border='0' width='504' height='394' id="Picture 4" src="SimpleTriangle_files/image003.jpg"/>

      <br/><br/>
    </p>

    <h1>Form Resizing</h1>

    <p>
      There is one thing left we could improve on in our simple
      program. If you try resizing the window, you’ll notice that the triangle gets
      squished and stretched. This is because our swap chain’s back buffer isn’t
      resized along with the window, causing Direct3D to stretch the final image
      instead of rendering the scene properly. In order to fix this, we will need to
      override our form’s resizing event and recreate the render target whenever the
      size changes:

      <br/><br/>

      <pre class="prettyprint">// handle form size changes
form.UserResized += (o, e) =>
{
    renderTarget.Dispose();

    swapChain.ResizeBuffers(2, 0, 0, Format.R8G8B8A8_UNorm, SwapChainFlags.AllowModeSwitch);
    using (var resource = Resource.FromSwapChain&lt;Texture2D&gt;(swapChain, 0))
        renderTarget = new RenderTargetView(device, resource);

    context.OutputMerger.SetTargets(renderTarget);
};</pre>
      <br/>

      Most of this code is similar to the initial set up we did
      for rendering. Note however the <span style='font-size:10.0pt;line-height:115%;
font-family:"Courier New";color:black;'>ResizeBuffers </span>method, which does the bulk of
      the work of resizing the back buffer without having to recreate the swap chain
      entirely.
      <br/><br/>
    </p>

    <h1>Cleanup</h1>

    <p>
      Once again, we can’t forget to clean up all of our objects
      when the program has finished. We ended up with a lot more this time around:

      <br/><br/>

      <pre class='prettyprint'>// clean up all resources
// anything we missed will show up in the debug output
vertices.Close();
vertexBuffer.Dispose();
layout.Dispose();
inputSignature.Dispose();
vertexShader.Dispose();
pixelShader.Dispose();
renderTarget.Dispose();
swapChain.Dispose();
device.Dispose();</pre>
      <br/>
    </p>

    <h1>Conclusion</h1>

    <p>
      We have now shown how to create vertex data, compile and
      apply shaders, and configure the graphics pipeline in order to render a 3D
      primitive. This is the backbone of all 3D graphics, so all of your applications
      will build upon this base so understand it well.

      <br/><br/>

      In the future, we will look at further configuration of the
      pipeline, explore more in-depth shaders, and discuss how 3D primitives are
      positioned and transformed to provide a 3D perspective of a virtual object.

      <br/><br/>

      Download the source code for this tutorial: <a href="SimpleTriangle_files/SimpleTriangle.zip">SimpleTriangle.zip</a>
      <br/>
      <b>Note:</b> Requires the latest build of the library from SVN.
    </p>

    <br/>
    <br/>

  </div>
</div>

<?php	
	require_once("../footer.php");
?>