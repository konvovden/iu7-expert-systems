using Lab3.Extensions;
using Lab3.Models;
using Lab3.Models.Enums;

namespace Lab3;

public class Searcher
{
    private readonly List<Rule> _rules;
    private readonly List<Node> _openedNodes;
    private readonly List<Rule> _openedRules;
    private readonly List<Node> _closedNodes;
    private readonly List<Rule> _closedRules;
    private readonly List<Node> _forbiddenNodes;
    private readonly List<Rule> _forbiddenRules;

    private Node? _goalNode;
    private bool _solutionFlag;
    private bool _noSolutionFlag;

    public Searcher(List<Rule> rules)
    {
        _rules = rules;

        _openedNodes = new List<Node>();
        _openedRules = new List<Rule>();
        _closedNodes = new List<Node>();
        _closedRules = new List<Rule>();
        _forbiddenNodes = new List<Node>();
        _forbiddenRules = new List<Rule>();

        _goalNode = null;
    }

    public void Search(Node goalNode, List<Node> inputNodes)
    {
        _solutionFlag = true;
        _noSolutionFlag = true;
        
        _goalNode = goalNode;
        _openedNodes.Push(goalNode);
        
        inputNodes.ForEach(n => n.Mark = Mark.Closed);
        _closedNodes.AddRange(inputNodes);

        Console.WriteLine("Starting search...");
        OutputLists();
        
        while (_solutionFlag && _noSolutionFlag)
        {
            var childCount = SearchChild();

            if (!_solutionFlag)
            {
                Console.WriteLine($"Solution found: {string.Join(", ", _closedRules.Select(r => r.Number))}!");
            }
            else if (childCount == 0)
            {
                if (_openedNodes.Count == 0)
                {
                    _noSolutionFlag = false;
                    Console.WriteLine("Solution not found!");
                }
                else
                {
                    Console.WriteLine("No child found. Starting backtracking...");
                    BackTracking();
                }
            }
        }
    }
    
    private int SearchChild()
    {
        var childCount = 0;

        
        foreach (var rule in _rules)
        {
            var currentNode = _openedNodes.Peek();

            if (rule.Mark != Mark.None)
                continue;
            
            if (rule.TargetNode != currentNode)
                continue;

            rule.Mark = Mark.Viewed;
            _openedRules.Add(rule);
            Console.WriteLine($"Added Rule {rule.Number} to Opened Rules");

            var newOpenedNodes = rule.InputNodes
                .Where(n => !_closedNodes.Contains(n))
                .Reverse()
                .ToList();
            
            if (newOpenedNodes.Count == 0)
            {
                Console.WriteLine("All Rule input Nodes are closed. Starting labeling...");
                Labeling();
            }
            else
            {
                newOpenedNodes.ForEach(n => _openedNodes.Push(n));
                Console.WriteLine($"Added Nodes {string.Join(", ", newOpenedNodes.Select(n => n.Number))} to Opened Nodes");

                OutputLists();
            }

            childCount++;
            break;
        }

        return childCount;
    }

    private void Labeling()
    {
        while (_openedRules.Peek().InputNodes.All(n => _closedNodes.Contains(n)) && _solutionFlag)
        {
            var currentRule = _openedRules.Pop();
            _closedRules.Add(currentRule);
            Console.WriteLine($"Added rule {currentRule.Number} to closed Rules");

            var currentNode = _openedNodes.Pop();
            _closedNodes.Add(currentNode);
            Console.WriteLine($"Added node {currentNode.Number} to closed Nodes");

            if (currentNode == _goalNode)
            {
                Console.WriteLine($"Node {currentNode.Number} is Goal node");
                _solutionFlag = false;
                break;
            }

        }
        
        OutputLists();
    }

    private void BackTracking()
    {
        var currentNode = _openedNodes.Pop();
        currentNode.Mark = Mark.Forbidden;
        _forbiddenNodes.Add(currentNode);
        Console.WriteLine($"Node {currentNode.Number} moved from Opened to Forbidden nodes");
        
        var currentRule = _openedRules.Pop();
        currentRule.Mark = Mark.Forbidden;
        _forbiddenRules.Add(currentRule);
        Console.WriteLine($"Rule {currentRule.Number} move from Opened to Forbidden rules");

        var nodesToRemove = currentRule.InputNodes.Where(n => _openedNodes.Contains(n));
        _openedNodes.RemoveAll(n => nodesToRemove.Contains(n));
        Console.WriteLine($"Rule {currentRule.Number} input nodes {string.Join(", ", nodesToRemove.Select(n => n.Number))} removed from opened nodes");
        
        OutputLists();
    }

    private void OutputLists()
    {
        Console.WriteLine($"Opened Rules: {string.Join(", ", _openedRules.Select(r => r.Number))}");
        Console.WriteLine($"Opened Nodes: {string.Join(", ", _openedNodes.Select(n => n.Number))}");
        Console.WriteLine($"Closed Rules: {string.Join(", ", _closedRules.Select(r => r.Number))}");
        Console.WriteLine($"Closed Nodes: {string.Join(", ", _closedNodes.Select(n => n.Number))}");
        Console.WriteLine($"Forbidden Rules: {string.Join(", ", _forbiddenRules.Select(r => r.Number))}");
        Console.WriteLine($"Forbidden Nodes: {string.Join(", ", _forbiddenNodes.Select(n => n.Number))}");
        Console.WriteLine("------------------------------------------");
    }
}