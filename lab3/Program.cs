using Lab3.Models;

namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        var nodes = Enumerable.Range(0, 34)
            .Select(n => new Node(n))
            .ToList();

        var rules = new List<Rule>
        {
            new Rule(101, [nodes[1], nodes[2]], nodes[3]),
            new Rule(102, [nodes[3], nodes[2], nodes[4]], nodes[7]),
            new Rule(103, [nodes[5], nodes[6]], nodes[4]),
            new Rule(104, [nodes[8], nodes[31]], nodes[3]),
            new Rule(105, [nodes[7], nodes[9]], nodes[14]),
            new Rule(106, [nodes[4], nodes[10], nodes[11]], nodes[9]),
            new Rule(107, [nodes[12], nodes[13]], nodes[11]),
            new Rule(108, [nodes[21], nodes[15]], nodes[33]),
            new Rule(109, [nodes[16], nodes[17]], nodes[15]),
            new Rule(110, [nodes[9], nodes[21]], nodes[14]),
            new Rule(111, [nodes[18], nodes[32]], nodes[9]),
            new Rule(112, [nodes[19], nodes[20]], nodes[21]),
        };

        var searcher = new Searcher(rules);
        
        searcher.Search(nodes[14], [nodes[18], nodes[32], nodes[19], nodes[20]]);
    }
}