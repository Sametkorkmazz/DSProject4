using System;

namespace DSProject4
{
    class TreeNode
    {
        public int key;
        public int height;
        public TreeNode leftchild, rightchild;

        public TreeNode(int key)
        {
            this.key = key;
            height = 1;
        }
    }

    class AVLTree
    {
        public TreeNode root;

        public AVLTree()
        {
            root = null;
        }

        public int getHeight(TreeNode node)
        {
            if (node == null) return 0;
            return node.height;
        }

        public TreeNode rightRotate(TreeNode node)
        {
            TreeNode x = node.leftchild;
            TreeNode t2 = x.rightchild;
            x.rightchild = node;
            node.leftchild = t2;
            node.height = Math.Max(getHeight(node.leftchild), getHeight(node.rightchild)) + 1;
            x.height = Math.Max(getHeight(x.leftchild), getHeight(x.rightchild)) + 1;
            return x;
        }

        public TreeNode leftRotate(TreeNode node)
        {
            TreeNode x = node.rightchild;
            TreeNode t2 = x.leftchild;
            x.leftchild = node;
            node.rightchild = t2;
            node.height = Math.Max(getHeight(node.leftchild), getHeight(node.rightchild)) + 1;
            x.height = Math.Max(getHeight(x.leftchild), getHeight(x.rightchild)) + 1;
            return x;
        }

        public int getBalance(TreeNode N)
        {
            if (N == null) return 0;
            return getHeight(N.leftchild) - getHeight(N.rightchild);
        }

        public TreeNode insert(TreeNode node, int key)
        {
            if (node == null)
            {
                return (new TreeNode(key));
            }

            if (key < node.key)
                node.leftchild = insert(node.leftchild, key);
            else if (key > node.key)
                node.rightchild = insert(node.rightchild, key);

            node.height = 1 + Math.Max(getHeight(node.leftchild), getHeight(node.rightchild));
            int balance = getBalance(node);

            if (balance > 1 && key < node.leftchild.key)
                return rightRotate(node);
            if (balance < -1 && key > node.rightchild.key)
                return leftRotate(node);
            if (balance > 1 && key > node.leftchild.key)
            {
                node.leftchild = leftRotate(node.leftchild);
                return rightRotate(node);
            }

            if (balance < -1 && key < node.rightchild.key)
            {
                node.rightchild = rightRotate(node.rightchild);
                return leftRotate(node);
            }

            return node;
        }

        public void preOrder(TreeNode node)
        {
            if (node != null)
            {
                Console.Write(node.key + " ");
                preOrder(node.leftchild);
                preOrder(node.rightchild);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AVLTree tree = new AVLTree();
            tree.root = tree.insert(tree.root, 10);
            tree.root = tree.insert(tree.root, 20);
            tree.root = tree.insert(tree.root, 30);
            tree.root = tree.insert(tree.root, 40);
            tree.root = tree.insert(tree.root, 50);
            tree.root = tree.insert(tree.root, 60);
            tree.root = tree.insert(tree.root, 70);
            tree.root = tree.insert(tree.root, 80);
            Console.WriteLine("AVL Tree Preorder Dolaşımı. Sayılar küçükten büyüğe eklenmiştir.");
            tree.preOrder(tree.root);
            Console.WriteLine();
        }
    }
}