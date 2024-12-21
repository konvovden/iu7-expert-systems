using Lab4.Models;
using Lab4.Models.Enums;

namespace Lab4;

class Program
{
    static void Main(string[] args)
    {
        // 1. S(x1) v M(x1)
        // 2. ^M(x2) v ^L(x2, дождь)
        // 3. ^S(x3) v L(x3, снег)
        // 4. ^L(Лена, y1) v ^L(Петя, y1)
        // 5. L(Лена, y2) v L(Петя, y2)
        // 6. L(Петя, дождь)
        // 7. L(Петя, снег)
        // Inverted: ^M(x4) v S(x4)
        
        var disjuncts = new List<Disjunct>
        {
            new Disjunct([
                new Predicate("S", true, [new Variable("x1", Flag.Variable)]),
                new Predicate("M", true, [new Variable("x1", Flag.Variable)])
            ]),
            new Disjunct([
                new Predicate("M", false, [new Variable("x2", Flag.Variable)]),
                new Predicate("L", false, [new Variable("x2", Flag.Variable), new Variable("дождь", Flag.Constant)])
            ]),
            new Disjunct([
                new Predicate("S", false, [new Variable("x3", Flag.Variable)]),
                new Predicate("L", true, [new Variable("x3", Flag.Variable), new Variable("снег", Flag.Constant)])
            ]),
            new Disjunct([
                new Predicate("L", false, [new Variable("Лена", Flag.Constant), new Variable("y1", Flag.Variable)]),
                new Predicate("L", false, [new Variable("Петя", Flag.Constant), new Variable("y1", Flag.Variable)])
            ]),
            new Disjunct([
                new Predicate("L", true, [new Variable("Лена", Flag.Constant), new Variable("y2", Flag.Variable)]),
                new Predicate("L", true, [new Variable("Петя", Flag.Constant), new Variable("y2", Flag.Variable)])
            ]),
            new Disjunct([
                new Predicate("L", true, [new Variable("Петя", Flag.Constant), new Variable("дождь", Flag.Constant)])
            ]),
            new Disjunct([
                new Predicate("L", true, [new Variable("Петя", Flag.Constant), new Variable("снег", Flag.Constant)])
            ])
        };

        var invertedTarget = new Disjunct([
            new Predicate("M", false, [new Variable("x4", Flag.Variable)]),
            new Predicate("S", true, [new Variable("x4", Flag.Variable)])
        ]);

        
        var unifier = new Resolventer();

        unifier.Solve(disjuncts, invertedTarget);
    }
}