namespace StringProcessorAPI.Logic
{
    class Node
    {
        public int Key;
        public Node Left;
        public Node Right;

        public Node(int key)
        {
            Key = key;
            Left = null;
            Right = null;
        }
    }

    class BinaryTree
    {
        public Node Root;

        public BinaryTree()
        {
            Root = null;
        }

        public void Add(int key)
        {
            Node node = new Node(key);

            if (Root == null)
            {
                Root = node;
            }
            else
            {
                RecursiveAdd(Root, node);
            }
        }

        private void RecursiveAdd(Node currentNode, Node addNode)
        {
            if (addNode.Key < currentNode.Key)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = addNode;
                }
                else
                {
                    RecursiveAdd(currentNode.Left, addNode);
                }
            }
            else if (addNode.Key >= currentNode.Key)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = addNode;
                }
                else
                {
                    RecursiveAdd(currentNode.Right, addNode);
                }
            }
        }

        public void InfinixOrder(Node Root, List<int> sortedList)
        {
            if (Root == null)
            {
                return;
            }

            InfinixOrder(Root.Left, sortedList);
            sortedList.Add(Root.Key);
            InfinixOrder(Root.Right, sortedList);
        }

    }

    public class TreeSortClass
    {
        public static int[] TreeSort(int[] array)
        {
            BinaryTree binaryTree = new BinaryTree();

            foreach (int item in array)
            {
                binaryTree.Add(item);
            }

            List<int> sortedList = new List<int>();
            binaryTree.InfinixOrder(binaryTree.Root, sortedList);

            return sortedList.ToArray();
        }
    }
}
