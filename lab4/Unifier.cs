using Lab4.Models;
using Lab4.Models.Enums;

namespace Lab4;

public class Unifier
{
    public void Unify(List<Disjunct> disjuncts)
    {
        while (true)
        {
            var disjunctUnification = FindDisjunctsToUnify(disjuncts);

            if (disjunctUnification == null)
            {
                Console.WriteLine("No disjuncts to unify:");
                Console.WriteLine($"Disjuncts: {string.Join("   ", disjuncts)}");

                break;
            }

            Console.WriteLine($"Disjuncts: {string.Join("   ", disjuncts)}");
            Console.WriteLine($"Unifying disjuncts: {disjunctUnification.MainDisjunct}    {disjunctUnification.TargetDisjunct}");
            Console.WriteLine($"Unifying predicates: {disjunctUnification.MainPredicate}   {disjunctUnification.TargetPredicate}");

            MakeUnification(disjuncts, disjunctUnification);
            
            Console.WriteLine("-----------------------------------------------");
            
        }
    }

    private void MakeUnification(List<Disjunct> disjuncts, DisjunctUnification unification)
    {
        disjuncts.Remove(unification.MainDisjunct);

        unification.TargetDisjunct.Predicates.Remove(unification.TargetPredicate);

        var changes = new Dictionary<string, string>();

        for (var i = 0; i < unification.MainPredicate.Variables.Count; i++)
        {
            if (unification.TargetPredicate.Variables[i].Flag == Flag.NoValue)
            {
                changes[unification.TargetPredicate.Variables[i].Name] =
                    unification.MainPredicate.Variables[i].Name;
            }
                    
        }

        Console.WriteLine($"Changes: {string.Join(" ", changes.Select(kv => $"{kv.Key} -> {kv.Value}"))}");

        foreach (var targetPredicate in unification.TargetDisjunct.Predicates)
        {
            foreach (var variable in targetPredicate.Variables)
            {
                if (changes.TryGetValue(variable.Name, out var name))
                {
                    variable.Name = name;
                    variable.Value = name;
                    variable.Flag = Flag.Linked;
                }
            }
        }
    }
    
    private DisjunctUnification? FindDisjunctsToUnify(List<Disjunct> disjuncts)
    {
        foreach (var mainDisjunct in disjuncts)
        {
            foreach (var mainPredicate in mainDisjunct.Predicates)
            {
                if (mainPredicate.Variables.Any(v => v.Flag != Flag.HasValue))  
                    continue;

                foreach (var targetDisjunct in disjuncts)
                {
                    if (targetDisjunct == mainDisjunct)
                        continue;
                    
                    foreach (var targetPredicate in targetDisjunct.Predicates)
                    {
                        if (mainPredicate.CanBeUnified(targetPredicate))
                            return new DisjunctUnification(mainDisjunct, mainPredicate, targetDisjunct, targetPredicate);
                    }
                }
            }
        }

        return null;
    }
}