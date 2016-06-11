<?php
	$g_pageTitle = "SlimDX Features";
	$g_pageName = "Features";
	
	require_once("header.php");
?>

<div id="content">
  <div class="wrapper">

    <div class="paragraph">
      <h2>SlimDX Features</h2>
      <p>
        <h3>Supported Systems</h3>
        SlimDX supports a wide range of APIs, operating systems, and configurations. All versions of Windows XP, Vista, Server 2003, Server 2008, and 7 are supported, for both 32 and 64 bit.
        There is a minimum requirement of .NET 2.0, but any subsequent version (including or excluding service packs) will work.
      </p>
      <br/>
      <br/>
      <p>
        <h3>Supported APIs</h3>
        <div class="mclist">
          <ul>
            <li>Math Library</li>
            <li>Direct3D9</li>
            <li>Direct3D9Ex</li>
            <li>Direct3D10</li>
            <li>Direct3D 10.1</li>
            <li>Direct3D 11</li>
            <li>Direct2D</li>
            <li>D3DCompiler</li>
            <li>DirectWrite</li>
            <li>DirectInput</li>
            <li>DirectSound</li>
            <li>DXGI</li>
            <li>DXGI 1.1</li>
            <li>Windows Multimedia</li>
            <li>Raw Input</li>
            <li>X3DAudio</li>
            <li>XAPO</li>
            <li>XACT3</li>
            <li>XAudio2</li>
            <li>XInput</li>
          </ul>
          <br/>
        </div>
      </p>
      <br/>
      <p>
        <h3>Interoperability</h3>
        SlimDX has full support for sharing DirectX objects with other libraries.
        All SlimDX objects expose their native pointers for use by other code, and SlimDX can wrap native pointers with its own objects.
        This allows seamless cooperation with libraries such as DirectShow.NET, CUDA.NET, native code DirectX libraries, and more.
      </p>
      <br/>
      <br/>
      <p>
        <h3>64-bit Systems and "Any CPU"</h3>
        SlimDX includes x64 bit assemblies, and is the <i>only</i> DirectX interface for .NET to do so.
        The redistributable and SDK installers automatically register the correct assemblies.
        Applications can also compile for the "Any CPU" architecture when using SlimDX, and the correct version will be selected.
        As a result, SlimDX based applications can smoothly target both x86-32 and x86-64 bit machines without the headaches introduced by other similar libraries.
        (Itanium IA64 is not a supported configuration, sorry.)
      </p>
      <br/>
      <br/>
      <p>
        <h3>Performance</h3>
        Performance is a crucial factor in all of the design work done on SlimDX, and the team works carefully to ensure that overhead is kept to a minimum. SlimDX has proven to
        be faster than both XNA and MDX in several performance tests, although the margin is often negligible. Additionally, an experimental offshoot of SlimDX called SlimGen
        that is still under development promises to bring SIMD support straight into the math library, boosting performance considerably.
      </p>
      <br/>
      <br/>
      <p>
        <h3>.NET 4.0 Support</h3>
        SlimDX is the <i>only</i> DirectX interface for .NET to provide full .NET 4.0 binaries. This means no having to go through extra
        configuration steps to ensure that your .NET 4.0 application can use SlimDX properly.
      </p>
    </div>

    <div class="paragraph">
      <h2>Choosing a Managed Game API</h2>
      <p>
        There are several APIs available for developing games in .NET. The following table and feature discussions demonstrate the differences
        between these APIs, hopefully making it easy to pick the one that is best suited for your needs.
      </p>
      <br/>
      <table class="table table_gradient" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr class="table_header">
            <th class="table_header_cell1">Features</th>
            <th>SlimDX</th>
            <th>XNA</th>
            <th>MDX</th>
            <th>Windows Code Pack</th>
            <th>OpenTK</th>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Actively Developed</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Native .NET 4 Binaries</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Source License</div>
            </td>
            <td>
              <div>
                <a href="http://www.opensource.org/licenses/mit-license.html">MIT</a>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <a href="http://code.msdn.microsoft.com/WindowsAPICodePack/Project/License.aspx" style="color:#4e4e4e">Microsoft</a>
              </div>
            </td>
            <td>
              <div>
                <a href="http://www.opensource.org/licenses/mit-license.html" style="color:#4e4e4e">MIT</a>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Support for x64</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>ClickOnce</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>SIMD Math</div>
            </td>
            <td>
              <div>
                <a href="http://code.google.com/p/slimgen/">Experimental</a>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>WPF Interop</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>GAC Installer</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
        </tbody>
      </table>
      <table class="table table_gradient" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr class="table_header">
            <th class="table_header_cell1">Platforms</th>
            <th>SlimDX</th>
            <th>XNA</th>
            <th>MDX</th>
            <th>Windows Code Pack</th>
            <th>OpenTK</th>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Windows</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Xbox 360</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Windows Phone 7</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Mac</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Linux</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <table class="table table_gradient" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr class="table_header">
            <th class="table_header_cell1">Graphics APIs</th>
            <th>SlimDX</th>
            <th>XNA</th>
            <th>MDX</th>
            <th>Windows Code Pack</th>
            <th>OpenTK</th>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Direct3D 9</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <div>&nbsp;</div>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Direct3D 10</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Direct3D 11</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>OpenGL</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Direct2D</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
        </tbody>
      </table>
      <table class="table table_gradient" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr class="table_header">
            <th class="table_header_cell1">Input APIs</th>
            <th>SlimDX</th>
            <th>XNA</th>
            <th>MDX</th>
            <th>Windows Code Pack</th>
            <th>OpenTK</th>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Keyboard</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Mouse</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Joystick</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>Xbox 360 Gamepad</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
        </tbody>
      </table>
      <table class="table table_gradient" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr class="table_header">
            <th class="table_header_cell1">Audio APIs</th>
            <th>SlimDX</th>
            <th>XNA</th>
            <th>MDX</th>
            <th>Windows Code Pack</th>
            <th>OpenTK</th>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>DirectSound</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>XACT</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>XAudio2</div>
            </td>
            <td>
              <div>
                <img src="../images/sdx_check_icon.png"/>
              </div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
          </tr>
          <tr>
            <td class="table_cell1">
              <div>OpenAL</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>&nbsp;</div>
            </td>
            <td>
              <div>
                <img src="../images/check_icon.png"/>
              </div>
            </td>
          </tr>
        </tbody>
      </table>

      <p>
        <h3>When to use XNA Game Studio</h3>
        Use XNA when you need Xbox360 or Windows Phone 7 support. If you are only targeting the Windows operating system
        and are comfortable with the added complexity, SlimDX provides access to newer rendering APIs such as Direct3D 11 and Direct2D.
        Even if you end up using XNA, SlimDX is still a viable choice for creating tools and plugins for the content pipeline.
      </p>
      <br/>
      <br/>
      <p>
        <h3>When to use MDX</h3>
        Don't. MDX has been deprecated for many years now. It still targets an older .NET framework version and suffers from many bugs and flaws
        such as <a href="http://msdn.microsoft.com/en-us/library/ms172219.aspx">loader lock</a> that will never be fixed. SlimDX supports
        everything exposed by MDX and much more, so there's no excuse for choosing MDX for a new project.
      </p>
      <br/>
      <br/>
      <p>
        <h3>When to use the Windows API Code Pack</h3>
        It is the opinion of the SlimDX team that the Code Pack is an inferior choice when it comes to DirectX support for .NET.
        While its offering is certainly adequate, it is not better than SlimDX in any area and lacks quite a few features
        that are present in SlimDX. Additionally it does not support Direct3D 9, which can be a deal breaker if you require Windows XP support.
      </p>
      <br/>
      <br/>
      <p>
        <h3>When to use OpenTK</h3>
        Use OpenTK whenever you want OpenGL support, or must support the Mac and Linux platforms. If you are only targeting Windows,
        you may be better off with Windows specific technologies like DirectX.
      </p>
    </div>

  </div>
</div>

<?php	
	require_once("footer.php");
?>

