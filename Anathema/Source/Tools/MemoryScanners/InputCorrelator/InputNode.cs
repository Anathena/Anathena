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
    class InputNode : TreeNode, IEnumerable
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
            ChildrenNodes = new List<InputNode>();
        }

        public InputNode(Keys Key)
        {
            ParentNode = null;
            this.NodeType = NodeTypeEnum.Input;
            this.Key = Key;
            ChildrenNodes = new List<InputNode>();
        }

        public String EvaluateText()
        {
            String Result = String.Empty;
            
            switch (NodeType)
            {
                case NodeTypeEnum.AND:
                    if (ChildrenNodes.Count == 0)
                    {
                        Result += this.ToString();
                        break;
                    }
                    foreach (InputNode Child in this)
                    {
                        Result += Child.EvaluateText();
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += " and ";
                    }
                    break;
                case NodeTypeEnum.OR:
                    if (ChildrenNodes.Count == 0)
                    {
                        Result += this.ToString();
                        break;
                    }
                    foreach (InputNode Child in this)
                    {
                        Result += Child.EvaluateText();
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += " or ";
                    }
                    break;
                case NodeTypeEnum.NOT:
                    if (ChildrenNodes.Count == 0)
                    {
                        Result += this.ToString();
                        break;
                    }
                    foreach (InputNode Child in this)
                    {
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += "not ";
                        Result += Child.EvaluateText();
                    }
                    break;
                case NodeTypeEnum.Input:
                    Result += this.ToString();

                    foreach (InputNode Child in this)
                        Result += Child.EvaluateText();

                    break;
            }

            this.Text = Result;
            return Result;
        }

        public void AddChild(InputNode Node)
        {
            if (this.NodeType == NodeTypeEnum.Input && Node.NodeType == NodeTypeEnum.Input)
                return;

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

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)ChildrenNodes).GetEnumerator();
        }

    } // End class

} // End namespace