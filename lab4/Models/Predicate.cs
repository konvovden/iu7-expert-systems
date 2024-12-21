using Lab4.Models.Enums;

namespace Lab4.Models;

public class Predicate : IEquatable<Predicate>
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

    public bool Equals(Predicate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Sign == other.Sign && Variables.SequenceEqual(other.Variables);
    }
}