SlimDX/trunk/tools

This directory is for internally developed SlimDX build tools. The build process
for SlimDX will depend on the existence of the tool binaries, so you should 
make sure that you configure new tool projects in this directory as follows:

* Use only one project/solution configuration ('Primary' is the recommended 
  name for this configuration).
  
* Use a post-build step to copy the binaries for the tool to the tool's
  solution directory, e.g:
    
    xcopy "$(TargetPath)" "$(SolutionDir)" /Y
  
  This will ensure that the binaries are in a stable location.
  
* Check the binaries into the repository. This ensures the binaries are always
  available, even when SlimDX is built in a 2005 environment, and saves us the
  trouble of having to maintain a 2005 and 2008 solution for each tool (the
  automatic migration to 2005 versions of the rest of SlimDX is handled by a
  tool in this directory, so we couldn't leverage that here).
  
See BuildTasks for an example.
