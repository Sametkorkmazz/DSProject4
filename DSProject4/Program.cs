using System;

namespace DSProject4
{
    class TrieNode
    {
        public TrieNode[] childNodes;
        public bool kelimeSonu;

        public TrieNode()
        {
            childNodes = new TrieNode[26];
            kelimeSonu = false;
            for (int i = 0; i < 26; i++)
            {
                childNodes[i] = null;
            }
        }
    }

    class Trie
    {
        private TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void insert(string kelime)
        {
            int harfSayisi = kelime.Length;
            TrieNode node = root;
            for (int i = 0; i < harfSayisi; i++)
            {
                int index = kelime[i] - 'a';
                if (node.childNodes[index] == null)
                {
                    node.childNodes[index] = new TrieNode();
                }

                node = node.childNodes[index];
            }

            node.kelimeSonu = true;
        }

        public bool find(string kelime)
        {
            int harfSayisi = kelime.Length;
            TrieNode node = root;
            for (int i = 0; i < harfSayisi; i++)
            {
                int index = kelime[i] - 'a';
                if (node.childNodes[index] == null)
                {
                    return false;
                }

                node = node.childNodes[index];
            }

            return (node.kelimeSonu);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Trie tree = new Trie();
            string[] kelimeler = { "car", "house", "computer", "weather", "key", "plane" };
            for (int i = 0; i < kelimeler.Length; i++)
            {
                tree.insert(kelimeler[i]);
            }

            if (tree.find("car"))
            {
                Console.WriteLine("Car Kelimesi bulundu!");
            }

            if (tree.find("keyboard"))
            {
                Console.WriteLine("Keyboard kelimesi bulundu!");
            }
            else
            {
                Console.WriteLine("Keyboard kelimesi bulunamadı!");
            }
        }
    }
}