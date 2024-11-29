using Lab1.Finders;
using Lab1.Models;

namespace Lab1;

class Program
{
    private static Graph example1 => new Graph(new List<GraphEdge>()
    {
        new GraphEdge(0, 1, "101"),
        new GraphEdge(0, 3, "103"),
        new GraphEdge(3, 4, "104"),
        new GraphEdge(3, 5, "105"),
        new GraphEdge(1, 2, "102"),
        new GraphEdge(2, 4, "109"),
        new GraphEdge(2, 6, "108"),
        new GraphEdge(5, 4, "107"),
        new GraphEdge(6, 5, "106")
    });

    static void Main(string[] args)
    {
        var dfs = new GraphDFSPathFinder(example1, 0, 5);
        var dfsResult = dfs.Find();

        Console.WriteLine("DFS: " + string.Join(" ", dfsResult));

        var bfs = new GraphBFSPathFinder(example1, 0, 5);
        var bfsResult = bfs.Find();

        Console.WriteLine("BFS: " + string.Join(" ", bfsResult));
    }
}