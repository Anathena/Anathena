<?php
	$g_pageTitle = "SlimDX Download Page";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">    
		<div id="left">
			<h1>November 2008</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
				<p>
				The November 2008 release of SlimDX is now out! For this version of SlimDX, there has been a heavy focus on making it easier for developers as well as users to work with SlimDX. There are plenty of bug fixes, but the big news is updated and revised documentation, along with a completely rebuilt installation system. 
				As always, SlimDX's development is driven by requests from developers and users, so we encourage people to contact us if they have any questions, suggestions, or general comments. There are several ways of getting in contact with the SlimDX team listed on the <a href="support.php">Support page</a>. 
				</p>
			</div>
			
			<div class="paragraph">
				<h2>End User Runtime</h2>
				<p>This is the package for end users. It works on both 32 and 64 bit systems, for all versions of Windows supported by the .NET Framework. If you're just trying to run a SlimDX program that someone else gave you, this is the installer you want to download. Developers should point users to this link, or include it with their own installers. See the documentation for more information.</p><br />
				<a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20%28November%202008%29.msi"><img src="images/latest.jpg" alt="Latest User Runtime" /></a>
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
				<a href="http://slimdx.googlecode.com/files/SlimDX%20SDK%20%28November%202008%29.exe"><img src="images/latest.jpg" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#November_2008" rel="nofollow">What&#x27;s Changed in the November 2008 SDK</a> -- Information on the work we did for the November 2008 SDK release.</li>
				<li><a href="http://slimdx.org/docs/#Debugging_Tips" rel="nofollow">Debugging Tips</a> -- Tips on how to work on and debug applications using SlimDX. It&#x27;s a really good idea to be familiar with the contents of this page, and check on it every so often, because it is updated periodically.</li>
				</ul>
			</div>
			
			<div class="paragraph">
				<h2>Remarks</h2>
				<p>The November SDK does not add much to the SlimDX core library in terms of new features. There are some bug fixes, all of which are listed in the Release Notes, as well as in the SDK documentation. The focus of this release was on making development and deployment of SlimDX applications much smoother, since this is an area where we've been getting a lot of feedback for improvements.<br /><br />
				Amongst those features is a proper end user runtime, which quickly and quietly installs everything needed on a system to run SlimDX programs. This installer supports silent installation and does not create a Program Files entry. It is unified for 32 bit and 64 bit systems, and the SDK includes the full WiX script used to create it so that developers may customize it or use it as a model.<br /><br />
				We have also evolved our documentation beyond a simple class reference, and it now includes a number of topics discussing development. There are relatively few of these articles right now, but in the future this part of the documentation will continue to expand.</p>
			</div>
		</div>
	
		<div id="right">
			<ul class="sidemenu">
				<li><a href="http://slimdx.org/docs/#November_2008">Release Notes</a></li>
				<li><a href="releases.php">Previous Releases</a></li>
				<li><a href="license.php">Licensing Information</a></li>
				<li><a href="http://slimdx.org/docs">Documentation</a></li>
			</ul>
		</div>
		
		<div id="clear"></div>
    </div>
</div>

<?php	
	require_once("footer.php");
?>

