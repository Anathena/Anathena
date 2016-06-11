<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<link rel="stylesheet" href="../css/reset.css" type="text/css" />
	<link rel="stylesheet" href="../css/default.css" type="text/css" />
	<link href="prettify/prettify.css" type="text/css" rel="stylesheet" />
	<script type="text/javascript" src="prettify/prettify.js"></script>
	<script src="prettify/lang-hlsl.js" type="text/javascript"></script>

<?php
	if(empty($g_pageTitle))
		$g_pageTitle = "SlimDX";
		
	echo "<title>$g_pageTitle</title>"
?>

</head>
<body onload="prettyPrint()">

<div id="top1">
	<div class="wrapper">
    	<div id="logo">
        <a href="http://slimdx.org/"><img src="../images/logoblackbg.jpg" alt="SlimDX" /></a>
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
	
	WriteMenuItem("Home", "../index.php");
	WriteMenuItem("Features", "../features.php");
	WriteMenuItem("Download", "../download.php");
	WriteMenuItem("Documentation", "/docs");
	WriteMenuItem("Tutorials", "../tutorials.php");
	WriteMenuItem("Licensing", "../license.php");
	WriteMenuItem("Support", "../support.php");
?>            
        </ul>
    </div>
</div>