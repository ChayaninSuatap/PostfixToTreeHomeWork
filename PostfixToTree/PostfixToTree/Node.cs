using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostfixToTree
{
    class Node
    {
        public NodeType Type = NodeType.NUM;
        public Node NodeA, NodeB;
        public OpType Op = OpType.ADD;
        public int Value = 0;

        public Node(NodeType type)
        {
            Type = type;
        }

        public void SetOpType(string op)
        {
            switch (op)
            {
                case "+": Op = OpType.ADD; break;
                case "-": Op = OpType.SUB; break;
                case "*": Op = OpType.MUL; break;
                case "/": Op = OpType.DIV; break;
            }
        }

        public string PrintNode(bool innerNode = false)
        {
            if (Type == NodeType.OP)
            {
                string opString = "";
                switch (Op)
                {
                    case OpType.ADD: opString = "+"; break;
                    case OpType.SUB: opString = "-"; break;
                    case OpType.MUL: opString = "*"; break;
                    case OpType.DIV: opString = "/"; break;
                }
                if (innerNode == false)
                    return "(" + NodeA.PrintNode() + opString + NodeB.PrintNode() + ")";
                else
                    return NodeA.PrintNode() + opString + NodeB.PrintNode() + " = " + GetNodeValue();
            }
            else
                return Value + "";
        }

        public int GetNodeValue()
        {
            if (Type == NodeType.NUM) return Value;
            else
                switch (Op)
                {
                    case OpType.ADD: return NodeA.GetNodeValue() + NodeB.GetNodeValue();
                    case OpType.SUB: return NodeA.GetNodeValue() - NodeB.GetNodeValue();
                    case OpType.MUL: return NodeA.GetNodeValue() * NodeB.GetNodeValue();
                    case OpType.DIV: return NodeA.GetNodeValue() / NodeB.GetNodeValue();
                    default: return 0;
                }
        }

        public string GetOpTypeString()
        {
            switch (Op)
            {
                case OpType.ADD: return "+";
                case OpType.SUB: return "-";
                case OpType.MUL: return "*";
                case OpType.DIV: return "/";
                default: return "error";
            }
        }
    }

    public enum NodeType { OP, NUM }
    public enum OpType { ADD, SUB, MUL, DIV }
}
