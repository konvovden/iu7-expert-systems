using Lab4.Models.Enums;

namespace Lab4.Models;

public class Variable : IEquatable<Variable>
{
    public string Name { get; set; }
    public Flag Flag { get; set; }

    public Variable(string name, Flag flag = Flag.Variable)
    {
        Name = name;
        Flag = flag;
    }

    public override string ToString()
    {
        return Name;
    }

    public bool Equals(Variable? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this.Name == other.Name && this.Flag == other.Flag;
    }
}