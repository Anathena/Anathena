<?php
	$g_pageTitle = "SlimDX Tutorials";
	$g_pageName = "Tutorials";
	
	require_once("header.php");
?>

<div id="content">
  <div class="wrapper">

    <div id="left">
      <div class="paragraph">
        <h1>Direct3D 11 Tutorials</h1>
        <p>
          A series of tutorials demonstrating the initialization and basic use of Direct3D 11.
          <br/><br/>
        </p>

        <a href="tutorials/BasicWindow.php">
          <h2>Basic Window</h2>
        </a>
        <p>
          <a href="tutorials/BasicWindow.php">
            <img src="images/basicwindow.png"/>
          </a>
          <br/>
          <br/>

          This tutorial is the start of the series explaining and demonstrating the use of Direct3D 11 via SlimDX. Readers are expected to know how to use their chosen .NET language and IDE before attempting to understand these tutorials, as language concepts will not be explained.
          The first tutorial will deal with getting a SlimDX project set up and ready to go, and will then explain the process of initializing a window and launching a rendering loop.

          <br/>
          <br/>
        </p>

        <a href="tutorials/DeviceCreation.php">
          <h2>Device Creation</h2>
        </a>
        <p>
          <a href="tutorials/DeviceCreation.php">
            <img src="images/devicecreation.png"/>
          </a>
          <br/>
          <br/>

          In this second tutorial, we give an overview of the Direct3D 11 graphics device and show how to initialize it properly for rendering.
          The Device object is the primary means of communicating with a virtual graphics adapter installed on the system, and will be used for
          all resource creation and the final rendering of 3D primitives. The sample will conclude by showing how to use the device to clear the background to a solid color.

          <br/>
          <br/>
        </p>

        <a href="tutorials/SimpleTriangle.php">
          <h2>Simple Triangle</h2>
        </a>
        <p>
          <a href="tutorials/SimpleTriangle.php">
            <img src="images/simpletriangle.png"/>
          </a>
          <br/>
          <br/>

          Rendering primitives is the raison d’être of a 3D graphics API. This tutorial explains the process of supplying Direct3D with
          primitive data and ensuring that the entire graphics pipeline is properly configured so as to produce visual evidence of the primitives
          on the screen. By the end a single rendered triangle will be visible on screen.

          <br/>
          <br/>
        </p>
      </div>
    </div>

    <div id="right">
      <h3>Direct3D 11</h3>
      <ul class="sidemenu">
        <li><a href="tutorials/BasicWindow.php">Basic Window</a></li>
        <li><a href="tutorials/DeviceCreation.php">Device Creation</a></li>
        <li><a href="tutorials/SimpleTriangle.php">Simple Triangle</a></li>
      </ul>
    </div>

    <div id="clear"></div>
  </div>
</div>

<?php	
	require_once("footer.php");
?>

