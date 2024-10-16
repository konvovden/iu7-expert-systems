namespace Lab1.Models;

public class Graph
{
    public List<GraphEdge> Edges { get; set; }

    public Graph(List<GraphEdge> edges)
    {
        Edges = edges;
    }
}