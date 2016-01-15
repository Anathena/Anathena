using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Gma.System.MouseKeyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class InputNode
    {
        public enum NodeTypeEnum
        {
            AND,
            OR,
            NOT,
            Key
        }

        private List<InputNode> Children;
        private NodeTypeEnum NodeType;
        private Keys Key;

        public InputNode(NodeTypeEnum NodeType)
        {
            this.NodeType = NodeType;
            Children = new List<InputNode>();
        }

        public Boolean InputConditionValid()
        {
            return false;
        }

    } // End class
    
} // End namespace