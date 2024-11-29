using Lab1.Models;

namespace Lab1.Finders;

public class GraphBFSPathFinder
{
    private readonly Graph _graph;
    private readonly int _startNode;
    private readonly int _targetNode;

    private readonly Queue<int> _openedNodes;
    private readonly List<int> _closedNodes;
    private readonly Dictionary<int, int> _resultPath;

    private bool _solutionNotFound;
    private bool _childFound;

    public GraphBFSPathFinder(Graph graph, 
        int startNode,
        int targetNode)
    {
        _graph = graph;
        _startNode = startNode;
        _targetNode = targetNode;

        _openedNodes = new Queue<int>();
        _closedNodes = new List<int>();
        _resultPath = new Dictionary<int, int>();

        _solutionNotFound = true;
        _childFound = true;
    }

    public List<int> Find()
    {
        _openedNodes.Enqueue(_startNode);
        WriteNodesToConsole();
        
        while (_childFound && _solutionNotFound)
        {
            FindChild();
            
            if (!_solutionNotFound)
                break;

            if (!_childFound && _openedNodes.Count > 0)
            {
                var currentNode = _openedNodes.Dequeue();
                _closedNodes.Add(currentNode);
                _childFound = true;
                WriteNodesToConsole();
            }
        }

        if (_solutionNotFound)
            throw new Exception("Solution not found!");

        return CalculateResultPath();
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
            _openedNodes.Enqueue(edge.EndNode);
            _resultPath[edge.EndNode] = edge.StartNode;
            _childFound = true;

            if (edge.EndNode == _targetNode)
                _solutionNotFound = false;
            
            WriteNodesToConsole();

            break;
        }
    }

    private List<int> CalculateResultPath()
    {
        var result = new List<int>();
        var currentNode = _targetNode;

        while (_resultPath.TryGetValue(currentNode, out var nextNode))
        {
            result.Add(currentNode);

            currentNode = nextNode;
        }
        
        result.Add(currentNode);

        result.Reverse();
        
        return result;
    }

    private void WriteNodesToConsole()
    {
        Console.WriteLine($"Opened: {string.Join(", ", _openedNodes)} | Closed: {string.Join(", ", _closedNodes)}");
    }
}