<?php
header('Content-type: application/json');


$dbconn = mysql_connect("localhost", "web", "bl4ckj4ck"); 
mysql_select_db("docs");

$query = "";

if(isset($_GET["url"]) and strlen($_GET["url"]) > 0)
{
	$url = mysql_real_escape_string($_GET["url"]);
	$query = "SELECT * FROM TableOfContents WHERE Url = '$url'";
}
else
{
	die();
}

$results = mysql_query($query);

$topics = array();
while($row = mysql_fetch_assoc($results))
{
	$topicEntry = $row;
	$topics[] = $topicEntry;
}
echo json_encode($topics);

mysql_close($dbconn);
?>

