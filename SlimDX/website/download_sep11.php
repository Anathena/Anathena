<?php
	$g_pageTitle = "SlimDX Download Page (September 2011)";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">
		<div id="left">
			<h1>September 2011</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
        <p>
          The September 2011 release of SlimDX is now out!
        </p>
        <br />
				<p>
          The September 2011 release was a minor stability release.
          Once again this release does not reflect a corresponding release of the DirectX SDK.
          While several improvements were made, along with internal upgrades to the SlimDX website, server,
          and documentation system, the majority of development effort has been focused on the V2 branch,
          which was recently moved to GitHub.
        </p><br />
			</div>
			
			<div class="paragraph">
				<h2>End User Runtime</h2>
        <p>
          This is the package for end users.
          It works on both 32 and 64 bit systems, for all versions of Windows supported by the .NET Framework. Pick the one that matches your target framework.
        </p>
        <br />
        <p>
        If you're just trying to run a SlimDX program that someone else gave you, you will need one of these installers.
				Developers should point users to one of these links, or include it with their own installers.
				However, developers should install the SDK below, not this package.
				See the documentation for more information.
				</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20for%20.NET%202.0%20%28September%202011%29.msi"><img src="images/install_20.png" alt=".NET 2.0 User Runtime" /></a>
        <!-- <a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20for%20.NET%204.0%20%28March%202011%29.exe"><img src="images/install_40.png" alt=".NET 4.0 User Runtime" /></a> -->
        <br />The 4.0 runtime is not available for this version. It will be available with a December 2011 update.
			</div>
			
			<div class="paragraph">
				<h2>Developer SDK</h2>
				<p>The SDK includes all of the binaries, documentation, and samples.
				It also includes all of the files necessary to build custom installation packages if desired.
				The SDK supports both 32 bit and 64 bit systems.
				Note that for the September 2011 SDK release, Visual C++ runtimes are not included and must be installed separately.
				</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20SDK%20%28September%202011%29.msi"><img src="images/install_sdk.png" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#What's_New_In_September_2011" rel="nofollow">What&#x27;s Changed in the September 2011 SDK</a> -- Information on the work we did for the September 2011 SDK release.</li>
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
				<li><a href="http://slimdx.org/docs/#What's_New_In_September_2011">Release Notes</a></li>
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

