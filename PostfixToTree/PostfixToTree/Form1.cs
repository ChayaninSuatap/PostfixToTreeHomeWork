using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostfixToTree
{
    public partial class Form1 : Form
    {
        ImageList imageList;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.fire);
            treeView1.ImageList = imageList;
        }

        private void butEval_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var input = textBox1.Text;
            var stack = new List<Node>();
            foreach (var x in input)
            {
                var numRange = "0123456789";
                if (numRange.IndexOf(x) != -1)
                {
                    //num case
                    int num = Convert.ToInt32(x) - 48;
                    var node = new Node(NodeType.NUM);
                    node.Value = num;
                    stack.Add(node);
                }
                else
                {
                    //op case
                    var nodeb = stack.Pop();
                    var nodea = stack.Pop();
                    var node = new Node(NodeType.OP);
                    node.SetOpType(x + "");
                    node.NodeA = nodea;
                    node.NodeB = nodeb;
                    stack.Add(node);
                }
            }
            RenderTreeView(stack[0],treeView1.Nodes);
        }

        void RenderTreeView(Node dataNode, TreeNodeCollection uiNode)
        {
            if (dataNode.Type == NodeType.OP)
            {  
                uiNode.Add(dataNode.GetOpTypeString());
                RenderTreeView(dataNode.NodeA, uiNode[uiNode.Count - 1].Nodes);
                RenderTreeView(dataNode.NodeB, uiNode[uiNode.Count - 1].Nodes);
            }
            else
            {
                uiNode.Add(dataNode.Value + "");
                uiNode[uiNode.Count - 1].ImageIndex = 1;
                uiNode[uiNode.Count - 1].SelectedImageIndex = 1;
            }
            uiNode[uiNode.Count - 1].Tag = dataNode.PrintNode(true);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            label1.Text = treeView1.SelectedNode.Tag.ToString();
            
        }
    }
    static class Extension
    {
        public static Node Pop(this List<Node> list)
        {
            var t = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return t;
        }
    }
}
