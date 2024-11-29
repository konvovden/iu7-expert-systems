using Lab2.Models;

namespace Lab2;

class Program
{
    static void Main(string[] args)
    {
        var nodes = Enumerable.Range(0, 34)
            .Select(n => new GraphNode(n))
            .ToList();

        var rules = new List<GraphRule>
        {
            new GraphRule(101, [nodes[1], nodes[2]], nodes[3]),
            new GraphRule(102, [nodes[3], nodes[2], nodes[4]], nodes[7]),
            new GraphRule(103, [nodes[5], nodes[6]], nodes[4]),
            new GraphRule(104, [nodes[8], nodes[31]], nodes[3]),
            new GraphRule(105, [nodes[7], nodes[9]], nodes[14]),
            new GraphRule(106, [nodes[4], nodes[10], nodes[11]], nodes[9]),
            new GraphRule(107, [nodes[12], nodes[13]], nodes[11]),
            new GraphRule(108, [nodes[21], nodes[15]], nodes[33]),
            new GraphRule(109, [nodes[16], nodes[17]], nodes[15]),
            new GraphRule(110, [nodes[9], nodes[21]], nodes[14]),
            new GraphRule(111, [nodes[18], nodes[32]], nodes[9]),
            new GraphRule(112, [nodes[19], nodes[20]], nodes[21]),
        };

        var bfs = new BFS(rules);
        
        bfs.Search(nodes[33], [nodes[16], nodes[17], nodes[19], nodes[20], nodes[5], nodes[6], nodes[10], nodes[12], nodes[13]]);
    }
}