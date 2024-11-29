using Lab4.Models.Enums;

namespace Lab4.Models;

public class Predicate
{
    public string Name { get; set; }
    public bool Sign { get; set; }
    public List<Variable> Variables { get; set; }

    public Predicate(string name, bool sign, List<Variable> variables)
    {
        Name = name;
        Sign = sign;
        Variables = variables;
    }

    public override string ToString()
    {
        var signString = Sign ? "" : "^";
        return $"{signString}{Name}({string.Join(", ", Variables)})";
    }

    public bool CanBeUnified(Predicate other)
    {
        if (Name != other.Name)
            return false;

        if (Sign == other.Sign)
            return false;

        if (Variables.Count != other.Variables.Count)
            return false;

        for (var i = 0; i < Variables.Count; i++)
        {
            if (Variables[i].Flag == Flag.HasValue && other.Variables[i].Flag == Flag.HasValue &&
                Variables[i].Value != other.Variables[i].Value)
                return false;
        }

        return true;
    }
}