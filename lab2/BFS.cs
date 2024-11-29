using Lab2.Models;

namespace Lab2;

public class BFS
{
    public List<GraphRule> Rules { get; set; }
    public List<GraphNode> ClosedNodes { get; set; }
    public List<GraphRule> ClosedRules { get; set; }
    public GraphNode? GoalNode { get; set; }
    public bool SolutionFound { get; set; }
    public bool SolutionNotFound { get; set; }

    public BFS(List<GraphRule> rules)
    {
        Rules = rules;
        ClosedNodes = new List<GraphNode>();
        ClosedRules = new List<GraphRule>();
        GoalNode = null;
        SolutionFound = true;
        SolutionNotFound = true;
    }

    public void Search(GraphNode goal, List<GraphNode> inputNodes)
    {
        GoalNode = goal;
        ClosedNodes.AddRange(inputNodes);

        while (SolutionFound && SolutionNotFound)
        {
            var closedRulesCount = CloseRules();
            
            if (!SolutionNotFound)
            {
                Console.WriteLine("Solution found, search stopped");
                
                break;
            }

            if (closedRulesCount == 0)
            {
                SolutionFound = false;
                Console.WriteLine("No closed nodes on iteration, no solution");
                break;
            }
            
            Console.WriteLine($"Closed {closedRulesCount} rules");
        }
    }

    private int CloseRules()
    {
        var closedRulesCount = 0;
        
        foreach (var rule in Rules)
        {
            if (!SolutionNotFound)
                break;
            
            if (rule.Mark != 0)
                continue;
            
            if (rule.StartNodes.All(n => ClosedNodes.Contains(n)))
            {
                rule.Mark = 1;
                rule.TargetNode.Mark = 1;

                ClosedRules.Add(rule);
                
                if (!ClosedNodes.Contains(rule.TargetNode))
                    ClosedNodes.Add(rule.TargetNode);

                if (rule.TargetNode == GoalNode)
                    SolutionNotFound = false;

                closedRulesCount++;

                Console.WriteLine($"Close rule {rule.Number} and node {rule.TargetNode.Number}");
                OutputLists();
            }
        }

        return closedRulesCount;
    }

    private void OutputLists()
    {
        Console.WriteLine($"Closed rules: {string.Join(", ", ClosedRules.Select(r => r.Number))}");
        Console.WriteLine($"Closed nodes: {string.Join(", ", ClosedNodes.Select(n => n.Number))}");
    }
}