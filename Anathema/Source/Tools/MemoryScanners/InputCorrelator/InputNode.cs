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
    class InputNode : TreeNode
    {
        public enum NodeTypeEnum
        {
            Input,

            AND,
            OR,
            NOT
        }

        private InputNode ParentNode;
        private List<InputNode> ChildrenNodes;
        private NodeTypeEnum NodeType;
        private Keys Key;

        public InputNode(NodeTypeEnum NodeType)
        {
            ParentNode = null;
            this.NodeType = NodeType;
            this.Text = this.ToString();
            ChildrenNodes = new List<InputNode>();
        }

        public InputNode(Keys Key)
        {
            ParentNode = null;
            this.NodeType = NodeTypeEnum.Input;
            this.Key = Key;
            this.Text = this.ToString();
            ChildrenNodes = new List<InputNode>();
        }

        public void AddChild(InputNode Node)
        {
            Node.ParentNode = this;

            ChildrenNodes.Add(Node);
            this.Nodes.Add(Node);
        }

        public void RemoveChild(InputNode Node)
        {
            ChildrenNodes.Remove(Node);
            this.Nodes.Remove(Node);
        }

        public void DeleteNode()
        {
            ParentNode.RemoveChild(this);
        }

        public InputNode GetChildAtIndex(Int32 Index)
        {
            return ChildrenNodes[Index];
        }

        public Boolean InputConditionValid()
        {
            return false;
        }

        public override String ToString()
        {
            switch (NodeType)
            {
                case NodeTypeEnum.AND:
                    return "AND";
                case NodeTypeEnum.OR:
                    return "OR";
                case NodeTypeEnum.NOT:
                    return "NOT";
                case NodeTypeEnum.Input:
                    return Key.ToString();
            }
            return String.Empty;
        }

    } // End class

} // End namespace