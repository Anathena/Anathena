<?php
	$g_pageTitle = "SlimDX Download Page (March 2009)";
	$g_pageName = "Download";
	
	require_once("header.php");
?>

<div id="content">
	<div class="wrapper">
		<div id="left">
			<h1>March 2009 SP1</h1>
			<div class="paragraph">
				<h2>Introduction</h2>
				<p>
				The March 2009 SP1 release of SlimDX is now out!
				<b>Due to corrupted 64 bit binaries in the initial release, we are releasing a patched version of the March release to fix these problems.
				If you are on a 64 bit system, please <i>uninstall</i> SlimDX March and then install this version instead.</b>
				</p><br />
				<p>
				This is a major release in terms of new functionality and improvements behind the scenes.
				Many missing corners were filled in, and several usability enhancements were made across the library on top of the standard array of bug fixes.
				A new DLL containing beta-level technologies, such as Direct2D and Direct3D11, was added to showcase developing APIs.
				These APIs will be moved into the core SlimDX DLL once they are given a full release by Microsoft.
				Until then, they should be viewed as previews, and should not be used for products that expect stable behavior. 
				</p><br />
				<p>
				Performance is another issue focused on in the March release.
				After extensive testing, several improvements were made to the error handling infrastructure, which showed significant performance increases across the entire library.
				On top of this, performance issues in the math library were discovered and subsequently eliminated.
				This involved modifying the types to use fields instead of properties, as value type getters are not inlined in .NET prior to version 3.5.
				These small changes could have a substantial impact on your application simply by recompiling with the new DLL. 
				</p>
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
				<a href="http://slimdx.googlecode.com/files/SlimDX%20Runtime%20%28March%202009%20SP1%29.msi"><img src="images/latest.jpg" alt="Latest User Runtime" /></a>
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
				<a href="http://slimdx.googlecode.com/files/SlimDX%20SDK%20%28March%202009%20SP1%29.exe"><img src="images/latest.jpg" alt="Latest Developer SDK" /></a>
			</div>
			
			<div class="paragraph">
				<h2>Information For Developers</h2>
				<p>If you&#x27;re new to this whole thing, read the <a href="http://slimdx.org/docs/#Getting_Started" rel="nofollow">Getting Started</a> documentation page.
				It should help get you moving.
				Other pages of interest include:</p>
				<ul>
				<li><a href="http://slimdx.org/docs/#March_2009" rel="nofollow">What&#x27;s Changed in the March 2009 SDK</a> -- Information on the work we did for the March 2009 SDK release.</li>
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
				This release of SlimDX includes technology previews of Microsoft's beta Windows 7 technologies.
				They are strictly <b>experimental</b> and not guaranteed to work well or at all.
				The previews are contained in a new temporary library file called SlimDX.Windows7.dll.
				The new library is not digitally signed, and will not be installed to the Global Assembly Cache.
				Both the core library and the new library seem to work fine, but we make no promises as long as everything is still beta.
				You will need the full DirectX March 2009 SDK installed to run any Windows7 code, including the samples.
				</p><br />
			</div>
		</div>
	
		<div id="right">
			<ul class="sidemenu">
				<li><a href="http://slimdx.org/docs/#March_2009">Release Notes</a></li>
				<li><a href="http://slimdx.org/docs">Documentation</a></li>
				<li><a href="license.php">Licensing Information</a></li>
				<li><a href="releases.php">Previous Releases</a></li>
				<li><a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=24a541d6-0486-4453-8641-1eee9e21b282&displaylang=en">DirectX SDK (March 2009)</a></li>
			</ul>
		</div>
		
		<div id="clear"></div>
    </div>
</div>

<?php	
	require_once("footer.php");
?>

