using Lab3.Models.Enums;

namespace Lab3.Models;

public class Rule
{
    public int Number { get; set; }
    public Node TargetNode { get; set; }
    public List<Node> InputNodes { get; set; }
    public Mark Mark { get; set; }

    public Rule(int number, List<Node> inputNodes, Node targetNode, Mark mark = Mark.None)
    {
        Number = number;
        TargetNode = targetNode;
        InputNodes = inputNodes;
        Mark = mark;
    }
}