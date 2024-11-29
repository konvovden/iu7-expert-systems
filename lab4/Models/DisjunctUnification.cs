namespace Lab4.Models;

public class DisjunctUnification
{
    public Disjunct MainDisjunct { get; set; }
    public Predicate MainPredicate { get; set; }
    public Disjunct TargetDisjunct { get; set; }
    public Predicate TargetPredicate { get; set; }

    public DisjunctUnification(Disjunct mainDisjunct, 
        Predicate mainPredicate,
        Disjunct targetDisjunct,
        Predicate targetPredicate)
    {
        MainDisjunct = mainDisjunct;
        MainPredicate = mainPredicate;
        TargetDisjunct = targetDisjunct;
        TargetPredicate = targetPredicate;
    }
}