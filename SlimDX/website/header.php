<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" 
"http://www.w3.org/TR/html4/strict.dtd">
<html>

<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<link rel="stylesheet" href="/css/reset.css" type="text/css">
	<link rel="stylesheet" href="/css/default.css" type="text/css">
	<link rel="stylesheet" href="/css/docs.css" type="text/css">
<?php
	if(empty($g_pageTitle))
		$g_pageTitle = "SlimDX";
		
	echo "<title>$g_pageTitle</title>"
?>

</head>
<body>

<div id="top1">
	<div class="wrapper">
    	<div id="logo">
        <a href="http://slimdx.org/"><img src="/images/logoblackbg.jpg" alt="SlimDX" /></a>
        </div>
        <div style="padding-left:415px; height:60px;"></div>
        
        <ul class="menu">
<?php
	function WriteMenuItem($itemName, $url)
	{
		global $g_pageName;
		
		if($g_pageName == $itemName)
			echo '<li class="current">';
		else
			echo '<li>';
			
		echo "<a href=\"$url\">$itemName</a></li>\n";
	}
	
	WriteMenuItem("Home", "/index.php");
	WriteMenuItem("Features", "/features.php");
	WriteMenuItem("Download", "/download.php");
	WriteMenuItem("Documentation", "/docs/");
	WriteMenuItem("Tutorials", "/tutorials.php");
	WriteMenuItem("Licensing", "/license.php");
	WriteMenuItem("Consulting", "/consulting.php");
?>            
        </ul>
    </div>
</div>