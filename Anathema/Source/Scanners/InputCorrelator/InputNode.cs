using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anathema.Source.Scanners.InputCorrelator
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

        public Boolean EvaluateCondition(Dictionary<Keys, DateTime> KeyEvents, DateTime QueryTime, Int32 TimeOutInterval)
        {
            Boolean Result = false;

            switch (NodeType)
            {
                case NodeTypeEnum.Input:
                    if (KeyEvents.ContainsKey(Key) && Math.Abs((QueryTime - KeyEvents[Key]).TotalMilliseconds) < TimeOutInterval)
                        Result = true;
                    break;
                case NodeTypeEnum.AND:
                    Result = true;
                    foreach (InputNode Node in this)
                        Result &= Node.EvaluateCondition(KeyEvents, QueryTime, TimeOutInterval);
                    break;
                case NodeTypeEnum.OR:
                    foreach (InputNode Node in this)
                        Result |= Node.EvaluateCondition(KeyEvents, QueryTime, TimeOutInterval);
                    break;
                case NodeTypeEnum.NOT:
                    foreach (InputNode Node in this)
                        Result = !Node.EvaluateCondition(KeyEvents, QueryTime, TimeOutInterval);
                    break;

            }

            return Result;
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
                    Result += "(";
                    foreach (InputNode Child in this)
                    {
                        Result += Child.EvaluateText();
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += " and ";
                    }
                    Result += ")";
                    break;
                case NodeTypeEnum.OR:
                    if (ChildrenNodes.Count == 0)
                    {
                        Result += this.ToString();
                        break;
                    }
                    Result += "(";
                    foreach (InputNode Child in this)
                    {
                        Result += Child.EvaluateText();
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += " or ";
                    }
                    Result += ")";
                    break;
                case NodeTypeEnum.NOT:
                    if (ChildrenNodes.Count == 0)
                    {
                        Result += this.ToString();
                        break;
                    }
                    Result += "(";
                    foreach (InputNode Child in this)
                    {
                        if (ChildrenNodes.Count == 1 || Child != ChildrenNodes.Last())
                            Result += "not ";
                        Result += Child.EvaluateText();
                    }
                    Result += ")";
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

        public Boolean CanAddChild(InputNode Node)
        {
            if (this.NodeType == NodeTypeEnum.Input && Node.NodeType == NodeTypeEnum.Input)
                return false;
            return true;
        }

        public InputNode AddChild(InputNode Node, Int32? InsertLocation = null)
        {
            if (InsertLocation == null)
                InsertLocation = this.Nodes.Count;

            // Adding AND/OR operations feels counter-intuitive without some parent swapping magic:
            if (Node.ChildrenNodes.Count == 0 && (Node.NodeType == NodeTypeEnum.AND || Node.NodeType == NodeTypeEnum.OR || Node.NodeType == NodeTypeEnum.NOT))
            {
                InputNode CurrentParent = this.ParentNode;
                Int32 InsertIndex = CurrentParent == null ? 0 : CurrentParent.Nodes.IndexOf(this);
                if (CurrentParent != null)
                    CurrentParent.RemoveChild(this);
                Node.AddChild(this);
                if (CurrentParent != null)
                    CurrentParent.AddChild(Node, InsertIndex);
            }
            else
            {
                Node.ParentNode = this;
                ChildrenNodes.Insert(InsertLocation.Value, Node);
                this.Nodes.Insert(InsertLocation.Value, Node);
            }

            InputNode CurrentNode = this;
            while (CurrentNode != null && CurrentNode.ParentNode != null)
                CurrentNode = CurrentNode.ParentNode;
            return CurrentNode;
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
            return Index < ChildrenNodes.Count ? ChildrenNodes[Index] : null;
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