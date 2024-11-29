using Lab3.Models.Enums;

namespace Lab3.Models;

public class Node
{
    public int Number { get; set; }
    public Mark Mark { get; set; }

    public Node(int number, Mark mark = Mark.None)
    {
        Number = number;
        Mark = mark;
    }
}