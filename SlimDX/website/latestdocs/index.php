<?php
	$g_pageTitle = "SlimDX Documentation";
	$g_pageName = "Documentation";
	
	require_once("../header.php");
?>

<!--
Copyright (c) 2007-2011 SlimDX Group
 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
-->

<script type="text/javascript" src="lib/jquery.js"></script>
<script type="text/javascript" src="jquery.jstree.js"></script>

<script type="text/javascript" src="docs.js"></script>

<div id="page_container">
	<div id="sidebar">
		<div id="tree"></div>
	</div>

	<div id="content_container">
		<div id="frame_container">
			<iframe id="content_frame" frameborder="0" onload="iframeFirstLoad();"></iframe>
		</div>
	</div>
</div>

<script type="text/javascript">
$(function () {
	//debugger;
	
	syncToc = false;
	//if a target was provided as part of the link, navigate to it
	if(window.location.hash.length > 1)
	{
		syncToc = window.location.hash;
		navigateFrame(window.location.hash);
	}
	
	//hook up hash change detection
	window.onhashchange = locationHashChanged;
	
	configureTreeView();
});
</script>

<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("UA-2872756-3");
pageTracker._trackPageview();
} catch(err) {}</script>

</body>
</html>

