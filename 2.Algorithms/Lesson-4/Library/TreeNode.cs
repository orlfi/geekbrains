
namespace Library
{
    public class TreeNode
    {
        public int Value { get; set; }

        internal TreeNode leftChild;
        public TreeNode LeftChild => leftChild;

        internal TreeNode rightChild;
        public TreeNode RightChild => rightChild;

        public TreeNode Parent { get;  }

        internal int depth;

        internal BinnaryTree tree;
        public TreeNode(int value)
        {
            Value = value;
        }

        internal TreeNode(TreeNode parent, int value, BinnaryTree tree)
        {
            this.Parent = parent;
            Value = value;
            this.tree = tree;
        }

        public override bool Equals(object obj)
        {
            TreeNode node = obj as TreeNode;
            if (node == null)
                return false;

            return this.Value == node.Value;
        }
    }
}