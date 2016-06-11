<?php
	$g_pageTitle = "SlimDX Download Page (August 2009)";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">
		<div id="left">
			<h1>August 2009</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
				<p>
				The August 2009 release of SlimDX is now out!
				<p>
				This is a major release in terms of new functionality, mainly due to the long-awaited inclusion of Windows 7 support.
				The new Windows 7 features include Direct3D 11, Direct2D, and DirectWrite.
				<b>Note:</b> Direct3D 11 is still considered somewhat experimental, and developers should seriously consider following the current Subversion code if 11 is an active development target.
				</p><br />
			</div>
			
			<div class="paragraph">
				<h2>End User Runtime</h2>
				<p>
				This is the package for end users.
				It works on both 32 and 64 bit systems, for all versions of Windows supported by the .NET Framework 2.0.
				If you're just trying to run a SlimDX program that someone else gave you, this is the installer you want to download.
				Developers should point users to this link, or include it with their own installers.
				However, developers should install the SDK below, not this package.
				See the documentation for more information.
				</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20%28August%202009%29.msi"><img src="images/latest.jpg" alt="Latest User Runtime" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Developer SDK</h2>
				<p>
				In the past, SlimDX was split into several different pieces for the core runtime, documentation, samples, and individual binaries.
				This is no longer the case; we have decided to follow the same model as the DirectX team and produce a redistributable runtime and a Developer SDK.
				The SDK includes all of the binaries, documentation, and samples.
				It also includes all of the files necessary to build custom installation packages if desired.
				The SDK supports both 32 bit and 64 bit systems.
				</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20SDK%20%28August%202009%29.exe"><img src="images/latest.jpg" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#August_2009" rel="nofollow">What&#x27;s Changed in the August 2009 SDK</a> -- Information on the work we did for the August 2009 SDK release.</li>
				<li><a href="http://slimdx.org/" rel="nofollow">SlimDX Main Page</a> -- Our main homepage. This page makes it fairly straightforward to get to all of the other pages related to this project.</li>
				<li><a href="http://slimdx.org/docs/#Debugging_Tips" rel="nofollow">Debugging Tips</a> -- Tips on how to work on and debug applications using SlimDX. It&#x27;s a really good idea to be familiar with the contents of this page, and check on it every so often, because it is updated periodically.</li>
				</ul>
			</div>
			
			<div class="paragraph">
				<h2>Remarks</h2>
				<p>
				We have updated our documentation so that it now links to the DirectX documentation for many classes, taking you straight to the original unmanaged type that a SlimDX type represents.
				However, this linking is machine generated, and we haven't filled it in for everything yet.
				If you see something that is incorrectly linked, please report it to <a href="http://code.google.com/p/slimdx/issues/detail?id=464">issue 464</a>.
				</p><br />
				<p>
				This release of SlimDX includes support for Microsoft's Windows 7 technologies.
				Although they are no longer experimental, the support is very new and not considered to be especially stable.
				Please don't hesitate to report bugs, or anything that looks like a bug.
				We don't mind!
				It's also recommended that you follow the latest Subversion version of the code if you are developing with 11, as we're expecting to fix lots of things.
				</p><br />
			</div>
		</div>
	
		<div id="right">
			<ul class="sidemenu">
				<li><a href="http://slimdx.org/docs/#August_2009">Release Notes</a></li>
				<li><a href="http://slimdx.org/docs">Documentation</a></li>
				<li><a href="license.php">Licensing Information</a></li>
				<li><a href="releases.php">Previous Releases</a></li>
				<li><a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=b66e14b8-8505-4b17-bf80-edb2df5abad4&displaylang=en">DirectX SDK (August 2009)</a></li>
			</ul>
		</div>
		
		<div id="clear"></div>
    </div>
</div>

<?php	
	require_once("footer.php");
?>

