using Lab4.Models;
using Lab4.Models.Enums;

namespace Lab4;

public class Resolventer
{
    public void Solve(List<Disjunct> disjuncts, Disjunct invertedTarget)
    {
        disjuncts = disjuncts.Prepend(invertedTarget).ToList();
        
        while (true)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Current disjuncts:");
            Console.WriteLine(string.Join("\n", disjuncts));
            var resolvent = UnifyDisjuncts(disjuncts);

            if (resolvent is null)
            {
                if (disjuncts.Count == 0)
                {
                    Console.WriteLine("Result: true. No disjuncts left");
                }
                else
                {
                    Console.WriteLine("Result: false. No disjuncts to unify");
                    Console.WriteLine($"Disjuncts list: {string.Join(" _ ", disjuncts)}");
                }
                
                break;
            }
            
            if (resolvent.Predicates.Count == 0)
            {
                Console.WriteLine("Result: true. Resolvent is empty");
                break;
            }
        }
    }

    public Disjunct? UnifyDisjuncts(List<Disjunct> disjuncts)
    {
        for (int i = 0; i < disjuncts.Count; i++)
        {
            for (int j = 0; j < disjuncts.Count(); j++)
            {
                if(i == j)
                    continue;

                for (int k = 0; k < disjuncts[i].Predicates.Count; k++)
                {
                    for (int l = 0; l < disjuncts[j].Predicates.Count; l++)
                    {
                        var changes = Unifier.Unify(disjuncts[i].Predicates[k],
                            disjuncts[j].Predicates[l]);

                        if (changes != null)
                        {
                            Console.WriteLine($"Found disjuncts to unify: {disjuncts[i]}   {disjuncts[j]}");
                            Console.WriteLine($"Predicates to unify: {disjuncts[i].Predicates[k]}    {disjuncts[j].Predicates[l]}");
                            Console.WriteLine($"Changes: {string.Join(", ", changes.Select(c => $"{c.Key} -> {c.Value.Name}"))}");

                            disjuncts[i].Predicates.RemoveAt(k);
                            disjuncts[j].Predicates.RemoveAt(l);

                            
                            for (int m = 0; m < disjuncts[i].Predicates.Count; m++)
                            {
                                for (int n = 0; n < disjuncts[i].Predicates[m].Variables.Count; n++)
                                {
                                    if (changes.TryGetValue(disjuncts[i].Predicates[m].Variables[n].Name, out var newVariable))
                                    {
                                        disjuncts[i].Predicates[m].Variables[n].Name = newVariable.Name;
                                        disjuncts[i].Predicates[m].Variables[n].Flag = newVariable.Flag;
                                    }
                                }
                            }
                            
                            for (int m = 0; m < disjuncts[j].Predicates.Count; m++)
                            {
                                for (int n = 0; n < disjuncts[j].Predicates[m].Variables.Count; n++)
                                {
                                    if (changes.TryGetValue(disjuncts[j].Predicates[m].Variables[n].Name, out var newVariable))
                                    {
                                        disjuncts[j].Predicates[m].Variables[n].Name = newVariable.Name;
                                        disjuncts[j].Predicates[m].Variables[n].Flag = newVariable.Flag;
                                    }
                                }
                            }

                            var resolvent = new Disjunct([]);

                            var disjunct1 = disjuncts[i];
                            var disjunct2 = disjuncts[j];
                            
                            foreach (var predicate in disjunct1.Predicates)
                            {
                                if(!resolvent.Predicates.Any(p => p.Equals(predicate)))
                                    resolvent.Predicates.Add(predicate);
                            }
                            
                            foreach (var predicate in disjunct2.Predicates)
                            {
                                if(!resolvent.Predicates.Any(p => p.Equals(predicate)))
                                    resolvent.Predicates.Add(predicate);
                            }

                            disjuncts.Remove(disjunct1);
                            disjuncts.Remove(disjunct2);

                            Console.WriteLine($"Resolvent: {resolvent}");

                            for (int m = 0; m < resolvent.Predicates.Count; m++)
                            {
                                for (int n = 0; n < resolvent.Predicates.Count; n++)
                                {
                                    if (m == n)
                                        continue;

                                    if (resolvent.Predicates[m].Sign != resolvent.Predicates[n].Sign &&
                                        resolvent.Predicates[m].Name == resolvent.Predicates[n].Name &&
                                        resolvent.Predicates[m].Variables.SequenceEqual(resolvent.Predicates[n].Variables))
                                    {
                                        var predicate1 = resolvent.Predicates[m];
                                        var predicate2 = resolvent.Predicates[n];

                                        resolvent.Predicates.Remove(predicate1);
                                        resolvent.Predicates.Remove(predicate2);

                                        Console.WriteLine("Removed opposite predicates from resolvent");
                                        Console.WriteLine($"Resolvent: {resolvent}");
                                    }
                                }
                            }
                            
                            if (resolvent.Predicates.Count != 0)
                                disjuncts.Add(resolvent);
                            
                            return resolvent;
                        }
                    }
                }
            }
        }

        return null;
    }
}