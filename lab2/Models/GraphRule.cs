namespace Lab2.Models;

public class GraphRule
{
    public int Number { get; set; }
    public GraphNode TargetNode { get; set; }
    public List<GraphNode> StartNodes { get; set; }
    public int Mark { get; set; }

    public GraphRule(int number,
        List<GraphNode> startNodes,
        GraphNode targetNode,
        int mark = 0)
    {
        Number = number;
        TargetNode = targetNode;
        StartNodes = startNodes;
        Mark = mark;
    }
}