using Lab4.Models;
using Lab4.Models.Enums;

namespace Lab4;

public static class Unifier
{
    public static Dictionary<string, Variable>? Unify(Predicate predicate1, Predicate predicate2)
    {
        int mainPredicate = 0;
        
        if (predicate1.Name != predicate2.Name)
            return null;

        if (predicate1.Sign == predicate2.Sign)
            return null;

        if (predicate1.Variables.Count != predicate2.Variables.Count)
            return null;

        var changes = new Dictionary<string, Variable>();

        for (int i = 0; i < predicate1.Variables.Count(); i++)
        {
            var v1 = predicate1.Variables[i];
            var v2 = predicate2.Variables[i];

            if (v1.Flag == Flag.Constant && v2.Flag == Flag.Constant) // Две константы
            {
                if (v1.Name != v2.Name)
                    return null;
            }
            else if (v1.Flag == Flag.Constant && v2.Flag == Flag.Variable) // Константа и переменная
            {
                if (mainPredicate == 2)
                    return null;
                
                mainPredicate = 1;
                
                changes[v2.Name] = v1;
            }
            else if (v1.Flag == Flag.Variable && v2.Flag == Flag.Constant) // Переменная и константа
            {
                if (mainPredicate == 1)
                    return null;

                mainPredicate = 2;
                
                changes[v1.Name] = v2;
            }
            else if (v1.Flag == Flag.Variable && v2.Flag == Flag.Variable) // Две переменные
            {
                changes[v1.Name] = v2;
            }
        }


        return changes;
    }
}