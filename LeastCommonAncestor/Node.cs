using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeastCommonAncestor
{
    class Node {
 
        public Node parent;
        public Node left;
        public Node right;
        public int Data;

        public Node(int data)
        {
            Data = data;
        }
 
        public Node(Node parent, Node left, Node right, int data) {
            this.parent = parent;
            this.left = left;
            this.right = right;
            this.Data = data;
        }
 
        public bool isRoot() {
            return parent == this;
        }
    }
}
