namespace Lab3.Extensions;

public static class IListExtensions
{
    public static void Push<T>(this IList<T> list, T value)
    {
        list.Add(value);
    }

    public static T Peek<T>(this IList<T> list)
    {
        return list.Last();
    }

    public static T Pop<T>(this IList<T> list)
    {
        var value = list.Peek();

        list.RemoveAt(list.Count - 1);

        return value;
    }
}