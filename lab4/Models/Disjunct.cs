namespace Lab4.Models;

public class Disjunct
{
    public List<Predicate> Predicates { get; set; }

    public Disjunct(List<Predicate> predicates)
    {
        Predicates = predicates;
    }

    public override string ToString()
    {
        return string.Join(" v ", Predicates);
    }
}