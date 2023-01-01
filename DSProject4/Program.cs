using System;
using System.Collections.Generic;

namespace DSProject4
{
    class vertex
    {
        public char label;

        public vertex(char label)
        {
            this.label = label;
        }
    }

    class Graph
    {
        protected bool[] visited;
        protected vertex[] vertexList;
        protected int[,] adjMatrix;
        protected int nvertex;
        protected int inf = 10000;

        public Graph()
        {
            nvertex = 0;
            vertexList = new vertex[10];
            adjMatrix = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    adjMatrix[i, j] = inf;
                }
            }

            visited = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                visited[i] = false;
            }
        }

        public int getVerts()
        {
            return nvertex;
        }

        public void displayMatrix()
        {
            Console.Write(new string(' ', 15));

            for (int i = 0; i < nvertex; i++)
            {
                Console.Write($"{vertexList[i].label,-5}");
            }

            Console.WriteLine();
            for (int i = 0; i < nvertex; i++)
            {
                string a = $"{vertexList[i].label} ({i})";
                Console.Write($"{a,-15}");
                for (int j = 0; j < nvertex; j++)
                {
                    if (adjMatrix[i, j] == 10000)
                    {
                        Console.Write($"{double.PositiveInfinity,-5}");
                    }
                    else
                    {
                        Console.Write($"{adjMatrix[i, j],-5}");
                    }
                }

                Console.WriteLine();
            }
        }

        public void addVertex(char l)
        {
            vertexList[nvertex++] = new vertex(l);
        }
    }

    class GraphNW : Graph
    {
        public void addEdge(int start, int end, int weight)
        {
            adjMatrix[start, end] = weight;
            adjMatrix[end, start] = weight;
        }

        public int getMin(int[] dist, bool[] inTree)
        {
            int min = inf;
            int minİndex = 0;
            for (int i = 0; i < nvertex; i++)
            {
                if (!inTree[i] && min > dist[i])
                {
                    min = dist[i];
                    minİndex = i;
                }
            }

            return minİndex;
        }

        public void PrimsMst(int start)
        {
            int[] parent = new int[nvertex];
            int[] dist = new int[nvertex];
            bool[] inTree = new bool[nvertex];
            for (int i = 0; i < nvertex; i++)
            {
                inTree[i] = false;
                dist[i] = inf;
            }

            dist[0] = 0;
            parent[0] = 0;
            for (int i = 0; i < nvertex - 1; i++)
            {
                int minİndex = getMin(dist, inTree);
                inTree[minİndex] = true;
                for (int j = 0; j < nvertex; j++)
                {
                    if (!inTree[j] && adjMatrix[minİndex, j] < dist[j])
                    {
                        parent[j] = minİndex;
                        dist[j] = adjMatrix[minİndex, j];
                    }
                }
            }

            printTree(parent);
        }

        private void printTree(int[] parent)
        {
            Console.WriteLine("Prim's Minumum Spanning Tree: ");
            Console.WriteLine("Edge \tWeight");
            for (int i = 0; i < nvertex; i++)
            {
                if (parent[i] != i)
                {
                    Console.Write($"{vertexList[parent[i]].label} - {vertexList[i].label}\t{adjMatrix[i, parent[i]]}\n");
                }
            }
        }
    }

    class GraphDW : Graph
    {
        public void addEdge(int start, int end, int weight)
        {
            adjMatrix[start, end] = weight;
        }

        public void Dijkstra(int start)
        {
            char[] parent = new char[nvertex];
            int[] dist = new int[nvertex];

            for (int i = 0; i < nvertex; i++)
            {
                dist[i] = adjMatrix[start, i];
                parent[i] = vertexList[start].label;
            }

            visited[start] = true;
            for (int i = 0; i < nvertex; i++)
            {
                int min = inf;
                int minİndex = 0;
                for (int j = 0; j < nvertex; j++)
                {
                    if (!visited[j] && min > dist[j])
                    {
                        min = dist[j];
                        minİndex = j;
                    }
                }

                visited[minİndex] = true;
                for (int j = 0; j < nvertex; j++)
                {
                    if (!visited[j] && (min + adjMatrix[minİndex, j]) < dist[j])
                    {
                        dist[j] = min + adjMatrix[minİndex, j];
                        parent[j] = vertexList[minİndex].label;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                visited[i] = false;
            }

            displayPath(start, dist, parent);
        }

        private void displayPath(int start, int[] dist, char[] parent)
        {
            Console.Write($"\n{vertexList[start].label} düğümünden diğer düğümlere en kısa uzaklık: \n");

            for (int i = 0; i < nvertex; i++)
            {
                if (dist[i] == inf)
                {
                    Console.Write($"{vertexList[i].label} Düğümüne, Minimum: {double.PositiveInfinity} Bir önceki düğüm: {parent[i]}\n");
                }
                else
                {
                    Console.Write($"{vertexList[i].label} Düğümüne, Minimum: {dist[i]} Bir önceki düğüm: {parent[i]}\n");
                }
            }

            Console.WriteLine();
        }
    }

    class GraphN : Graph
    {
        public void addedge(int start, int end)
        {
            adjMatrix[start, end] = 1;
            adjMatrix[end, start] = 1;
        }

        public void BFT(int start)
        {
            Queue<int> kuyruk = new Queue<int>();
            kuyruk.Enqueue(start);
            int nodeToVisit;
            Console.Write($"{vertexList[start].label} ");
            visited[start] = true;
            while (kuyruk.Count > 0)
            {
                int node = kuyruk.Dequeue();
                while ((nodeToVisit = getUnvisitedNode(node)) != -1)
                {
                    Console.Write($"{vertexList[nodeToVisit].label} ");
                    kuyruk.Enqueue(nodeToVisit);
                }
            }

            for (int i = 0; i < nvertex; i++)
            {
                visited[i] = false;
            }

            Console.WriteLine();
        }

        public int getUnvisitedNode(int node)
        {
            for (int i = 0; i < nvertex; i++)
            {
                if (!visited[i] && adjMatrix[node, i] == 1)
                {
                    visited[i] = true;
                    return i;
                }
            }

            return -1;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            while (a == 0)
            {
                Console.WriteLine("Yönsüz ve ağırlıksız Graph Breadth First Traversel için (1)\nYönlü ve ağırlıklı graph Dijkstra sıralaması için (2)\nPrim's algoritması ile minumum spanning tree için (3)\nÇıkmak için (4)");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        BFT();
                        break;
                    case "2":
                        Console.Clear();
                        Dijkstra();
                        break;
                    case "3":
                        Console.Clear();
                        Prim();
                        break;
                    case "4":
                        Console.Clear();
                        a = 1;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        static void Prim()
        {
            GraphNW graph = new GraphNW();
            graph.addVertex('A');
            graph.addVertex('B');
            graph.addVertex('C');
            graph.addVertex('D');
            graph.addVertex('E');
            graph.addVertex('F');
            graph.addEdge(0, 1, 3);
            graph.addEdge(0, 3, 5);
            graph.addEdge(2, 4, 8);
            graph.addEdge(2, 3, 2);
            graph.addEdge(1, 2, 4);
            graph.addEdge(1, 3, 7);
            graph.addEdge(5, 2, 18);
            graph.addEdge(5, 3, 13);
            graph.addEdge(5, 4, 6);
            graph.displayMatrix();
            graph.PrimsMst(0);
        }

        static void BFT()
        {
            GraphN graph = new GraphN();
            graph.addVertex('A');
            graph.addVertex('B');
            graph.addVertex('C');
            graph.addVertex('D');
            graph.addVertex('E');
            graph.addVertex('F');
            graph.addedge(0, 1);
            graph.addedge(0, 3);
            graph.addedge(2, 4);
            graph.addedge(1, 2);
            graph.addedge(5, 2);
            graph.addedge(5, 3);
            graph.addedge(1, 4);
            Console.WriteLine("Bağlılık Matrisi:");
            graph.displayMatrix();

            Console.Write("Başlamak istediğiniz düğümün indexini girin: ");
            int a = Int32.Parse(Console.ReadLine());
            if (a < 0 || a > 5)
            {
                Console.Clear();

                Console.WriteLine("Geçersiz İndex!");
            }
            else
            {
                Console.WriteLine("Breadth First Gezme ");
                graph.BFT(a);
            }
        }

        static void Dijkstra()
        {
            GraphDW graph = new GraphDW();
            graph.addVertex('A');
            graph.addVertex('B');
            graph.addVertex('C');
            graph.addVertex('D');
            graph.addVertex('E');
            graph.addEdge(0, 1, 5);
            graph.addEdge(0, 3, 10);
            graph.addEdge(2, 4, 14);
            graph.addEdge(2, 1, 11);
            graph.addEdge(1, 3, 2);
            graph.addEdge(3, 2, 7);
            graph.addEdge(3, 4, 8);
            Console.WriteLine("Ağırlık Matrisi:");

            graph.displayMatrix();
            Console.Write("Başlamak istediğiniz düğümün indexini girin: ");
            int a = Int32.Parse(Console.ReadLine());
            if (a < 0 || a > 5)
            {
                Console.Clear();
                Console.WriteLine("Geçersiz İndex!");
            }
            else
            {
                graph.Dijkstra(a);
            }
        }
    }
}