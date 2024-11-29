using Lab4.Models;
using Lab4.Models.Enums;

namespace Lab4;

class Program
{
    static void Main(string[] args)
    {
        
        // P1(BT), ^P2(DT, BT), ^P1(y) v P2(x, y) v ^l(x, y)
        var disjuncts = new List<Disjunct>
        {
            new Disjunct([new Predicate("P1", true, [new Variable("BT", "BT", Flag.HasValue)])]),
            new Disjunct([
                new Predicate("P2", false,
                    [new Variable("DT", "DT", Flag.HasValue), new Variable("BT", "BT", Flag.HasValue)])
            ]),
            new Disjunct([
                new Predicate("P1", false, [new Variable("y")]),
                new Predicate("P2", true, [new Variable("x"), new Variable("y")]),
                new Predicate("l", false, [new Variable("x"), new Variable("y")])
            ])
        };

        var unifier = new Unifier();

        unifier.Unify(disjuncts);
    }
}