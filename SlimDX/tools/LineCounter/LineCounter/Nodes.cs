using System.Collections.Generic;

namespace LineCounter
{
    class Node
    {
        public string Name;
        public List<Node> ChildNodes = new List<Node>();

        public override string ToString()
        {
            return Name;
        }
    }

    class FileNode : Node
    {
        public string Path;
        public int TotalLines;
        public int CodeLines;
        public int Comments;
        public int BlankLines;
        public bool Valid;
    }
}
