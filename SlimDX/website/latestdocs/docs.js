var shouldSearch = true;
var syncToc = false;

//Returns the location currently displayed by the iframe
function getCurrentLocation()
{
	//get the current doc in the iframe -- src attribute is >>> WRONG <<<
	var frame = document.getElementById("content_frame").contentWindow;
	return frame.location;
}

//Get the current topic, by stripping down the location string to just the topic portion
function getCurrentTopic()
{
	var loc = getCurrentLocation();
	var topic = loc.pathname.substring(window.location.pathname.length + 5);
	topic = topic.substring(0, topic.lastIndexOf("."));
	return topic;
}

//Code wants to navigate the url, currently this is always the tree view
function navigateToUrl(url)
{
	//we should not trigger a search, since the user got here via the tree
	shouldSearch = false;
	navigateFrame(url);
}

//Navigate the frame directly, without touching other state (triggers iframeLoaded)
function navigateFrame(url)
{
	if(url.charAt(0) == "#")
		url = url.substring(1);

	var target = "html/" + url + ".htm";
	document.getElementById("content_frame").src = target;
}

//Respond to a change in the hash by navigating if needed, only user can CHANGE the topic this way
function locationHashChanged() {
	var topic = window.location.hash.substring(1);
	var current = getCurrentTopic();
	if(current != topic)
	{
		//new topic means triggered by user
		navigateFrame(topic);
		$("#tree").jstree("search", window.location.hash);
	}
}

//scroll the tree to a target node (after a search for that node triggered by iframe navigate)
function scrollTree() {
	//check if there's a scroll point set
	var scrollto = $("#sidebar").attr("scrollto");
	if(scrollto == null || scrollto.length == 0)
		return;
	
	//find the ndoe if it exists
	var node = document.getElementById("node_" + scrollto);
	if(node == null)
		return;

	//select and open the node, since its children are now relevant
	$("#tree").jstree("open_node", node);
	$("#tree").jstree("select_node", node, true);
	
	//okay we have a target, scroll to it
	var offset = node.offsetTop;
	if(offset == 0)
	{
		//node has not been positioned, try and use its parents instead
		path = $("#tree").jstree("get_path", node, true);
		path.pop();
		while(path.length > 0 && offset == 0)
		{
			var parentId = path.pop();
			offset = document.getElementById(parentId).offsetTop;
		}
	}

	document.getElementById("sidebar").scrollTop = offset;
	//clear the scroll point ttribute
	$("#sidebar").attr("scrollto", "");
}

//Respond to the iframe being loaded -- if new page, change location hash and search tree
function iframeLoaded() {
	var loc = getCurrentLocation(); 
	//ignore the initial blank load
	if(loc.host != document.location.host)
		return;

	//strip off the html/ and path name from the location, then add the hash
	var target = "#" + getCurrentTopic();
	//if this is true, a link inside the iframe was clicked and the hash is updated to match
	if(target != window.location.hash)
	{
		window.location.hash = target;
		if(shouldSearch)
			$("#tree").jstree("search", target);
		shouldSearch = true;
	}
}

function iframeFirstLoad()
{
	//nothing happens on the first real page load, subsequent loads go through the update logic above
	var content_frame = document.getElementById("content_frame");
	if(content_frame.src != null && content_frame.src.length > 0)
		content_frame.onload = iframeLoaded;
}

function configureTreeView()
{
	$("#tree").jstree({
		"core" : { "animation": 00 },
		"plugins" : [ "themes", "json_data", "ui", "search" ],
		"themes" : { "theme" : "classic", "icons" : false },
		"ui" : {
			"select_limit": 1,
			"selected_parent_close" : "select_parent",
			"initially_select": [ "node_0" ]
		},
		"json_data" : {
			"ajax" : {
				"url" : "topic.php",
				"data" : function (n) {
					var queryString = {};
					if(n.attr)
						queryString.id = n.attr("nodeId");

					return queryString;
				},
				"success" : function (data) {
					//patch the data up to match what jsTree expects
					for(i = 0; i < data.length; ++i)
					{
						data[i].data = data[i].Title;
						data[i].attr = {
							"id": "node_" + data[i].Id, 
							"nodeId" : data[i].Id,
							"topic": data[i].Url
						};
						if(data[i].TopicId != null)
							data[i].state = "closed";
					}

					//if nothing is in the iframe, use this to fill it
					var loc = getCurrentLocation(); 
					if(loc.host != document.location.host && window.location.hash.length < 2)
						navigateFrame(data[0].attr.topic);
					
					return data;
				}
			}
		},
		"search" : {
			"ajax" : {
				"url": "search.php",
				"data": function(n) {
					var queryString = { "url": n.substring(1) };
					return queryString;
				},
				"success": function(data) {
					if(data.length == 0)
						return null;

					//build an id list from the path sent back
					var path = data[0].ParentPath;
					var nodes = [];
					if(path != null)
					{
						nodes = path.split("/");
						nodes.shift();
						for(i = 0; i < nodes.length; ++i)
						{
							nodes[i] = "#node_" + nodes[i];
						}
					}

					//mark the node as the scroll target
					$("#sidebar").attr("scrollto", data[0].Id);
					//attempt to scroll right now, in case we already have the node loaded
					scrollTree();

					return nodes;
				}
			}
		}
	})
	.bind("load_node.jstree", function(e, data) {
		if(data[0] == -1)
			return;
	
		if(syncToc)
		{
			$("#tree").jstree("search", syncToc);
			syncToc = false;
		}

		//if this load was triggered by a search, we'll want to scroll
		scrollTree();
	})
	.bind("click.jstree", function(e) {
		if(e.target.nodeName.toLowerCase() != "a")
			return;

		var node = e.target.parentNode;
		var topic = $(node).attr("topic");
		if(topic != null)
		{
			navigateToUrl(topic);
			$("#tree").jstree("open_node", node);
		}
	});
}