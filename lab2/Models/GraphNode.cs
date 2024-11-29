namespace Lab2.Models;

public class GraphNode
{
    public int Number { get; set; }
    public int Mark { get; set; }

    public GraphNode(int number, int mark = 0)
    {
        Number = number;
        Mark = mark;
    }
}