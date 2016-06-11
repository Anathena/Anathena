<?php
	$g_pageTitle = "SlimDX Download Page (February 2010)";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">
		<div id="left">
			<h1>February 2010</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
				<p>
				The February 2010 release of SlimDX is now out!
				<p>
				This release focused on fixing bugs and ensuring that the new Windows 7 APIs were stable. Additionally,
        the sample framework and samples package were completely overhauled to better demonstrate aspects of
        the library. This should be the most feature-full and stable release of SlimDX yet.
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
				<a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20%28February%202010%29.msi"><img src="images/latest.jpg" alt="Latest User Runtime" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Developer SDK</h2>
				<p>
				In the past, SlimDX was split into several different pieces for the core runtime, documentation, samples, and individual binaries.
				This is no longer the case; we have decided to follow the same model as the DirectX team and produce a redistributable runtime and a Developer SDK.
				The SDK includes all of the binaries, documentation, and samples.
				It also includes all of the files necessary to build custom installation packages if desired.
				The SDK supports both 32 bit and 64 bit systems.
				<b>Note:</b> In this release, the samples must be unpackaged manually, via a start menu shortcut.
				Make sure to unpack the samples to your user directory, not Program Files.
				</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20SDK%20%28February%202010%29.msi"><img src="images/latest.jpg" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#February_2010" rel="nofollow">What&#x27;s Changed in the February 2010 SDK</a> -- Information on the work we did for the February 2010 SDK release.</li>
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
				<li><a href="http://slimdx.org/docs/#February_2010">Release Notes</a></li>
				<li><a href="http://slimdx.org/docs">Documentation</a></li>
				<li><a href="license.php">Licensing Information</a></li>
				<li><a href="releases.php">Previous Releases</a></li>
				<li><a href="http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=2c7da5fb-ffbb-4af6-8c66-651cbd28ca15">DirectX SDK (February 2010)</a></li>
			</ul>
		</div>
		
		<div id="clear"></div>
    </div>
</div>

<?php	
	require_once("footer.php");
?>

