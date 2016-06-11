<?php
	$g_pageTitle = "SlimDX Download Page (January 2012)";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">
		<div id="left">
			<h1>January 2012</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
        <p>
          The January 2012 release of SlimDX is now out!
        </p>
        <br />
		<p>
          The January 2012 release was a minor stability release.
          Once again this release does not reflect a corresponding release of the DirectX SDK.
          Development effort was spent on fixing minor bugs and resolving issues in the installer and deployment infrastructure. 
        </p>
        <p>
          <font color="red">NOTE:</font> As of this release, samples are no longer part of the SDK but shipped as a separate download.
        </p><br />
			</div>
			
			<div class="paragraph">
			<h2>End User Runtime</h2>
			<p><font color="red">NOTE:</font> Application developers do <b>not</b> need these files.</p>
        <p>
          These packages are for end users and install all prerequisites for SlimDX based software.
          If you're just trying to run a SlimDX based program that someone else gave you, you will need one of these installers.
          Developers do not need this except for distribution purposes, and should install the SDK below instead.
          The SDK includes these files.
        </p>
        <br />
        <p>
        <table class="simple_table">
          <tr>
            <td style="border: 0;"></td>
            <td style="font-weight: bold;">x86 (32-bit)</td>
            <td style="font-weight: bold;">x64 (64-bit)</td>
          </tr>
          <tr>
            <td style="text-align: center; font-weight: bold;">.NET 2.0</td>
            <td colspan="2"><a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20.NET%202.0%20%28January%202012%29.msi">Download</td>
          </tr>
          <tr>
            <td style="font-weight: bold;">.NET 4.0</td>
            <td><a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20.NET%204.0%20x86%20%28January%202012%29.msi">x86 Download</a></td>
            <td><a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20.NET%204.0%20x64%20%28January%202012%29.msi">x64 Download</a></td>
          </tr>
        </table>
			</div>
			
			<div class="paragraph">
				<h2>Developer SDK</h2>
				<p>The SDK includes all of the binaries, documentation, and samples.
				It also includes all of the files necessary to build custom installation packages if desired.
				The SDK supports both 32 bit and 64 bit systems.
				</p><br />
				<a href="http://code.google.com/p/slimdx/downloads/detail?name=SlimDX%20SDK%20%28January%202012%29.msi"><img src="images/install_sdk.png" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#What's_New_In_January_2012" rel="nofollow">What&#x27;s Changed in the January 2012 SDK</a> -- Information on the work we did for the January 2012 SDK release.</li>
				<li><a href="http://slimdx.org/" rel="nofollow">SlimDX Main Page</a> -- Our main homepage. This page makes it fairly straightforward to get to all of the other pages related to this project.</li>
				<li><a href="http://slimdx.org/docs/#Managed_Message_Loop" rel="nofollow">Managed Message Loop</a> -- Describes the rationale behind the recommended message loop for SlimDX applications.</li>
				<li><a href="http://slimdx.org/docs/#Debugging_Tips" rel="nofollow">Debugging Tips</a> -- Tips on how to work on and debug applications using SlimDX. It&#x27;s a really good idea to be familiar with the contents of this page, and check on it every so often, because it is updated periodically.</li>
				</ul>
			</div>
			
			<div class="paragraph">
				<h2>Remarks</h2>
				<p>
				We have updated our documentation so that it now links to the DirectX documentation for many classes, taking you straight to the original unmanaged type that a SlimDX type represents.
				However, this linking is machine generated, and we haven't filled it in for everything yet.
				If you see something that is incorrectly linked, please report it in our <a href="http://code.google.com/p/slimdx/issues/list">issue tracker</a>.
				</p><br />
			</div>
		</div>
	
		<div id="right">
			<ul class="sidemenu">
				<li><a href="http://slimdx.org/docs/#What's_New_In_January_2012">Release Notes</a></li>
				<li><a href="http://slimdx.org/docs">Documentation</a></li>
				<li><a href="license.php">Licensing Information</a></li>
				<li><a href="releases.php">Previous Releases</a></li>
				<li><a href="http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=3021d52b-514e-41d3-ad02-438a3ba730ba">DirectX SDK (June 2010)</a></li>
			</ul>
		</div>
		
		<div id="clear"></div>
    </div>
</div>

<?php	
	require_once("footer.php");
?>

