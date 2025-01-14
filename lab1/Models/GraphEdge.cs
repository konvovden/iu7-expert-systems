namespace Lab1.Models;

public class GraphEdge
{
    public int StartNode { get; set; }
    public int EndNode { get; set; }
    public string Value { get; set; }
    public bool Mark { get; set; }

    public GraphEdge(int startNode, int endNode, string value, bool mark = false)
    {
        StartNode = startNode;
        EndNode = endNode;
        Mark = mark;
    }
}