using Lab4.Models.Enums;

namespace Lab4.Models;

public class Variable
{
    public string Name { get; set; }
    public string Value { get; set; }
    public Flag Flag { get; set; }

    public Variable(string name, string value = "", Flag flag = Flag.NoValue)
    {
        Name = name;
        Value = value;
        Flag = flag;
    }

    public override string ToString()
    {
        return Flag switch
        {
            Flag.NoValue => Name,
            Flag.Linked => Value,
            Flag.HasValue => Value,
            _ => Name
        };
    }
}