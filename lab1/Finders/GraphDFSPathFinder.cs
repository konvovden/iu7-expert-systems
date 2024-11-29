using Lab1.Models;

namespace Lab1.Finders;

public class GraphDFSPathFinder
{
    private readonly Graph _graph;
    private readonly int _startNode;
    private readonly int _targetNode;

    private readonly Stack<int> _openedNodes;
    private readonly List<int> _closedNodes;

    private bool _solutionNotFound;
    private bool _childFound;

    public GraphDFSPathFinder(Graph graph, 
        int startNode,
        int targetNode)
    {
        _graph = graph;
        _startNode = startNode;
        _targetNode = targetNode;

        _openedNodes = new Stack<int>();
        _closedNodes = new List<int>();

        _solutionNotFound = true;
        _childFound = true;
    }

    public List<int> Find()
    {
        _openedNodes.Push(_startNode);
        WriteNodesToConsole();

        while (_childFound && _solutionNotFound)
        { 
            FindChild();
            
            if (!_solutionNotFound)
                break;

            if (!_childFound && _openedNodes.Count > 0)
            {
                var currentNode = _openedNodes.Pop();
                _closedNodes.Add(currentNode);
                _childFound = true;
                WriteNodesToConsole();
            }
        }

        if (_solutionNotFound)
            throw new Exception("Solution not found!");

        return _openedNodes.Reverse().ToList();
    }

    private void FindChild()
    {
        _childFound = false;

        var currentNode = _openedNodes.Peek();
        
        foreach (var edge in _graph.Edges)
        {
            if (edge.StartNode != currentNode)
                continue;
            
            if (_openedNodes.Contains(edge.EndNode) || _closedNodes.Contains(edge.EndNode))
                continue;
            
            if (edge.Mark)
                continue;

            edge.Mark = true;
            _openedNodes.Push(edge.EndNode);
            _childFound = true;

            if (edge.EndNode == _targetNode)
                _solutionNotFound = false;

            WriteNodesToConsole();

            break;
        }
    }

    private void WriteNodesToConsole()
    {
        Console.WriteLine($"Opened: {string.Join(", ", _openedNodes)} | Closed: {string.Join(", ", _closedNodes)}");
    }
}