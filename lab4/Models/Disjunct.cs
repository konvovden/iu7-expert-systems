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

    public bool IsOpposite(Disjunct other)
    {
        if (this.Predicates.Count != other.Predicates.Count)
            return false;
        
        for (int m = 0; m < this.Predicates.Count; m++)
        {
            if (this.Predicates[m].Sign == other.Predicates[m].Sign)
                return false;
            
            if(this.Predicates[m].Name != other.Predicates[m].Name)
                return false;

            if (this.Predicates[m].Variables.Count != other.Predicates[m].Variables.Count)
                return false;
            
            if (!this.Predicates[m].Variables.SequenceEqual(other.Predicates[m].Variables))
                return false;
        }

        return true;
    }
}