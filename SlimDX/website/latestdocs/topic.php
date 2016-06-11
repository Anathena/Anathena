<?php
header('Content-type: application/json');

$dbconn = mysql_connect("localhost", "web", "bl4ckj4ck"); 
mysql_select_db("docs");

$query = "";
$id = null;

if(isset($_GET["id"]) and is_numeric($_GET["id"]))
{
	$id = (int) $_GET["id"];
	$query = "SELECT * FROM TableOfContents WHERE ParentId = $id";
}
else
{
	$query = "SELECT * FROM TableOfContents WHERE ParentId IS NULL";
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

