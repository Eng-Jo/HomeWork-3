using System;
using System.Collections.Generic;
using System.Linq;

class DisjointSet
{
    private int[] parent;
    private int[] rank;

    
    public DisjointSet(int n)
    {
        parent = new int[n];
        rank = new int[n];

        for (int i = 0; i < n; i++)
        {
            parent[i] = i;  
            rank[i] = 0;     
        }
    }

    
    public int Find(int u)
    {
        if (parent[u] != u)
        {
            parent[u] = Find(parent[u]);  
        }
        return parent[u];
    }

    
    public void Union(int u, int v)
    {
        int rootU = Find(u);
        int rootV = Find(v);

        if (rootU != rootV)
        {
            if (rank[rootU] > rank[rootV])
            {
                parent[rootV] = rootU;
            }
            else if (rank[rootU] < rank[rootV])
            {
                parent[rootU] = rootV;
            }
            else
            {
                parent[rootV] = rootU;
                rank[rootU]++;
            }
        }
    }
}

class KruskalMST
{
    private int vertices;
    private List<Edge> edges;

   
    public KruskalMST(int vertices, List<Edge> edges)
    {
        this.vertices = vertices;
        this.edges = edges;
    }

    
    public class Edge
    {
        public int Weight;
        public int U;
        public int V;

        public Edge(int weight, int u, int v)
        {
            Weight = weight;
            U = u;
            V = v;
        }
    }

    
    public void FindMST()
    {
       
        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        
        DisjointSet ds = new DisjointSet(vertices);

        List<Edge> mst = new List<Edge>();  
        int mstWeight = 0;  

        
        foreach (Edge edge in edges)
        {
            int u = edge.U;
            int v = edge.V;
            int weight = edge.Weight;

           
            if (ds.Find(u) != ds.Find(v))
            {
                ds.Union(u, v);
                mst.Add(edge);
                mstWeight += weight;
            }

            
            if (mst.Count == vertices - 1)
            {
                break;
            }
        }

        
        Console.WriteLine("Edges in MST:");
        foreach (Edge edge in mst)
        {
            Console.WriteLine($"({edge.U}, {edge.V}) - Weight: {edge.Weight}");
        }
        Console.WriteLine("Total weight of MST: " + mstWeight);
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        List<KruskalMST.Edge> edges = new List<KruskalMST.Edge>
        {
            new KruskalMST.Edge(1, 0, 1),
            new KruskalMST.Edge(2, 0, 2),
            new KruskalMST.Edge(3, 1, 2),
            new KruskalMST.Edge(4, 1, 3),
            new KruskalMST.Edge(5, 2, 3)
        };

        
        KruskalMST kruskal = new KruskalMST(4, edges);

        
        kruskal.FindMST();
    }
}
