using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeastCommonAncestor
{
    class Program
    {
        static void Main(string[] args)
        {
            Node root = new Node(4);
            root.parent = root;

            root.left = new Node(3);
            root.left.parent = root;

            root.left.left = new Node(2);
            root.left.right = new Node(1);
            root.left.left.parent = root.left;
            root.left.right.parent = root.left;

            root.left.right.right = new Node(5);
            root.left.right.right.parent = root.left.right;

            // Console.WriteLine("LCA of 2 and 5 is {0}", FindLCA1(root.left.left, root.left.right.right).Data);
            // Console.WriteLine("LCA of 2 and 5 is {0}", FindLCA2(root.left.left, root.left.right.right).Data);
            Console.WriteLine("LCA of 2 and 5 is {0}", FindLCA3(root.left.left, root.left.right.right).Data);
        }

        // Approach 1 using IsParent
        static Node FindLCA1(Node a, Node b)
        {
            HashSet<Node> hashSetNodes = new HashSet<Node>();

            Node commonParent = null;

            while (hashSetNodes.Add(a))
            {
                a = a.parent;
            }

            while (b.isRoot() == false)
            {
                if (hashSetNodes.Contains(b))
                {
                    commonParent = b;
                    break;
                }

                b = b.parent;
            }

            if (commonParent == null && b.isRoot())
            {
                return b;
            }

            return commonParent; 
        }


        // Approach 2 using IsParent
        static Node FindLCA2(Node a, Node b)
        {
            if (a == null || b == null)
            {
                return null;
            }

            int d1 = GetDepth(a);

            int d2 = GetDepth(b);

            // Make sure d1 is the smaller one
            // so we do not need to write additional rules to 
            // determine which node is deeper. Our rule can be h2 should
            // always be the deeper one. 
            if (d1 > d2)
            {
                Swap(d1, d2);
                Swap(a, b);
            }

            while (d2 > d1)
            {
                b = b.parent;
                d2--;
            }

            while (!a.isRoot() && !b.isRoot())
            {
                if (a == b)
                {
                    return a;
                }

                a = a.parent;
                b = b.parent;
            }

            // If you are out of the loop, either a or b are the root
            // so check and return
            if (a.isRoot())
            {
                return a;
            }

            return b;
        }

        // Given a leaf node, determine depth of that node
        static int GetDepth(Node n)
        {
            int depth = 0;

            while (!n.isRoot())
            {
                depth++;
                n = n.parent;
            }

            return depth;
        }

        static void Swap(int a, int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        static void Swap(Node a, Node b)
        {
            Node temp = a;
            a = b;
            b = temp;
        }

        // Approach 3 (in case IsParent is not available)
        static Node FindLCA3(Node a, Node b)
        {
            Node root = FindRoot(a);

            return commonAncestorHelper(root, a, b);
        }

        static Node commonAncestorHelper(Node root, Node node1, Node node2)
        {
            if (root == null)
            {
                return null;
            }

            if (root == node1 || root == node2)
            {
                return root;
            }

            Node leftResult = commonAncestorHelper(root.left, node1, node2);
            Node rightResult = commonAncestorHelper(root.right, node1, node2);

            if (leftResult != null && rightResult != null)
            {
                return root;
            }
            else if (leftResult != null)
            {
                return root.left;
            }
            else if (rightResult != null)
            {
                return root.right;
            }

            return null;
        }

        static Node FindRoot(Node node)
        {
            if (node == null)
            {
                return null;
            }

            while (!node.isRoot())
            {
                node = node.parent;
            }

            return node;
        }
    }
}
