using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Node
    {
        public string Key { get; set; }
        
        public Node Next { get; set; }
        public Node() { }
        public Node(string Key, Node Next)
        {
            this.Key = Key;
            this.Next = Next;
        }
    }
    public class SymbolTable
    {
        private List<Node> list;
        private int tableSize = 7919;
       
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private int HashFunction(string Key)
        {
            int index = 79;
            int asciiVal = 0;
            for (int i = 0; i < Key.Length; i++)
            {
                asciiVal = (int)Key[i] * i;
                index = index * 443 + asciiVal;
            }
            return index % tableSize;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public KeyValuePair<int, int> Insert(string Key)
        {
            int index = HashFunction(Key);
            Node node = list[index];

            if (node == null)
            {
                list[index] = new Node(Key, null);
                return new KeyValuePair<int, int>(index,0);
            }

            if (node.Key == Key)
                throw new Exception("Can't use same key!");
            int i = 0;
            while (node.Next != null)
            {
                node = node.Next;
                i++;
                if (node.Key == Key)
                    throw new Exception("Can't use same key!");
            }

            Node newNode = new Node(Key, null);
            node.Next = newNode;
            return new KeyValuePair<int, int>(index, i);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool Lookup(string Key)
        {
            int index = HashFunction(Key);
            Node node = list[index];
            while (node != null)
            {
                if (node.Key == Key)
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public KeyValuePair<int, int> Position(string Key) {
            int index = HashFunction(Key);
            Node node = list[index];
            int i = 0;
            while (node != null)
            {
                if (node.Key == Key)
                {
                    return new KeyValuePair<int, int>(index,i);
                }
                node = node.Next;
                i++;
            }
            return Insert(Key);

        }
    }
}
