using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByneriTry
{
    internal class TreeNode
    {
        public List<string>? defense {  get; set; }
        public (int,int) Range { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

    }
    internal class BSTree
    {
        private TreeNode? root;

        public bool Search(int value)
        {
            // Use a helper method for the recursive implementation
            return SearchRecursive(root, value);
        }

        private bool SearchRecursive(TreeNode? node, int value)
        {
            // Base case: if the node is null, the value is not in the tree
            if (node == null)
            {
                return false;
            }

            // If the value is found, return true
            if (value == node.Value)
            {
                return true;
            }

            // If the value is less than the current node, search in the left subtree
            if (value < node.Value)
            {
                return SearchRecursive(node.Left, value);
            }
            // If the value is greater than the current node, search in the right subtree
            else
            {
                return SearchRecursive(node.Right, value);
            }
        }

        public void Remove(int value)
        {
            // Use a helper method for the recursive implementation
            root = RemoveRecursive(root, value);
        }

        private TreeNode? RemoveRecursive(TreeNode? node, int value)
        {
            // Base case: if the node is null, the value is not in the tree
            if (node == null)
            {
                return null;
            }

            // If the value is less than the current node, recurse left
            if (value < node.Value)
            {
                node.Left = RemoveRecursive(node.Left, value);
            }
            // If the value is greater than the current node, recurse right
            else if (value > node.Value)
            {
                node.Right = RemoveRecursive(node.Right, value);
            }
            // If the value is equal to the current node, this is the node to remove
            else
            {
                // Case 1: Leaf node (no children)
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                // Case 2: Node with only right child
                else if (node.Left == null)
                {
                    return node.Right;
                }
                // Case 3: Node with only left child
                else if (node.Right == null)
                {
                    return node.Left;
                }
                // Case 4: Node with two children
                else
                {
                    // Find the minimum value in the right subtree (successor)
                    int minValue = FindMin(node.Right);
                    // Replace the current node's value with the successor's value
                    node.Value = minValue;
                    // Remove the successor from the right subtree
                    node.Right = RemoveRecursive(node.Right, minValue);
                }
            }

            return node;
        }


        private int FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.Value;
        }

        public void Insert(int value)
        {
            // Use a helper method for the recursive implementation
            root = InsertRecursive(root, value);
        }


        private TreeNode? InsertRecursive(TreeNode? node, int value)
        {
            // Base case: if the node is null, create a new node with the given value
            if (node == null)
            {
                return new TreeNode() { Value = value };
            }

            // If the value already exists, don't insert it again
            if (value == node.Value)
            {
                return node;
            }

            // Recursively insert into the left subtree if the value is smaller
            if (value < node.Value)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            // Recursively insert into the right subtree if the value is larger
            else
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            // Return the (unchanged) node pointer
            return node;
        }

        public List<int> InOrderTraversal() => InOrderTraversalHelper(root);

        private List<int> InOrderTraversalHelper(TreeNode? node)
        {
            // Base case: if the current node is null, return an empty list
            if (node == null)
            {
                return [];
            }

            // Recursively get the list from the left subtree
            var leftSubtreeList = InOrderTraversalHelper(node.Left);

            // Create a list with the current node's value
            var currentNodeList = new List<int> { node.Value };

            // Recursively get the list from the right subtree
            var rightSubtreeList = InOrderTraversalHelper(node.Right);

            // Combine all lists: left subtree, current node, right subtree
            return [.. leftSubtreeList, .. currentNodeList, .. rightSubtreeList];
        }

        public List<int> PreOrderTraversal() => PreOrderTraversalHelper(root);

        private List<int> PreOrderTraversalHelper(TreeNode? node)
        {
            // Base case: if the current node is null, return an empty list
            if (node == null) { return []; }

            // Create a list with the current node's value
            var currentNodeList = new List<int> { node.Value };

            // Recursively get the list from the left subtree
            var leftSubtreeList = PreOrderTraversalHelper(node.Left);

            // Recursively get the list from the right subtree
            var rightSubtreeList = PreOrderTraversalHelper(node.Right);

            // Combine all lists: current node, left subtree, and right subtree
            return [.. currentNodeList, .. leftSubtreeList, .. rightSubtreeList];
        }
    }
}

