namespace MagicxorAnalyzer.CSharp.Models;

public readonly struct Category : System.IEquatable<Category>
{
    public Category(string name, string shortName)
    {
        Name = name;
        ShortName = shortName;
    }

    public string Name { get; }
    public string ShortName { get; }

    public static bool operator ==(Category left, Category right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Category left, Category right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Category category
               && Equals(category);
    }

    public bool Equals(Category other)
    {
        return Name == other.Name
               && ShortName == other.ShortName;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(Name, ShortName);
    }
}
