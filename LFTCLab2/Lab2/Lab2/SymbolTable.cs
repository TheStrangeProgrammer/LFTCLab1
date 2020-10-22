//the insert and the position methods could be merged into a single method that checks if a key is in the symbol table and in case it finds the key, it returns the position,
//otherwise it adds the key to the corresponding location

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
        /// Hash function
        /// takes a string and returns an int
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>index</returns>
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
        /// Inserts a Key in the ST
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>position of the token in hashtable KeyValuePair<int, int></returns>
        public KeyValuePair<int, int> Insert(string Key)
        {
            int index = HashFunction(Key); //by combining the insert and position methods, you avoid doing some computations twice, like computing the hash value 
                                            //and checking for the key inside the linked lists
            Node node = list[index];

            if (node == null)
            {
                list[index] = new Node(Key, null);
                return new KeyValuePair<int, int>(index,0);
            }

            if (node.Key == Key)
                throw new Exception("Can't use same key!"); 
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
        /// Searches the token in symbol table ST
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>true if Key is in ST,false otherwise</returns>
        public bool Lookup(string Key)  //this method is not really necessary
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

        /// <summary>
        /// Searches the token in symbol table ST
        /// if found then returns position
        /// if not found insert in ST and return position
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>position of the token in hashtable KeyValuePair<int, int></returns>
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
